using ERP.API.Data;
using ERP.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.API.Services
{
    public interface IOtpService
    {
        Task<string> GenerateOtpAsync(string email);
        Task<(bool Success, string Message)> VerifyOtpAsync(string email, string otp);
        Task<bool> IsEmailVerifiedAsync(string email);
        Task MarkOtpAsUsedAsync(string email);
    }

    public class OtpService : IOtpService
    {
        private readonly ERPDbContext _context;
        private readonly PasswordHasher<EmailOtp> _hasher;

        public OtpService(ERPDbContext context)
        {
            _context = context;
            _hasher = new PasswordHasher<EmailOtp>();
        }

        public async Task<string> GenerateOtpAsync(string email)
        {
            // Invalidate existing active OTPs for this email
            var existingOtps = await _context.EmailOtps
                .Where(o => o.Email == email && !o.IsUsed && !o.IsVerified)
                .ToListAsync();

            foreach (var existing in existingOtps)
            {
                existing.IsUsed = true; // Mark as unusable
            }

            // Generate 6-digit OTP
            var random = new Random();
            var otpCode = random.Next(100000, 999999).ToString();

            var emailOtp = new EmailOtp
            {
                Email = email,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddMinutes(5),
                IsVerified = false,
                IsUsed = false,
                Attempts = 0
            };

            emailOtp.OtpHash = _hasher.HashPassword(emailOtp, otpCode);

            _context.EmailOtps.Add(emailOtp);
            await _context.SaveChangesAsync();

            return otpCode;
        }

        public async Task<(bool Success, string Message)> VerifyOtpAsync(string email, string otp)
        {
            var emailOtp = await _context.EmailOtps
                .Where(o => o.Email == email && !o.IsUsed)
                .OrderByDescending(o => o.CreatedAt)
                .FirstOrDefaultAsync();

            if (emailOtp == null)
            {
                return (false, "No active OTP found. Please request a new one.");
            }

            if (emailOtp.IsVerified)
            {
                return (false, "Email is already verified.");
            }

            if (emailOtp.Attempts >= 5)
            {
                emailOtp.IsUsed = true;
                await _context.SaveChangesAsync();
                return (false, "Maximum attempts exceeded. Please request a new OTP.");
            }

            if (DateTime.UtcNow > emailOtp.ExpiresAt)
            {
                emailOtp.IsUsed = true;
                await _context.SaveChangesAsync();
                return (false, "OTP has expired. Please request a new one.");
            }

            emailOtp.Attempts++;

            var verificationResult = _hasher.VerifyHashedPassword(emailOtp, emailOtp.OtpHash, otp);

            if (verificationResult == PasswordVerificationResult.Success)
            {
                emailOtp.IsVerified = true;
                await _context.SaveChangesAsync();
                return (true, "Email verified successfully.");
            }

            await _context.SaveChangesAsync();
            return (false, "Invalid OTP.");
        }

        public async Task<bool> IsEmailVerifiedAsync(string email)
        {
            var emailOtp = await _context.EmailOtps
                .Where(o => o.Email == email)
                .OrderByDescending(o => o.CreatedAt)
                .FirstOrDefaultAsync();

            return emailOtp != null && emailOtp.IsVerified && !emailOtp.IsUsed;
        }

        public async Task MarkOtpAsUsedAsync(string email)
        {
            var emailOtp = await _context.EmailOtps
                .Where(o => o.Email == email && o.IsVerified && !o.IsUsed)
                .OrderByDescending(o => o.CreatedAt)
                .FirstOrDefaultAsync();

            if (emailOtp != null)
            {
                emailOtp.IsUsed = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
