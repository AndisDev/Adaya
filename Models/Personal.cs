using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Adaya.Models
{

	[Keyless]
	public class Personal
	{
		public string Nama { get; set; }
		public string Gender { get; set; }
		public string Hobi { get; set; }
		public int Umur { get; set; }
	}
}
