using BottomTimeApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BottomTimeApi.Data {
	public class DiveRepository : IDiveRepository {
		private readonly DataContext _context;

		public DiveRepository(DataContext context) {
			_context = context;
		}

		public async Task AddDiveAsync(Dive dive) {
			await _context.Dives.AddAsync(dive);

			await _context.SaveChangesAsync();
		}

		public async Task<Dive> GetDiveByDiveIdAsync(int number) {
			return await _context.Dives.FindAsync(number);
		}

		public async Task<IEnumerable<Dive>> GetDivesAsync(int pageNumber = 1, int divesPerPage = 10) {
			if (pageNumber < 1) {
				throw new InvalidOperationException("The page number cannot be less than 1.");
			} else if (divesPerPage < 1) {
				throw new InvalidOperationException("The dives per page cannot be less than 1.");
			} else if (divesPerPage > 100) {
				throw new InvalidOperationException("The dives per page cannot be greater than 100.");
			}

			return await _context.Dives
				.OrderBy(dive => dive.Id)
				.Skip((pageNumber - 1) * divesPerPage)
				.Take(divesPerPage)
				.ToListAsync();
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
