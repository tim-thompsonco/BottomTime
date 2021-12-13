using BottomTimeApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BottomTimeApi.Data {
	public interface IDiveRepository {
		Task AddDiveAsync(Dive dive);
		Task<IEnumerable<Dive>> GetDivesAsync(int pageNumber, int divesPerPage);
		Task<Dive> GetDiveByDiveIdAsync(int id);
		Task UpdateDiveAsync(Dive dive);
		Task DeleteDiveAsync(Dive dive);
	}
}
