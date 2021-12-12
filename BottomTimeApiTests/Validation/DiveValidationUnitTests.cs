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
			Dive dive = new MockDive {
				Number = 10001
			};
			Exception exception = null;

			try {
				DiveValidator.ValidateDive(dive);
			} catch (Exception ex) {
				exception = ex;
			}

			Assert.IsNotNull(exception);
			Assert.IsTrue(exception is InvalidOperationException);
			Assert.IsTrue(exception.Message is "Dive number is too high. The maximum dive number is 10,000.");
		}

		[TestMethod]
		public void ValidateDiveFailsWhenDiveNumberTooLowTest() {
			Dive dive = new MockDive {
				Number = -1
			};
			Exception exception = null;

			try {
				DiveValidator.ValidateDive(dive);
			} catch (Exception ex) {
				exception = ex;
			}

			Assert.IsNotNull(exception);
			Assert.IsTrue(exception is InvalidOperationException);
			Assert.IsTrue(exception.Message is "Invalid dive number. The dive number must be 1 or higher.");
		}

		[TestMethod]
		public void ValidateDiveFailsWhenDiveStartAirPressureIsZeroTest() {
			Dive dive = new MockDive {
				StartAirPressure = 0
			};
			Exception exception = null;

			try {
				DiveValidator.ValidateDive(dive);
			} catch (Exception ex) {
				exception = ex;
			}

			Assert.IsNotNull(exception);
			Assert.IsTrue(exception is InvalidOperationException);
			Assert.IsTrue(exception.Message is "Invalid dive start air pressure. Start air pressure cannot be 0.");
		}

		[TestMethod]
		public void ValidateDiveFailsWhenDiveStartAirPressureIsNegativeTest() {
			Dive dive = new MockDive {
				StartAirPressure = -1
			};
			Exception exception = null;

			try {
				DiveValidator.ValidateDive(dive);
			} catch (Exception ex) {
				exception = ex;
			}

			Assert.IsNotNull(exception);
			Assert.IsTrue(exception is InvalidOperationException);
			Assert.IsTrue(exception.Message is "Invalid dive start air pressure. Start air pressure cannot be negative.");
		}

		[TestMethod]
		public void ValidateDiveFailsWhenDiveEndAirPressureIsNegativeTest() {
			Dive dive = new MockDive {
				EndAirPressure = -1
			};
			Exception exception = null;

			try {
				DiveValidator.ValidateDive(dive);
			} catch (Exception ex) {
				exception = ex;
			}

			Assert.IsNotNull(exception);
			Assert.IsTrue(exception is InvalidOperationException);
			Assert.IsTrue(exception.Message is "Invalid dive end air pressure. End air pressure cannot be negative.");
		}

		[TestMethod]
		public void ValidateDiveFailsWhenDiveMaxDepthIsNegativeTest() {
			Dive dive = new MockDive {
				MaxDepth = -1
			};
			Exception exception = null;

			try {
				DiveValidator.ValidateDive(dive);
			} catch (Exception ex) {
				exception = ex;
			}

			Assert.IsNotNull(exception);
			Assert.IsTrue(exception is InvalidOperationException);
			Assert.IsTrue(exception.Message is "Invalid dive max depth. Max depth cannot be negative.");
		}

		[TestMethod]
		public void ValidateDiveFailsWhenDiveAvgDepthIsNegativeTest() {
			Dive dive = new MockDive {
				AvgDepth = -1
			};
			Exception exception = null;

			try {
				DiveValidator.ValidateDive(dive);
			} catch (Exception ex) {
				exception = ex;
			}

			Assert.IsNotNull(exception);
			Assert.IsTrue(exception is InvalidOperationException);
			Assert.IsTrue(exception.Message is "Invalid dive average depth. Average depth cannot be negative.");
		}

		[TestMethod]
		public void ValidateDiveFailsWhenDiveAvgDepthIsGreaterThanMaxDepthTest() {
			Dive dive = new MockDive {
				AvgDepth = 45,
				MaxDepth = 40
			};
			Exception exception = null;

			try {
				DiveValidator.ValidateDive(dive);
			} catch (Exception ex) {
				exception = ex;
			}

			Assert.IsNotNull(exception);
			Assert.IsTrue(exception is InvalidOperationException);
			Assert.IsTrue(exception.Message is "Invalid dive average depth. Average depth cannot be greater than max depth.");
		}

		[TestMethod]
		public void ValidateDiveFailsWhenDiveWaterTemperatureIsNegativeTest() {
			Dive dive = new MockDive {
				WaterTemperature = -1
			};
			Exception exception = null;

			try {
				DiveValidator.ValidateDive(dive);
			} catch (Exception ex) {
				exception = ex;
			}

			Assert.IsNotNull(exception);
			Assert.IsTrue(exception is InvalidOperationException);
			Assert.IsTrue(exception.Message is "Invalid water temperature. Water temperature cannot be negative.");
		}

		[TestMethod]
		public void ValidateDiveFailsWhenDiveVisibilityIsNegativeTest() {
			Dive dive = new MockDive {
				Visibility = -1
			};
			Exception exception = null;

			try {
				DiveValidator.ValidateDive(dive);
			} catch (Exception ex) {
				exception = ex;
			}

			Assert.IsNotNull(exception);
			Assert.IsTrue(exception is InvalidOperationException);
			Assert.IsTrue(exception.Message is "Invalid dive visibility. Visibility cannot be negative.");
		}

		[TestMethod]
		public void ValidateDiveFailsWhenDiveWeightIsNegativeTest() {
			Dive dive = new MockDive {
				Weight = -1
			};
			Exception exception = null;

			try {
				DiveValidator.ValidateDive(dive);
			} catch (Exception ex) {
				exception = ex;
			}

			Assert.IsNotNull(exception);
			Assert.IsTrue(exception is InvalidOperationException);
			Assert.IsTrue(exception.Message is "Invalid dive weight. Weight cannot be negative.");
		}

		[TestMethod]
		public void ValidateDiveFailsWhenDiveTankSizeIsNegativeTest() {
			Dive dive = new MockDive {
				TankSize = -1
			};
			Exception exception = null;

			try {
				DiveValidator.ValidateDive(dive);
			} catch (Exception ex) {
				exception = ex;
			}

			Assert.IsNotNull(exception);
			Assert.IsTrue(exception is InvalidOperationException);
			Assert.IsTrue(exception.Message is "Invalid tank size. Tank size cannot be negative.");
		}
	}
}
