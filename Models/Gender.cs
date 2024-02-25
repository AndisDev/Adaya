using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adaya.Models
{
	[Table(name: "tblM_Gender")]
	public class Gender
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Nama { get; set; }
    }
}
