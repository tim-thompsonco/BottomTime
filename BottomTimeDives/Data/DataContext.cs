using BottomTimeDives.Models;
using Microsoft.EntityFrameworkCore;

namespace BottomTimeDives.Data {
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