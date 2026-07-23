using ERP.API.Data;
using Microsoft.EntityFrameworkCore;

namespace ERP.API.Services
{
    public class PermissionService
    {
        private readonly ERPDbContext _context;

        public PermissionService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<List<string>> GetEffectivePermissions(int userId)
        {
            // 1. Find which groups this user belongs to
            var groupIds = await _context.ApplicationUserGroups
                .Where(ug => ug.UserId == userId)
                .Select(ug => ug.GroupId)
                .ToListAsync();

            // 2. Find which permission IDs (RoleIds) those groups have
            var roleIds = await _context.ApplicationRoleGroups
                .Where(rg => groupIds.Contains(rg.GroupId))
                .Select(rg => rg.RoleId)
                .Distinct()
                .ToListAsync();

            // 3. Get the actual permission names (e.g. "UsersEdit")
            var permissionNames = await _context.AspNetRoles
                .Where(r => roleIds.Contains(r.Id))
                .Select(r => r.Name)
                .ToListAsync();

            return permissionNames;
        }
    }
}