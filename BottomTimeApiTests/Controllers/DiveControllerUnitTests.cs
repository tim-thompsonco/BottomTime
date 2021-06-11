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
			Assert.AreEqual(repository.TestDives[0].Number, testValues[0].Number);
			Assert.AreEqual(repository.TestDives[0].DiveSite, testValues[0].DiveSite);
			Assert.AreEqual(repository.TestDives[1].Number, testValues[1].Number);
			Assert.AreEqual(repository.TestDives[1].DiveSite, testValues[1].DiveSite);
		}

		[TestMethod]
		public async Task GetDiveByDiveNumberUnitTestSucceedsAsync() {
			DiveRepositoryMock repository = new DiveRepositoryMock();
			DiveController controller = new DiveController(repository);
			const int diveNumber = 2;

			ActionResult<Dive> testActionResult = await controller.GetDiveByDiveNumberAsync(diveNumber);
			OkObjectResult testResponse = testActionResult.Result as OkObjectResult;
			Dive testValue = testResponse.Value as Dive;

			Assert.IsNotNull(testResponse);
			Assert.IsTrue(testValue.Number is 2);
			Assert.IsTrue(testValue.DiveSite is "Test site two");
		}

		[TestMethod]
		public async Task GetDiveByDiveNumberUnitTestFailsAsync() {
			DiveRepositoryMock repository = new DiveRepositoryMock();
			DiveController controller = new DiveController(repository);
			const int nonExistentDiveNumber = 5;

			ActionResult<Dive> testActionResult = await controller.GetDiveByDiveNumberAsync(nonExistentDiveNumber);

			Assert.IsTrue(testActionResult.Result is NotFoundResult);
		}

		[TestMethod]
		public async Task AddDiveUnitTestAsync() {
			DiveRepositoryMock repository = new DiveRepositoryMock();
			DiveController controller = new DiveController(repository);
			Dive dive = new Dive { Number = 3, DiveSite = "A third dive site" };

			ActionResult<Dive> testActionResult = await controller.AddDiveAsync(dive);
			CreatedAtActionResult testResponse = testActionResult.Result as CreatedAtActionResult;
			Dive testValue = testResponse.Value as Dive;

			Assert.IsNotNull(testResponse);
			Assert.AreEqual(repository.TestDives[2].Number, testValue.Number);
			Assert.AreEqual(repository.TestDives[2].DiveSite, testValue.DiveSite);
		}

		[TestMethod]
		public async Task UpdateDiveUnitTestSucceedsAsync() {
			DiveRepositoryMock repository = new DiveRepositoryMock();
			DiveController controller = new DiveController(repository);
			Dive updatedDive = new Dive { Number = 2, DiveSite = "Updated dive site" };

			ActionResult<Dive> testActionResult = await controller.UpdateDiveAsync(updatedDive.Number, updatedDive);

			Assert.IsTrue(testActionResult.Result is NoContentResult);
			Assert.AreEqual(repository.TestDives[1].Number, updatedDive.Number);
			Assert.AreEqual(repository.TestDives[1].DiveSite, updatedDive.DiveSite);
		}

		[TestMethod]
		public async Task UpdateDiveUnitTestFailsAsync() {
			DiveRepositoryMock repository = new DiveRepositoryMock();
			DiveController controller = new DiveController(repository);
			Dive updatedDive = new Dive { Number = 2, DiveSite = "Updated dive site" };
			const int notMatchingNumber = 3;

			try {
				ActionResult<Dive> testActionResult = await controller.UpdateDiveAsync(notMatchingNumber, updatedDive);
			} catch (DBConcurrencyException ex) {
				Assert.IsTrue(ex.Message is "Expected to modify 1 record but modified 0 records.");
			}
		}

		[TestMethod]
		public async Task DeleteDiveUnitTestSucceedsAsync() {
			DiveRepositoryMock repository = new DiveRepositoryMock();
			DiveController controller = new DiveController(repository);
			const int numberToDelete = 2;

			ActionResult<Dive> testActionResult = await controller.DeleteDiveByNumberAsync(numberToDelete);

			Assert.IsTrue(testActionResult.Result is NoContentResult);
			Assert.IsTrue(repository.TestDives.Count is 1);
			Assert.IsTrue(repository.TestDives[0].Number != numberToDelete);
		}

		[TestMethod]
		public async Task DeleteDiveUnitTestFailsAsync() {
			DiveRepositoryMock repository = new DiveRepositoryMock();
			DiveController controller = new DiveController(repository);
			const int nonExistentNumber = 5;

			ActionResult<Dive> testActionResult = await controller.DeleteDiveByNumberAsync(nonExistentNumber);

			Assert.IsTrue(testActionResult.Result is NotFoundResult);
		}
	}
}
