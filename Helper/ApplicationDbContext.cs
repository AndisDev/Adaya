using Adaya.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Adaya.Helper
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
		: base(options)
		{
		}

		public DbSet<Gender> Genders { get; set; }
		public DbSet<Hobi> Hobis { get; set; }
		public DbSet<Generate> Generates { get; set; }
		public DbSet<TBUmur> TBUmurs { get; set; }
		public DbSet<Personal> Personals { get; set; }
	}
}
