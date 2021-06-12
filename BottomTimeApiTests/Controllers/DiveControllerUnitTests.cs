﻿using AutoMapper;
using BottomTimeApi;
using BottomTimeApi.Controllers;
using BottomTimeApi.Models;
using BottomTimeApiTests.Data;
using BottomTimeApiTests.Data.MockData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BottomTimeApiTests.Controllers {
	[TestClass]
	public class DiveControllerUnitTests {
		private static IMapper _mapper;

		public DiveControllerUnitTests() {
			if (_mapper == null) {
				MapperConfiguration mappingConfig = new(config => {
					config.AddProfile(new MappingProfile());
				});
				_mapper = mappingConfig.CreateMapper();
			}
		}

		[TestMethod]
		public async Task GetDivesUnitTestAsync() {
			DiveRepositoryMock repository = new();
			DiveController controller = new(repository, _mapper);

			ActionResult<List<Dive>> testActionResult = await controller.GetDivesAsync();
			OkObjectResult testResponse = testActionResult.Result as OkObjectResult;
			List<Dive> testValues = testResponse.Value as List<Dive>;

			Assert.IsNotNull(testResponse);
			Assert.AreEqual(3, testValues.Count);
			Assert.AreEqual(repository.TestDives[0].Id, testValues[0].Id);
			Assert.AreEqual(repository.TestDives[0].DiveSite, testValues[0].DiveSite);
			Assert.AreEqual(repository.TestDives[1].Id, testValues[1].Id);
			Assert.AreEqual(repository.TestDives[1].DiveSite, testValues[1].DiveSite);
		}

		[TestMethod]
		public async Task GetDiveByDiveIdUnitTestSucceedsAsync() {
			DiveRepositoryMock repository = new();
			DiveController controller = new(repository, _mapper);
			const int diveId = 342;

			ActionResult<Dive> testActionResult = await controller.GetDiveByDiveIdAsync(diveId);
			OkObjectResult testResponse = testActionResult.Result as OkObjectResult;
			Dive testValue = testResponse.Value as Dive;

			Assert.IsNotNull(testResponse);
			Assert.IsTrue(testValue.Id is 342);
			Assert.IsTrue(testValue.DiveSite is "Underwater Island");
		}

		[TestMethod]
		public async Task GetDiveByDiveIdUnitTestFailsAsync() {
			DiveRepositoryMock repository = new();
			DiveController controller = new(repository, _mapper);
			const int nonExistentDiveId = 789;

			ActionResult<Dive> testActionResult = await controller.GetDiveByDiveIdAsync(nonExistentDiveId);

			Assert.IsTrue(testActionResult.Result is NotFoundResult);
		}

		[TestMethod]
		public async Task AddDiveUnitTestAsync() {
			DiveRepositoryMock repository = new();
			DiveController controller = new(repository, _mapper);
			IMockDive mockDive = new MockDiveTwo();
			Dive dive = mockDive.GetMockDive();
			DiveDto diveDto = _mapper.Map<DiveDto>(dive);

			ActionResult<Dive> testActionResult = await controller.AddDiveAsync(diveDto);
			CreatedAtRouteResult testResponse = testActionResult.Result as CreatedAtRouteResult;
			Dive testValue = testResponse.Value as Dive;

			Assert.IsNotNull(testResponse);
			Assert.AreEqual(repository.TestDives[3].Id, testValue.Id);
			Assert.AreEqual(repository.TestDives[3].DiveSite, testValue.DiveSite);
		}

		[TestMethod]
		public async Task AddDiveUnitTestFailsValidationAsync() {
			DiveRepositoryMock repository = new();
			DiveController controller = new(repository, _mapper);
			DiveDto diveDto = new() { DiveSite = "A third dive site", Number = -1 };

			ActionResult<Dive> testActionResult = await controller.AddDiveAsync(diveDto);
			BadRequestObjectResult testActionBadRequestResult = testActionResult.Result as BadRequestObjectResult;

			Assert.IsTrue(testActionResult.Result is BadRequestObjectResult);
			Assert.IsTrue(testActionBadRequestResult.Value is "Invalid dive number. The dive number must be 1 or higher.");
		}

		[TestMethod]
		public async Task UpdateDiveUnitTestSucceedsAsync() {
			DiveRepositoryMock repository = new();
			DiveController controller = new(repository, _mapper);
			IMockDive mockDive = new MockDiveTwo();
			Dive updatedDive = mockDive.GetMockDive();
			updatedDive.DiveSite = "Not Underwater Island";

			ActionResult<Dive> testActionResult = await controller.UpdateDiveAsync(updatedDive.Id, updatedDive);

			Assert.IsTrue(testActionResult.Result is NoContentResult);
			Assert.AreEqual(repository.TestDives[1].Id, updatedDive.Id);
			Assert.AreEqual(repository.TestDives[1].DiveSite, updatedDive.DiveSite);
		}

		[TestMethod]
		public async Task UpdateDiveUnitTestFailsAsync() {
			DiveRepositoryMock repository = new();
			DiveController controller = new(repository, _mapper);
			Dive updatedDive = new() { Id = 342, DiveSite = "Not Underwater Island" };
			const int notMatchingId = 343;

			try {
				ActionResult<Dive> testActionResult = await controller.UpdateDiveAsync(notMatchingId, updatedDive);
			} catch (DBConcurrencyException ex) {
				Assert.IsTrue(ex.Message is "Expected to modify 1 record but modified 0 records.");
			}
		}

		[TestMethod]
		public async Task UpdateDiveUnitTestFailsValidationAsync() {
			DiveRepositoryMock repository = new();
			DiveController controller = new(repository, _mapper);
			Dive updatedDive = new() {
				Id = 342,
				DiveSite = "Not Underwater Island",
				Number = 10001
			};

			ActionResult<Dive> testActionResult = await controller.UpdateDiveAsync(updatedDive.Id, updatedDive);
			BadRequestObjectResult testActionBadRequestResult = testActionResult.Result as BadRequestObjectResult;

			Assert.IsTrue(testActionResult.Result is BadRequestObjectResult);
			Assert.IsTrue(testActionBadRequestResult.Value is "Dive number is too high. The maximum dive number is 10,000.");
		}

		[TestMethod]
		public async Task DeleteDiveUnitTestSucceedsAsync() {
			DiveRepositoryMock repository = new();
			DiveController controller = new(repository, _mapper);
			const int IdToDelete = 342;

			ActionResult<Dive> testActionResult = await controller.DeleteDiveByDiveId(IdToDelete);

			Assert.IsTrue(testActionResult.Result is NoContentResult);
			Assert.IsTrue(repository.TestDives.Count is 2);
			Assert.IsTrue(repository.TestDives[0].Id != IdToDelete);
		}

		[TestMethod]
		public async Task DeleteDiveUnitTestFailsAsync() {
			DiveRepositoryMock repository = new();
			DiveController controller = new(repository, _mapper);
			const int nonExistentId = 345;

			ActionResult<Dive> testActionResult = await controller.DeleteDiveByDiveId(nonExistentId);

			Assert.IsTrue(testActionResult.Result is NotFoundResult);
		}
	}
}
