using BottomTimeApi.DataAccess;
using BottomTimeApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BottomTimeApiTests.DataAccess {
	public class DiveRepositoryMock : IDiveRepository {
		public List<Dive> TestDives { get; set; }

		public DiveRepositoryMock() {
			TestDives = new List<Dive> {
				new Dive{Id=1, DiveSite = "Test site one"},
				new Dive{Id=2, DiveSite = "Test site two"}
			};
		}

		public Task AddDiveAsync(Dive dive) {
			throw new NotImplementedException();
		}

		public Task DeleteDiveAsync(Dive dive) {
			throw new NotImplementedException();
		}

		public Task<Dive> GetDiveByIdAsync(int id) {
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<Dive>> GetDivesAsync() {
			return TestDives;
		}

		public Task UpdateDiveAsync(Dive dive) {
			throw new NotImplementedException();
		}
	}
}
