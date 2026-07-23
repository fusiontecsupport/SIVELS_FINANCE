using ERP.API.Data;
using ERP.API.DTOs;
using ERP.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly ERPDbContext _context;

        public RolesController(ERPDbContext context)
        {
            _context = context;
        }

        // GET: api/Roles/list
        [HttpGet("list")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _context.AspNetRoles
                .OrderBy(r => r.RControllerName)
                .ThenBy(r => r.Name)
                .Select(r => new RoleListDto
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description,
                    MenuName = r.RMenuType,
                    ControllerName = r.RControllerName,
                    MenuIndex = r.RMenuIndex,
                    MenuGroupId = r.RMenuGroupId,
                    Order = r.RMenuGroupOrder
                })
                .ToListAsync();

            return Ok(roles);
        }

        // GET: api/Roles/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRole(Guid id)
        {
            var role = await _context.AspNetRoles.FindAsync(id);
            if (role == null) return NotFound();

            return Ok(new RoleListDto
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description,
                MenuName = role.RMenuType,
                ControllerName = role.RControllerName,
                MenuIndex = role.RMenuIndex,
                MenuGroupId = role.RMenuGroupId,
                Order = role.RMenuGroupOrder
            });
        }

        // POST: api/Roles/create
        [HttpPost("create")]
        public async Task<IActionResult> CreateRole(RoleSaveDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest(new { status = false, message = "Role name is required" });

            var nameExists = await _context.AspNetRoles.AnyAsync(r => r.Name == dto.Name);
            if (nameExists)
                return BadRequest(new { status = false, message = "A role with this name already exists" });

            var role = new AspNetRole
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
                RMenuType = dto.MenuName,
                RControllerName = dto.ControllerName,
                RMenuIndex = dto.MenuIndex,
                RMenuGroupId = dto.MenuGroupId,
                RMenuGroupOrder = dto.Order
            };

            _context.AspNetRoles.Add(role);
            await _context.SaveChangesAsync();

            return Ok(new { status = true, message = "Role Created Successfully", id = role.Id });
        }

        // PUT: api/Roles/update/{id}
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateRole(Guid id, RoleSaveDto dto)
        {
            var role = await _context.AspNetRoles.FindAsync(id);
            if (role == null) return NotFound();

            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest(new { status = false, message = "Role name is required" });

            var nameTaken = await _context.AspNetRoles
                .AnyAsync(r => r.Name == dto.Name && r.Id != id);
            if (nameTaken)
                return BadRequest(new { status = false, message = "A role with this name already exists" });

            role.Name = dto.Name;
            role.Description = dto.Description;
            role.RMenuType = dto.MenuName;
            role.RControllerName = dto.ControllerName;
            role.RMenuIndex = dto.MenuIndex;
            role.RMenuGroupId = dto.MenuGroupId;
            role.RMenuGroupOrder = dto.Order;

            await _context.SaveChangesAsync();
            return Ok(new { status = true, message = "Role Updated Successfully" });
        }

        // DELETE: api/Roles/delete/{id}
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteRole(Guid id)
        {
            var role = await _context.AspNetRoles.FindAsync(id);
            if (role == null) return NotFound();

            // A role in use by a group's permission set shouldn't just vanish
            // out from under it -- clean up the link table too.
            var links = _context.ApplicationRoleGroups.Where(rg => rg.RoleId == id);
            _context.ApplicationRoleGroups.RemoveRange(links);

            _context.AspNetRoles.Remove(role);
            await _context.SaveChangesAsync();

            return Ok(new { status = true, message = "Role Deleted Successfully" });
        }
    }
}
