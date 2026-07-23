using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.API.Models
{
    [Table("STATEMASTER")]
    public class StateMaster
    {
        [Key]
        public int STATEID { get; set; }

        [Required]
        [MaxLength(100)]
        public string STATENAME { get; set; }

        public int DISPSTATUS { get; set; } = 1;
    }
}
