using BottomTimeApi.Controllers;
using BottomTimeApi.Models;
using BottomTimeApiTests.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
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
		public async Task GetDiveByIdUnitTestAsync() {
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
	}
}
