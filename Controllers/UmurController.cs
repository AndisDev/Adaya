using System.Reflection;
using Adaya.Helper;
using Adaya.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Adaya.Controllers
{
	public class UmurController : Controller
	{
		private readonly ApplicationDbContext _context;

		public UmurController(ApplicationDbContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			var Umurs = _context.TBUmurs.FromSqlRaw("EXEC dbo.GetAllUmurs").ToList();
			return View(Umurs);
		}
	}
}
