using BottomTimeApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BottomTimeApi.DataAccess {
	public interface IDiveRepository {
		Task AddDiveAsync(Dive dive);
		Task<IEnumerable<Dive>> GetDivesAsync();
		Task<Dive> GetDiveByDiveNumberAsync(int id);
		Task UpdateDiveAsync(Dive dive);
		Task DeleteDiveAsync(Dive dive);
	}
}
