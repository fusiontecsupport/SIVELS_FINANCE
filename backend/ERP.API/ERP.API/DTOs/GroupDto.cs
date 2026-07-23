namespace ERP.API.DTOs
{
    public class GroupDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsAssigned { get; set; }
    }

    public class SaveUserGroupsDto
    {
        public List<int> GroupIds { get; set; }
    }

    public class GroupCreateDto
    {
        public string Name { get; set; }
    }

    public class PermissionDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Module { get; set; }   // e.g. "Users"
        public string? Action { get; set; }   // e.g. "Index", "Create", "Edit", "Delete"
        public bool IsAssigned { get; set; }
    }

    public class SaveGroupPermissionsDto
    {
        public List<Guid> PermissionIds { get; set; }
    }
}