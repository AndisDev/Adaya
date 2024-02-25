using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Adaya.Models
{
	[Table(name: "tblT_Umur")]

	[Keyless]
	public class TBUmur
    {
		[Required]
		public int Umur { get; set; }
        
        [Required]
        public int Total { get; set; }
    }
}
