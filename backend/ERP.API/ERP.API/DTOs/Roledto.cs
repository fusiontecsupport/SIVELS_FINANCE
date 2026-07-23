namespace ERP.API.DTOs
{
    public class RoleListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? MenuName { get; set; }        // RMenuType
        public string? ControllerName { get; set; }   // RControllerName
        public string? MenuIndex { get; set; }         // RMenuIndex
        public int? MenuGroupId { get; set; }           // RMenuGroupId
        public int? Order { get; set; }                  // RMenuGroupOrder
    }

    public class RoleSaveDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? MenuName { get; set; }
        public string? ControllerName { get; set; }
        public string? MenuIndex { get; set; }
        public int? MenuGroupId { get; set; }
        public int? Order { get; set; }
    }
}
