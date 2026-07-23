using ERP.API.Data;
using ERP.API.Models;
using ERP.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERP.API.DTOs;

namespace ERP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ERPDbContext _context;
        private readonly PasswordService _passwordService;

        public UserController(
            ERPDbContext context,
            PasswordService passwordService)
        {
            _context = context;
            _passwordService = passwordService;
        }

        // GET: api/User/list
        [HttpGet("list")]
        public IActionResult GetUsers()
        {
            var users = _context.Users
                .Select(u => new UserDto
                {
                    UserId = u.UserId,
                    Username = u.Username,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    MobileNumber = u.MobileNumber,
                    Gender = u.Gender,
                    Email = u.Email,
                    DateOfBirth = u.DateOfBirth
                })
                .ToList();

            return Ok(users);
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return NotFound();

            return Ok(new UserDto
            {
                UserId = user.UserId,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                MobileNumber = user.MobileNumber,
                Gender = user.Gender,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth
            });
        }

        // POST: api/User/create
        [HttpPost("create")]
        public IActionResult CreateUser(CreateUserDto user)
        {
            if (!ModelState.IsValid)
            {
                var errorMsg = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).FirstOrDefault();
                return BadRequest(new { message = errorMsg });
            }

            var existingUser = _context.Users
                .FirstOrDefault(x => x.Username == user.Username);

            if (existingUser != null)
            {
                return BadRequest(new
                {
                    message = "Username already exists."
                });
            }

            if (_context.Users.Any(x => x.Email == user.Email))
            {
                return BadRequest(new { message = "Email already exists." });
            }

            if (_context.Users.Any(x => x.MobileNumber == user.MobileNumber))
            {
                return BadRequest(new { message = "Mobile Number already exists." });
            }

            var newUser = new User();

            newUser.Username = user.Username;
            newUser.FirstName = user.FirstName;
            newUser.LastName = user.LastName;
            newUser.MobileNumber = user.MobileNumber;
            newUser.Gender = user.Gender;
            newUser.Email = user.Email;
            newUser.DateOfBirth = user.DateOfBirth;

            newUser.PasswordHash =
                _passwordService.HashPassword(
                    newUser,
                    user.Password
                );

            _context.Users.Add(newUser);
            _context.SaveChanges();

            return Ok(new
            {
                status = true,
                message = "User Created Successfully"
            });
        }

        // PUT: api/User/update/5
        [HttpPut("update/{id}")]
        public IActionResult UpdateUser(int id, UpdateUserDto dto)
        {
            var user = _context.Users.Find(id);
            if (user == null) return NotFound();

            user.Username = dto.Username;
            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.MobileNumber = dto.MobileNumber;
            user.Gender = dto.Gender;
            user.Email = dto.Email;
            user.DateOfBirth = dto.DateOfBirth;

            if (!string.IsNullOrWhiteSpace(dto.Password))
            {
                user.PasswordHash = _passwordService.HashPassword(user, dto.Password);
            }

            _context.SaveChanges();

            return Ok(new
            {
                status = true,
                message = "User Updated Successfully"
            });
        }

        // PUT: api/User/reset-password/5
        [HttpPut("reset-password/{id}")]
        public IActionResult ResetPassword(int id, ResetPasswordDto dto)
        {
            var user = _context.Users.Find(id);
            if (user == null) return NotFound();

            if (string.IsNullOrWhiteSpace(dto.NewPassword))
            {
                return BadRequest(new
                {
                    message = "New password is required"
                });
            }

            user.PasswordHash = _passwordService.HashPassword(user, dto.NewPassword);
            _context.SaveChanges();

            return Ok(new
            {
                status = true,
                message = "Password Reset Successfully"
            });
        }

        // DELETE: api/User/delete/5
        [HttpDelete("delete/{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return NotFound();

            _context.Users.Remove(user);
            _context.SaveChanges();

            return Ok(new
            {
                status = true,
                message = "User Deleted Successfully"
            });
        }

        // GET: api/User/{id}/effective-permissions
        [HttpGet("{id}/effective-permissions")]
        public async Task<IActionResult> GetEffectivePermissions(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            // Find which groups this user belongs to
            var groupIds = await _context.ApplicationUserGroups
                .Where(ug => ug.UserId == id)
                .Select(ug => ug.GroupId)
                .ToListAsync();

            // Find which permission IDs those groups have (distinct, since multiple groups could share a permission)
            var roleIds = await _context.ApplicationRoleGroups
                .Where(rg => groupIds.Contains(rg.GroupId))
                .Select(rg => rg.RoleId)
                .Distinct()
                .ToListAsync();

            // Get the actual permission details
            var permissions = await _context.AspNetRoles
                .Where(r => roleIds.Contains(r.Id))
                .OrderBy(r => r.RControllerName)
                .ThenBy(r => r.Name)
                .Select(r => new
                {
                    r.Name,
                    r.Description,
                    Module = r.RControllerName
                })
                .ToListAsync();

            return Ok(new
            {
                username = user.Username,
                permissions = permissions
            });
        }
    }
}