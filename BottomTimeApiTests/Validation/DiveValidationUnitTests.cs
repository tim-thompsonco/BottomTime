using BottomTimeApi.Models;
using BottomTimeApi.Validation;
using BottomTimeApiTests.Data.MockData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BottomTimeApiTests.Validation {
	[TestClass]
	public class DiveValidationUnitTests {
		[TestMethod]
		public void ValidateDiveFailsWhenDiveNumberTooHighTest() {
			IMockDive mockDive = new MockDiveOne();
			Dive dive = mockDive.GetMockDive();
			dive.Number = 10001;

			try {
				DiveValidator.ValidateDive(dive);
			} catch (Exception ex) {
				Assert.IsTrue(ex.Message is "Dive number is too high. The maximum dive number is 10,000.");
			}
		}

		[TestMethod]
		public void ValidateDiveFailsWhenDiveNumberTooLowTest() {
			IMockDive mockDive = new MockDiveOne();
			Dive dive = mockDive.GetMockDive();
			dive.Number = -1;

			try {
				DiveValidator.ValidateDive(dive);
			} catch (Exception ex) {
				Assert.IsTrue(ex.Message is "Invalid dive number. The dive number must be 1 or higher.");
			}
		}

		[TestMethod]
		public void ValidateDiveFailsWhenDiveLocationEmptyTest() {
			IMockDive mockDive = new MockDiveOne();
			Dive dive = mockDive.GetMockDive();
			dive.Location = string.Empty;

			try {
				DiveValidator.ValidateDive(dive);
			} catch (Exception ex) {
				Assert.IsTrue(ex.Message is "Invalid dive location. Location cannot be empty.");
			}
		}


		[TestMethod]
		public void ValidateDiveFailsWhenDiveLocationMissingTest() {
			IMockDive mockDive = new MockDiveOne();
			Dive dive = mockDive.GetMockDive();
			dive.Location = null;

			try {
				DiveValidator.ValidateDive(dive);
			} catch (Exception ex) {
				Assert.IsTrue(ex.Message is "Invalid dive location. Location cannot be empty.");
			}
		}

		[TestMethod]
		public void ValidateDiveFailsWhenDiveSiteEmptyTest() {
			IMockDive mockDive = new MockDiveOne();
			Dive dive = mockDive.GetMockDive();
			dive.DiveSite = string.Empty;

			try {
				DiveValidator.ValidateDive(dive);
			} catch (Exception ex) {
				Assert.IsTrue(ex.Message is "Invalid dive site. Dive site cannot be empty.");
			}
		}

		[TestMethod]
		public void ValidateDiveFailsWhenDiveSiteMissingTest() {
			IMockDive mockDive = new MockDiveOne();
			Dive dive = mockDive.GetMockDive();
			dive.DiveSite = null;

			try {
				DiveValidator.ValidateDive(dive);
			} catch (Exception ex) {
				Assert.IsTrue(ex.Message is "Invalid dive site. Dive site cannot be empty.");
			}
		}

		[TestMethod]
		public void ValidateDiveFailsWhenDiveStartAirPressureIsZeroTest() {
			IMockDive mockDive = new MockDiveOne();
			Dive dive = mockDive.GetMockDive();
			dive.StartAirPressure = 0;

			try {
				DiveValidator.ValidateDive(dive);
			} catch (Exception ex) {
				Assert.IsTrue(ex.Message is "Invalid dive start air pressure. Start air pressure cannot be 0.");
			}
		}

		[TestMethod]
		public void ValidateDiveFailsWhenDiveStartAirPressureIsNegativeTest() {
			IMockDive mockDive = new MockDiveOne();
			Dive dive = mockDive.GetMockDive();
			dive.StartAirPressure = -1;

			try {
				DiveValidator.ValidateDive(dive);
			} catch (Exception ex) {
				Assert.IsTrue(ex.Message is "Invalid dive start air pressure. Start air pressure cannot be negative.");
			}
		}
	}
}
