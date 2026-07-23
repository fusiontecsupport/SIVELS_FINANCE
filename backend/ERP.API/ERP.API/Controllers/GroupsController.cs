using ERP.API.Data;
using ERP.API.DTOs;
using ERP.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupsController : ControllerBase
    {
        private readonly ERPDbContext _context;

        public GroupsController(ERPDbContext context)
        {
            _context = context;
        }

        // ---------- GROUPS CRUD ----------

        // GET: api/Groups/list
        [HttpGet("list")]
        public async Task<IActionResult> GetGroups()
        {
            var groups = await _context.Groups
                .Select(g => new { g.Id, g.Name })
                .ToListAsync();

            return Ok(groups);
        }

        // GET: api/Groups/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGroup(int id)
        {
            var group = await _context.Groups.FindAsync(id);
            if (group == null) return NotFound();
            return Ok(new { group.Id, group.Name });
        }

        // POST: api/Groups/create
        [HttpPost("create")]
        public async Task<IActionResult> CreateGroup(GroupCreateDto dto)
        {
            var group = new Group { Name = dto.Name };
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();
            return Ok(new { status = true, message = "Group Created Successfully" });
        }

        // PUT: api/Groups/update/5
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateGroup(int id, GroupCreateDto dto)
        {
            var group = await _context.Groups.FindAsync(id);
            if (group == null) return NotFound();

            group.Name = dto.Name;
            await _context.SaveChangesAsync();
            return Ok(new { status = true, message = "Group Updated Successfully" });
        }

        // DELETE: api/Groups/delete/5
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            var group = await _context.Groups.FindAsync(id);
            if (group == null) return NotFound();

            // Also remove related links so we don't leave orphaned rows
            var roleLinks = _context.ApplicationRoleGroups.Where(rg => rg.GroupId == id);
            var userLinks = _context.ApplicationUserGroups.Where(ug => ug.GroupId == id);
            _context.ApplicationRoleGroups.RemoveRange(roleLinks);
            _context.ApplicationUserGroups.RemoveRange(userLinks);

            _context.Groups.Remove(group);
            await _context.SaveChangesAsync();
            return Ok(new { status = true, message = "Group Deleted Successfully" });
        }

        // ---------- USER <-> GROUP ASSIGNMENT ----------

        // GET: api/Groups/user/5
        // Returns all groups, marking which ones this user is currently assigned to
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetGroupsForUser(int userId)
        {
            var allGroups = await _context.Groups.ToListAsync();

            var assignedGroupIds = await _context.ApplicationUserGroups
                .Where(ug => ug.UserId == userId)
                .Select(ug => ug.GroupId)
                .ToListAsync();

            var result = allGroups.Select(g => new GroupDto
            {
                Id = g.Id,
                Name = g.Name,
                IsAssigned = assignedGroupIds.Contains(g.Id)
            });

            return Ok(result);
        }

        // PUT: api/Groups/user/5
        // Replaces this user's group assignments with the given list
        [HttpPut("user/{userId}")]
        public async Task<IActionResult> SaveUserGroups(int userId, SaveUserGroupsDto dto)
        {
            var existing = _context.ApplicationUserGroups
                .Where(ug => ug.UserId == userId);

            _context.ApplicationUserGroups.RemoveRange(existing);

            foreach (var groupId in dto.GroupIds)
            {
                _context.ApplicationUserGroups.Add(new ApplicationUserGroup
                {
                    UserId = userId,
                    GroupId = groupId
                });
            }

            await _context.SaveChangesAsync();

            return Ok(new { status = true, message = "Groups updated successfully" });
        }

        // ---------- GROUP <-> PERMISSIONS ASSIGNMENT ----------

        // GET: api/Groups/5/permissions
        // Returns ALL permissions, marking which ones this group currently has
        [HttpGet("{groupId}/permissions")]
        public async Task<IActionResult> GetPermissionsForGroup(int groupId)
        {
            var allPermissions = await _context.AspNetRoles
                .OrderBy(r => r.RControllerName)
                .ThenBy(r => r.Name)
                .ToListAsync();

            var assignedRoleIds = await _context.ApplicationRoleGroups
                .Where(rg => rg.GroupId == groupId)
                .Select(rg => rg.RoleId)
                .ToListAsync();

            var result = allPermissions.Select(p => new PermissionDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Module = p.RControllerName,
                Action = p.RMenuIndex,
                IsAssigned = assignedRoleIds.Contains(p.Id)
            });

            return Ok(result);
        }

        // PUT: api/Groups/5/permissions
        [HttpPut("{groupId}/permissions")]
        public async Task<IActionResult> SaveGroupPermissions(int groupId, SaveGroupPermissionsDto dto)
        {
            var existing = _context.ApplicationRoleGroups
                .Where(rg => rg.GroupId == groupId);

            _context.ApplicationRoleGroups.RemoveRange(existing);

            foreach (var roleId in dto.PermissionIds)
            {
                _context.ApplicationRoleGroups.Add(new ApplicationRoleGroup
                {
                    GroupId = groupId,
                    RoleId = roleId
                });
            }

            await _context.SaveChangesAsync();
            return Ok(new { status = true, message = "Permissions updated successfully" });
        }
    }
}