using BottomTimeApi.Data;
using BottomTimeApi.Models;
using BottomTimeApiTests.Data.MockData;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BottomTimeApiTests.Data {
	public class DiveRepositoryMock : IDiveRepository {
		public List<Dive> TestDives { get; set; }

		public DiveRepositoryMock() {
			IMockDive testDiveOne = new MockDiveOne();
			IMockDive testDiveTwo = new MockDiveTwo();
			IMockDive testDiveThree = new MockDiveThree();

			TestDives = new List<Dive> {
				testDiveOne.GetMockDive(),
				testDiveTwo.GetMockDive(),
				testDiveThree.GetMockDive()
			};
		}

		public Task AddDiveAsync(Dive dive) {
			TestDives.Add(dive);

			return Task.CompletedTask;
		}

		public Task DeleteDiveAsync(Dive dive) {
			TestDives.Remove(dive);

			return Task.CompletedTask;
		}

		public Task<Dive> GetDiveByDiveIdAsync(int id) {
			return Task.FromResult(TestDives.FirstOrDefault(d => d.Id == id));
		}

		public async Task<IEnumerable<Dive>> GetDivesAsync() {
			return TestDives;
		}

		public Task UpdateDiveAsync(Dive dive) {
			int indexDiveToUpdate = TestDives.FindIndex(d => d.Id == dive.Id);

			if (indexDiveToUpdate == -1) {
				throw new DBConcurrencyException("Expected to modify 1 record but modified 0 records.");
			}

			TestDives[indexDiveToUpdate] = dive;

			return Task.CompletedTask;
		}
	}
}
