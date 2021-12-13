using BottomTimeApi.Models;
using BottomTimeApi.Validation;
using BottomTimeApiTests.Data.MockData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BottomTimeApiTests.Validation {
	[TestClass]
	public class DiveValidationUnitTests {
		[TestMethod]
		public void ValidateDiveSucceedsWhenDiveNumberAtMaxValueTest() {
			Dive dive = new MockDive {
				Number = 10000
			};
			Exception exception = null;

			try {
				DiveValidator.ValidateDive(dive);
			} catch (Exception ex) {
				exception = ex;
			}

			Assert.IsNull(exception);
		}

		[TestMethod]
		public void ValidateDiveSucceedsWhenDiveNumberInValidRangeTest() {
			Dive dive = new MockDive {
				Number = 500
			};
			Exception exception = null;

			try {
				DiveValidator.ValidateDive(dive);
			} catch (Exception ex) {
				exception = ex;
			}

			Assert.IsNull(exception);
		}

		[TestMethod]
		public void ValidateDiveSucceedsWhenDiveNumberAtMinValueTest() {
			Dive dive = new MockDive {
				Number = 1
			};
			Exception exception = null;

			try {
				DiveValidator.ValidateDive(dive);
			} catch (Exception ex) {
				exception = ex;
			}

			Assert.IsNull(exception);
		}

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
				Number = 0
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
		public void ValidateDiveSucceedsWhenDiveStartAirPressureIsGreaterThanZeroTest() {
			Dive dive = new MockDive {
				EndAirPressure = 1,
				StartAirPressure = 1
			};
			Exception exception = null;

			try {
				DiveValidator.ValidateDive(dive);
			} catch (Exception ex) {
				exception = ex;
			}

			Assert.IsNull(exception);
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
		public void ValidateDiveSucceedsWhenDiveEndAirPressureIsZeroTest() {
			Dive dive = new MockDive {
				EndAirPressure = 0
			};
			Exception exception = null;

			try {
				DiveValidator.ValidateDive(dive);
			} catch (Exception ex) {
				exception = ex;
			}

			Assert.IsNull(exception);
		}

		[TestMethod]
		public void ValidateDiveSucceedsWhenDiveEndAirPressureIsPositiveTest() {
			Dive dive = new MockDive {
				EndAirPressure = 1
			};
			Exception exception = null;

			try {
				DiveValidator.ValidateDive(dive);
			} catch (Exception ex) {
				exception = ex;
			}

			Assert.IsNull(exception);
		}

		[TestMethod]
		public void ValidateDiveSucceedsWhenDiveEndAirPressureIsLessThanStartAirPressureTest() {
			Dive dive = new MockDive {
				EndAirPressure = 1000,
				StartAirPressure = 2000
			};
			Exception exception = null;

			try {
				DiveValidator.ValidateDive(dive);
			} catch (Exception ex) {
				exception = ex;
			}

			Assert.IsNull(exception);
		}

		[TestMethod]
		public void ValidateDiveSucceedsWhenDiveEndAirPressureIsEqualToStartAirPressureTest() {
			Dive dive = new MockDive {
				EndAirPressure = 1000,
				StartAirPressure = 1000
			};
			Exception exception = null;

			try {
				DiveValidator.ValidateDive(dive);
			} catch (Exception ex) {
				exception = ex;
			}

			Assert.IsNull(exception);
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
		public void ValidateDiveFailsWhenDiveEndAirPressureIsGreaterThanStartAirPressureTest() {
			Dive dive = new MockDive {
				EndAirPressure = 1200,
				StartAirPressure = 1000
			};
			Exception exception = null;

			try {
				DiveValidator.ValidateDive(dive);
			} catch (Exception ex) {
				exception = ex;
			}

			Assert.IsNotNull(exception);
			Assert.IsTrue(exception is InvalidOperationException);
			Assert.IsTrue(exception.Message is "Invalid dive end air pressure. End air pressure cannot be greater than start air pressure.");
		}

		[TestMethod]
		public void ValidateDiveSucceedsWhenDiveMaxDepthIsZeroTest() {
			Dive dive = new MockDive {
				AvgDepth = 0,
				MaxDepth = 0
			};
			Exception exception = null;

			try {
				DiveValidator.ValidateDive(dive);
			} catch (Exception ex) {
				exception = ex;
			}

			Assert.IsNull(exception);
		}

		[TestMethod]
		public void ValidateDiveSucceedsWhenDiveMaxDepthIsPositiveTest() {
			Dive dive = new MockDive {
				MaxDepth = 45
			};
			Exception exception = null;

			try {
				DiveValidator.ValidateDive(dive);
			} catch (Exception ex) {
				exception = ex;
			}

			Assert.IsNull(exception);
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
		public void ValidateDiveSucceedsWhenDiveAvgDepthIsZeroTest() {
			Dive dive = new MockDive {
				AvgDepth = 0
			};
			Exception exception = null;

			try {
				DiveValidator.ValidateDive(dive);
			} catch (Exception ex) {
				exception = ex;
			}

			Assert.IsNull(exception);
		}

		[TestMethod]
		public void ValidateDiveSucceedsWhenDiveAvgDepthIsPositiveTest() {
			Dive dive = new MockDive {
				AvgDepth = 30
			};
			Exception exception = null;

			try {
				DiveValidator.ValidateDive(dive);
			} catch (Exception ex) {
				exception = ex;
			}

			Assert.IsNull(exception);
		}

		[TestMethod]
		public void ValidateDiveSucceedsWhenDiveAvgDepthIsLessThanMaxDepthTest() {
			Dive dive = new MockDive {
				AvgDepth = 30,
				MaxDepth = 45
			};
			Exception exception = null;

			try {
				DiveValidator.ValidateDive(dive);
			} catch (Exception ex) {
				exception = ex;
			}

			Assert.IsNull(exception);
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
		public void ValidateDiveSucceedsWhenDiveWaterTemperatureIsZeroTest() {
			Dive dive = new MockDive {
				WaterTemperature = 0
			};
			Exception exception = null;

			try {
				DiveValidator.ValidateDive(dive);
			} catch (Exception ex) {
				exception = ex;
			}

			Assert.IsNull(exception);
		}

		[TestMethod]
		public void ValidateDiveSucceedsWhenDiveWaterTemperatureIsPositiveTest() {
			Dive dive = new MockDive {
				WaterTemperature = 40
			};
			Exception exception = null;

			try {
				DiveValidator.ValidateDive(dive);
			} catch (Exception ex) {
				exception = ex;
			}

			Assert.IsNull(exception);
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
		public void ValidateDiveSucceedsWhenDiveVisibilityIsZeroTest() {
			Dive dive = new MockDive {
				Visibility = 0
			};
			Exception exception = null;

			try {
				DiveValidator.ValidateDive(dive);
			} catch (Exception ex) {
				exception = ex;
			}

			Assert.IsNull(exception);
		}

		[TestMethod]
		public void ValidateDiveSucceedsWhenDiveVisibilityIsPositiveTest() {
			Dive dive = new MockDive {
				Visibility = 20
			};
			Exception exception = null;

			try {
				DiveValidator.ValidateDive(dive);
			} catch (Exception ex) {
				exception = ex;
			}

			Assert.IsNull(exception);
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
		public void ValidateDiveSucceedsWhenDiveWeightIsZeroTest() {
			Dive dive = new MockDive {
				Weight = 0
			};
			Exception exception = null;

			try {
				DiveValidator.ValidateDive(dive);
			} catch (Exception ex) {
				exception = ex;
			}

			Assert.IsNull(exception);
		}

		[TestMethod]
		public void ValidateDiveSucceedsWhenDiveWeightIsPositiveTest() {
			Dive dive = new MockDive {
				Weight = 20
			};
			Exception exception = null;

			try {
				DiveValidator.ValidateDive(dive);
			} catch (Exception ex) {
				exception = ex;
			}

			Assert.IsNull(exception);
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
		public void ValidateDiveSucceedsWhenDiveTankSizeIsZeroTest() {
			Dive dive = new MockDive {
				TankSize = 0
			};
			Exception exception = null;

			try {
				DiveValidator.ValidateDive(dive);
			} catch (Exception ex) {
				exception = ex;
			}

			Assert.IsNull(exception);
		}

		[TestMethod]
		public void ValidateDiveSucceedsWhenDiveTankSizeIsPositiveTest() {
			Dive dive = new MockDive {
				TankSize = 80
			};
			Exception exception = null;

			try {
				DiveValidator.ValidateDive(dive);
			} catch (Exception ex) {
				exception = ex;
			}

			Assert.IsNull(exception);
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
