using Adaya.Helper;
using Adaya.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Adaya.Controllers
{
    public class HobiController : Controller
    {
		private readonly ApplicationDbContext _context;

		public HobiController(ApplicationDbContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			var Hobis = _context.Hobis.FromSqlRaw("EXEC dbo.GetAllHobis").ToList();
			return View(Hobis);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(Hobi Hobi)
		{
			try
			{
				if (ModelState.IsValid)
				{
					_context.Database.ExecuteSqlRaw("EXEC dbo.CreateHobi @p0, @p1", Hobi.Id, Hobi.Nama);
					TempData["SweetAlertMessage"] = "Data saved successfully!";
					return RedirectToAction("Index");
				}
			}
			catch (SqlException ex)
			{
				if (ex.Number == 50000)
				{
					if (ex.Message.Contains("Id"))
					{
						ModelState.AddModelError(nameof(Hobi.Id), ex.Message);
					}
					if (ex.Message.Contains("Name"))
					{
						ModelState.AddModelError(nameof(Hobi.Nama), ex.Message);
					}
				}
			}

			return View(Hobi);
		}

		public IActionResult Edit(char id)
		{
			var Hobi = _context.Hobis.FromSqlRaw("EXEC dbo.GetHobi @p0", id).AsEnumerable().FirstOrDefault();

			if (Hobi == null)
			{
				return NotFound();
			}
			return View(Hobi);
		}

		[HttpPost]
		public IActionResult Edit(char id, Hobi Hobi)
		{
			try
			{
				if (ModelState.IsValid)
				{
					_context.Database.ExecuteSqlRaw("EXEC dbo.UpdateHobi @p0, @p1", id, Hobi.Nama);
					TempData["SweetAlertMessageEdit"] = "Data updated successfully!";
					return RedirectToAction("Index");
				}
			}
			catch (SqlException ex)
			{
				if (ex.Number == 50000)
				{
					ModelState.AddModelError(nameof(Hobi.Nama), ex.Message);
				}
			}

			return View(Hobi);
		}

		[HttpPost]
		public IActionResult Delete(char id)
		{
			_context.Database.ExecuteSqlRaw("EXEC dbo.DeleteHobi @p0", id);
			TempData["SweetAlertMessageDelete"] = "Data deleted successfully!";
			return RedirectToAction("Index");
		}
	}
}
