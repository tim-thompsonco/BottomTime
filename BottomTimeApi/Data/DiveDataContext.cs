using BottomTimeApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BottomTimeApi.Data {
	public class DiveDataContext : DbContext {
		public DiveDataContext(DbContextOptions<DiveDataContext> options) : base(options) {
		}

		public DbSet<Dive> Dives { get; set; }
	}
}