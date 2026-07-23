using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.API.Models
{
    [Table("COMPANYMASTER")]
    public class CompanyMaster
    {
        [Key]
        public int CMPYID { get; set; }

        [Required]
        public string CMPYNAME { get; set; }

        public string? CMPYADDR1 { get; set; }
        public string? CMPYADDR2 { get; set; }
        public string? CMPYADDR3 { get; set; }
        public string? CMPYCITY { get; set; }
        public string? CMPYSTATE { get; set; }
        public string? CMPYPINCODE { get; set; }
        public string? CMPYCONTACTPERSON { get; set; }
        public string? CMPYPHONE1 { get; set; }
        public string? CMPYPHONE2 { get; set; }
        public string? CMPYEMAIL { get; set; }
        public string? CMPYGSTNO { get; set; }
        public string? CMPYPANNO { get; set; }
        public short DISPSTATUS { get; set; } = 1;
        public string? CUSRID { get; set; }
        public DateTime? PRCSDATE { get; set; }
    }
}