using System.Reflection;
using Adaya.Helper;
using Adaya.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Adaya.Controllers
{
	public class GenderController : Controller
	{
		private readonly ApplicationDbContext _context;

		public GenderController(ApplicationDbContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			var genders = _context.Genders.FromSqlRaw("EXEC dbo.GetAllGenders").ToList();
			return View(genders);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(Gender gender)
		{
			try
			{
				if (ModelState.IsValid)
				{
					_context.Database.ExecuteSqlRaw("EXEC dbo.CreateGender @p0", gender.Nama);
					TempData["SweetAlertMessage"] = "Data saved successfully!";
					return RedirectToAction("Index");
				}
			}
			catch (SqlException ex)
			{
				if (ex.Number == 50000)
				{
					ModelState.AddModelError(nameof(gender.Nama), ex.Message);
				}
			}

			return View(gender);
		}

		public IActionResult Edit(int id)
		{
			var gender = _context.Genders.FromSqlRaw("EXEC dbo.GetGender @p0", id).AsEnumerable().FirstOrDefault();

			if (gender == null)
			{
				return NotFound();
			}
			return View(gender);
		}

		[HttpPost]
		public IActionResult Edit(int id, Gender gender)
		{
			try
			{
				if (ModelState.IsValid)
				{
					_context.Database.ExecuteSqlRaw("EXEC dbo.UpdateGender @p0, @p1", id, gender.Nama);
					TempData["SweetAlertMessageEdit"] = "Data updated successfully!";
					return RedirectToAction("Index");
				}
			}
			catch (SqlException ex)
			{
				if (ex.Number == 50000)
				{
					ModelState.AddModelError(nameof(gender.Nama), ex.Message);
				}
			}

			return View(gender);
		}

		[HttpPost]
		public IActionResult Delete(int id)
		{
			_context.Database.ExecuteSqlRaw("EXEC dbo.DeleteGender @p0", id);
			TempData["SweetAlertMessageDelete"] = "Data deleted successfully!";
			return RedirectToAction("Index");
		}
	}
}
