using BottomTimeApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BottomTimeApi.DataAccess {
	public class DataContext : DbContext {
		public DbSet<Dive> Dives { get; set; }

		public DataContext(DbContextOptions<DataContext> options) : base(options) {
		}

		protected override void OnModelCreating(ModelBuilder builder) {
			base.OnModelCreating(builder);
		}

		public override int SaveChanges() {
			ChangeTracker.DetectChanges();

			return base.SaveChanges();
		}
	}
}