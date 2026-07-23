namespace ERP.API.Models
{
    public class AspNetRole
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? RMenuType { get; set; }
        public string? RControllerName { get; set; }
        public int? RMenuGroupId { get; set; }
        public int? RMenuGroupOrder { get; set; }
        public string? RMenuIndex { get; set; }
    }
}