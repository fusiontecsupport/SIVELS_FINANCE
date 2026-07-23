using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.API.Models
{
    [Table("LOCATIONMASTER")]
    public class LocationMaster
    {
        [Key]
        public int LOCTID { get; set; }

        [Required]
        [MaxLength(100)]
        public string LOCTNAME { get; set; }

        // Tags this location to a state (used for the cascading dropdown)
        public int STATEID { get; set; }

        public int DISPSTATUS { get; set; } = 1;
    }
}
