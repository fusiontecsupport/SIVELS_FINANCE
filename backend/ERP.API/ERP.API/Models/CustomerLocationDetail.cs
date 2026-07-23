using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.API.Models
{
    [Table("CUSTOMERLOCATIONDETAIL")]
    public class CustomerLocationDetail
    {
        [Key]
        public int CATEAID { get; set; }

        public int CATEID { get; set; }
        public string CATEAADDR { get; set; }
        public int LOCTID { get; set; }
        public int STATEID { get; set; }
        public string? CATEA_GST_NO { get; set; }
        public string? CATEA_PINCODE { get; set; }
        public string? CATEAADDR1 { get; set; }
        public string? CATEAADDR2 { get; set; }
        public string? CATEA_CITY { get; set; }
        public string? CATEA_COUNTRY { get; set; }
    }
}