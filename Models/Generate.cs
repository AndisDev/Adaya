using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adaya.Models
{
	[Table(name: "tblT_Personal")]
	public class Generate
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Nama { get; set; }

		[Required]
		public int IdGender { get; set; }

		[Required]
		public char IdHobi { get; set; }

		[Required]
		public int Umur { get; set; }
	}
}
