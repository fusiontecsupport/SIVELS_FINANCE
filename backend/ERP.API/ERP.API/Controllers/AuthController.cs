using ERP.API.Data;
using ERP.API.DTOs;
using ERP.API.Models;
using ERP.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace ERP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ERPDbContext _context;
        private readonly PermissionService _permissionService;

        public AuthController(
            ERPDbContext context,
            PermissionService permissionService)
        {
            _context = context;
            _permissionService = permissionService;
        }

        [HttpPost("check-user")]
        public IActionResult CheckUser([FromBody] CheckUserDto dto)
        {
            if (_context.Users.Any(x => x.Username == dto.Username))
            {
                return Ok(new { success = false, message = "Username already exists" });
            }

            if (_context.Users.Any(x => x.Email == dto.Email))
            {
                return Ok(new { success = false, message = "Email already registered" });
            }

            if (_context.Users.Any(x => x.MobileNumber == dto.Mobile))
            {
                return Ok(new { success = false, message = "Mobile number already registered" });
            }

            return Ok(new { success = true, message = "User details validated successfully" });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(
            User user,
            [FromServices] PasswordService passwordService,
            [FromServices] IOtpService otpService)
        {
            // Model Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // OTP Verification Check
            var isVerified = await otpService.IsEmailVerifiedAsync(user.Email);
            if (!isVerified)
            {
                return BadRequest(new
                {
                    message = "Please verify your email before registration."
                });
            }

            // Username Duplicate Check
            if (_context.Users.Any(x => x.Username == user.Username))
            {
                return BadRequest(new
                {
                    message = "Username already exists."
                });
            }

            // Email Duplicate Check
            if (_context.Users.Any(x => x.Email == user.Email))
            {
                return BadRequest(new
                {
                    message = "Email already exists."
                });
            }

            // Mobile Number Duplicate Check
            if (_context.Users.Any(x => x.MobileNumber == user.MobileNumber))
            {
                return BadRequest(new
                {
                    message = "Mobile Number already exists."
                });
            }

            // Password Hashing
            user.PasswordHash = passwordService.HashPassword(
                user,
                user.PasswordHash
            );

            // Save User
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Mark OTP as consumed
            await otpService.MarkOtpAsUsedAsync(user.Email);

            return Ok(new
            {
                status = true,
                message = "User Registered Successfully"
            });
        }

        [HttpPost("send-otp")]
        public async Task<IActionResult> SendOtp(
            [FromBody] SendOtpDto dto,
            [FromServices] IOtpService otpService,
            [FromServices] IEmailService emailService)
        {
            if (string.IsNullOrWhiteSpace(dto.Email))
            {
                return BadRequest(new { message = "Email is required." });
            }

            // Email Duplicate Check
            if (_context.Users.Any(x => x.Email == dto.Email))
            {
                return BadRequest(new { message = "Email already registered." });
            }

            var otpCode = await otpService.GenerateOtpAsync(dto.Email);

            string subject = "Email Verification Code";
            string body = $@"Hello,

Your ERP verification code is:

{otpCode}

This OTP is valid for 5 minutes.

Do not share this code with anyone.

Thank you.";

            await emailService.SendEmailAsync(dto.Email, subject, body);

            return Ok(new { status = true, message = "OTP sent successfully." });
        }

        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp(
            [FromBody] VerifyOtpDto dto,
            [FromServices] IOtpService otpService)
        {
            if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Otp))
            {
                return BadRequest(new { message = "Email and OTP are required." });
            }

            var result = await otpService.VerifyOtpAsync(dto.Email, dto.Otp);
            if (result.Success)
            {
                return Ok(new { status = true, message = result.Message });
            }
            return BadRequest(new { message = result.Message });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(
            LoginDto user,
            PasswordService passwordService)
        {
            var existingUser = _context.Users
                .FirstOrDefault(x => x.Username == user.Username);

            if (existingUser == null)
            {
                return Unauthorized(new
                {
                    status = false,
                    message = "Invalid Username or Password"
                });
            }

            bool isValid = passwordService.VerifyPassword(
                existingUser,
                user.Password
            );

            if (!isValid)
            {
                return Unauthorized(new
                {
                    status = false,
                    message = "Invalid Username or Password"
                });
            }

            var previousLogin = existingUser.LastLogin;
            existingUser.LastLogin = DateTime.Now;
            await _context.SaveChangesAsync();

            var permissions = await _permissionService
                .GetEffectivePermissions(existingUser.UserId);

            return Ok(new
            {
                status = true,
                message = "Login Success",
                userId = existingUser.UserId,
                username = existingUser.Username,
                permissions = permissions,
                firstName = existingUser.FirstName,
                lastName = existingUser.LastName,
                lastLogin = previousLogin
            });
        }
    }
}