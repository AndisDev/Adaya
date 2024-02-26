using System.Data;
using Adaya.Helper;
using Adaya.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Adaya.Controllers
{
    public class GenerateController : Controller
    {
		private readonly ApplicationDbContext _context;

		public GenerateController(ApplicationDbContext context)
		{
			_context = context;
		}
		public IActionResult Index()
        {
            return View();
        }

		[HttpPost]
		public IActionResult Create([FromBody] List<Generate> personalData)
		{
			Console.WriteLine("Received personalData: " + personalData);

			try
			{
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("Nama", typeof(string));
				dataTable.Columns.Add("IdGender", typeof(int));
				dataTable.Columns.Add("IdHobi", typeof(char));
				dataTable.Columns.Add("Umur", typeof(int));

				if (personalData == null || !personalData.Any())
				{
					return Json(new { success = false, message = "Generate Data Terlebih Dahulu" });
				}

				foreach (var data in personalData)
					{
						dataTable.Rows.Add(data.Nama, data.IdGender, data.IdHobi, data.Umur);
					}

					var dataTableParameter = new SqlParameter("@PersonalData", SqlDbType.Structured)
					{
						TypeName = "dbo.PersonalTableType",
						Value = dataTable
					};

					_context.Database.ExecuteSqlRaw("EXEC dbo.InsertBulkPersonalData @PersonalData", dataTableParameter);
					_context.Database.ExecuteSqlRaw("EXEC dbo.InsertUmurTotals");

					return Json(new { success = true, message = "Data inserted successfully!" });
				

				
			}
			catch (Exception ex)
			{
				return Json(new { success = false, message = "Error: " + ex.Message });
			}
		}


	}
}
