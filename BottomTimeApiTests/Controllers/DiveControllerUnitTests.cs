using BottomTimeApi.Controllers;
using BottomTimeApi.Models;
using BottomTimeApiTests.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BottomTimeApiTests.Controllers {
	[TestClass]
	public class DiveControllerUnitTests {
		[TestMethod]
		public async Task GetDivesUnitTestAsync() {
			DiveRepositoryMock repository = new DiveRepositoryMock();
			DiveController controller = new DiveController(repository);

			ActionResult<List<Dive>> testActionResult = await controller.GetDivesAsync();
			OkObjectResult testResponse = testActionResult.Result as OkObjectResult;
			List<Dive> testValues = testResponse.Value as List<Dive>;

			Assert.IsNotNull(testResponse);
			Assert.AreEqual(2, testValues.Count);
			Assert.AreEqual(repository.TestDives[0].Id, testValues[0].Id);
			Assert.AreEqual(repository.TestDives[0].DiveSite, testValues[0].DiveSite);
			Assert.AreEqual(repository.TestDives[1].Id, testValues[1].Id);
			Assert.AreEqual(repository.TestDives[1].DiveSite, testValues[1].DiveSite);
		}

		[TestMethod]
		public async Task GetDiveByIdUnitTestSucceedsAsync() {
			DiveRepositoryMock repository = new DiveRepositoryMock();
			DiveController controller = new DiveController(repository);
			const int diveId = 2;

			ActionResult<Dive> testActionResult = await controller.GetDiveByIdAsync(diveId);
			OkObjectResult testResponse = testActionResult.Result as OkObjectResult;
			Dive testValue = testResponse.Value as Dive;

			Assert.IsNotNull(testResponse);
			Assert.IsTrue(testValue.Id is 2);
			Assert.IsTrue(testValue.DiveSite is "Test site two");
		}

		[TestMethod]
		public async Task GetDiveByIdUnitTestFailsAsync() {
			DiveRepositoryMock repository = new DiveRepositoryMock();
			DiveController controller = new DiveController(repository);
			const int nonExistentDiveId = 5;

			ActionResult<Dive> testActionResult = await controller.GetDiveByIdAsync(nonExistentDiveId);

			Assert.IsTrue(testActionResult.Result is NotFoundResult);
		}

		[TestMethod]
		public async Task AddDiveUnitTestAsync() {
			DiveRepositoryMock repository = new DiveRepositoryMock();
			DiveController controller = new DiveController(repository);
			Dive dive = new Dive { Id = 3, DiveSite = "A third dive site" };

			ActionResult<Dive> testActionResult = await controller.AddDiveAsync(dive);
			CreatedAtRouteResult testResponse = testActionResult.Result as CreatedAtRouteResult;
			Dive testValue = testResponse.Value as Dive;

			Assert.IsNotNull(testResponse);
			Assert.AreEqual(repository.TestDives[2].Id, testValue.Id);
			Assert.AreEqual(repository.TestDives[2].DiveSite, testValue.DiveSite);
		}

		[TestMethod]
		public async Task UpdateDiveUnitTestSucceedsAsync() {
			DiveRepositoryMock repository = new DiveRepositoryMock();
			DiveController controller = new DiveController(repository);
			Dive updatedDive = new Dive { Id = 2, DiveSite = "Updated dive site" };

			ActionResult<Dive> testActionResult = await controller.UpdateDiveAsync(updatedDive.Id, updatedDive);

			Assert.IsTrue(testActionResult.Result is NoContentResult);
			Assert.AreEqual(repository.TestDives[1].Id, updatedDive.Id);
			Assert.AreEqual(repository.TestDives[1].DiveSite, updatedDive.DiveSite);
		}

		[TestMethod]
		public async Task UpdateDiveUnitTestFailsAsync() {
			DiveRepositoryMock repository = new DiveRepositoryMock();
			DiveController controller = new DiveController(repository);
			Dive updatedDive = new Dive { Id = 2, DiveSite = "Updated dive site" };
			const int notMatchingId = 3;

			try {
				ActionResult<Dive> testActionResult = await controller.UpdateDiveAsync(notMatchingId, updatedDive);
			} catch (DBConcurrencyException ex) {
				Assert.IsTrue(ex.Message is "Expected to modify 1 record but modified 0 records.");
			}
		}

		[TestMethod]
		public async Task DeleteDiveUnitTestSucceedsAsync() {
			DiveRepositoryMock repository = new DiveRepositoryMock();
			DiveController controller = new DiveController(repository);
			const int idToDelete = 2;

			ActionResult<Dive> testActionResult = await controller.DeleteDiveByIdAsync(idToDelete);

			Assert.IsTrue(testActionResult.Result is NoContentResult);
			Assert.IsTrue(repository.TestDives.Count is 1);
			Assert.IsTrue(repository.TestDives[0].Id != idToDelete);
		}

		[TestMethod]
		public async Task DeleteDiveUnitTestFailsAsync() {
			DiveRepositoryMock repository = new DiveRepositoryMock();
			DiveController controller = new DiveController(repository);
			const int nonExistentDiveId = 5;

			ActionResult<Dive> testActionResult = await controller.DeleteDiveByIdAsync(nonExistentDiveId);

			Assert.IsTrue(testActionResult.Result is NotFoundResult);
		}
	}
}
