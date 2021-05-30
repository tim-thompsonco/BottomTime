using BottomTimeApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BottomTimeApi.DataAccess {
	public class DiveRepository : IDiveRepository {
		private readonly DataContext _context;

		public DiveRepository(DataContext context) {
			_context = context;
		}

		public async Task AddDiveAsync(Dive dive) {
			await _context.Dives.AddAsync(dive);

			await _context.SaveChangesAsync();
		}

		public async Task<Dive> GetDiveByIdAsync(int id) {
			return await _context.Dives.FindAsync(id);
		}

		public async Task<IEnumerable<Dive>> GetDivesAsync() {
			return await _context.Dives.ToListAsync();
		}

		public async Task UpdateDiveAsync(Dive dive) {
			_context.Update(dive);

			await _context.SaveChangesAsync();
		}

		public async Task DeleteDiveAsync(Dive dive) {
			_context.Remove(dive);

			await _context.SaveChangesAsync();
		}
	}
}
