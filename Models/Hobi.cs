using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adaya.Models
{
	[Table(name: "tblM_Hobi")]
	public class Hobi
    {
        [Key]
		[Required]
		public char Id { get; set; }
        
        [Required]
        public string Nama { get; set; }
    }
}
