using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.API.Models
{
    [Table("CUSTOMERMASTER")]
    public class CustomerMaster
    {
        [Key]
        public int CATEID { get; set; }

        public int CATETID { get; set; }
        public int CATENO { get; set; }
        public string? CATECODE { get; set; }
        public string? CATENAME { get; set; }
        public string? CATEDNAME { get; set; }
        public string? CATE_GST_NO { get; set; }
        public string? CATE_PAN_NO { get; set; }
        public string? CATE_TAN_NO { get; set; }
        public int? CURNID { get; set; }
        public string? CATEBANKNAME { get; set; }
        public string? CATEBANKBRNCHNAME { get; set; }
        public string? CATEBANKADDR { get; set; }
        public string? CATEBANK_ACTYPE { get; set; }
        public string? CATEBANK_ACNO { get; set; }
        public string? CATEBANK_IFCS_CODE { get; set; }
        public string? CATEBANK_IBAN_CODE { get; set; }
        public string? CATEBANK_SWIFT_CODE { get; set; }
        public string? CUSRID { get; set; }
        public int? LMUSRID { get; set; }
        public short? DISPSTATUS { get; set; }
        public DateTime? PRCSDATE { get; set; }
        public string? CATE_TALLY_NAME { get; set; }
        public int ACHEADID { get; set; }
    }
}