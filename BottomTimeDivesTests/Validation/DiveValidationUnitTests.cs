using BottomTimeDives.Models;
using BottomTimeDives.Validation;
using BottomTimeDivesTests.Data.MockData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BottomTimeDivesTests.Validation {
	[TestClass]
	public class DiveValidationUnitTests {
		[TestMethod]
		public void ValidateDiveSucceedsWhenDiveDateIsNowTest() {
			Dive dive = new MockDive {
				Date = DateTime.UtcNow
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
		public void ValidateDiveSucceedsWhenDiveDateIsInPastTest() {
			Dive dive = new MockDive {
				Date = DateTime.UtcNow.AddDays(-1)
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
		public void ValidateDiveFailsWhenDiveDateIsInFutureTest() {
			Dive dive = new MockDive {
				Date = DateTime.UtcNow.AddDays(1)
			};
			Exception exception = null;

			try {
				DiveValidator.ValidateDive(dive);
			} catch (Exception ex) {
				exception = ex;
			}

			Assert.IsNotNull(exception);
			Assert.IsTrue(exception is InvalidOperationException);
			Assert.AreEqual("Dive date cannot be a date in the future.", exception.Message);
		}

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
			Assert.AreEqual("Dive number is too high. The maximum dive number is 10,000.", exception.Message);
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
			Assert.AreEqual("Invalid dive number. The dive number must be 1 or higher.", exception.Message);
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
			Assert.AreEqual("Invalid dive start air pressure. Start air pressure cannot be 0.", exception.Message);
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
			Assert.AreEqual("Invalid dive start air pressure. Start air pressure cannot be negative.", exception.Message);
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
			Assert.AreEqual("Invalid dive end air pressure. End air pressure cannot be negative.", exception.Message);
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
			Assert.AreEqual("Invalid dive end air pressure. End air pressure cannot be greater than start air pressure.", exception.Message);
		}

		[TestMethod]
		public void ValidateDiveSucceedsWhenDiveDrySuitNumOfLinersIsZeroTest() {
			Dive dive = new MockDive {
				DrySuitNumOfLiners = 0
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
		public void ValidateDiveSucceedsWhenDiveDrySuitNumOfLinersIsPositiveTest() {
			Dive dive = new MockDive {
				DrySuitNumOfLiners = 1
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
		public void ValidateDiveFailsWhenDiveDrySuitNumOfLinersIsNegativeTest() {
			Dive dive = new MockDive {
				DrySuitNumOfLiners = -1
			};
			Exception exception = null;

			try {
				DiveValidator.ValidateDive(dive);
			} catch (Exception ex) {
				exception = ex;
			}

			Assert.IsNotNull(exception);
			Assert.IsTrue(exception is InvalidOperationException);
			Assert.AreEqual("Invalid DrySuitNumOfLiners value. DrySuitNumOfLiners cannot be negative.", exception.Message);
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
			Assert.AreEqual("Invalid dive max depth. Max depth cannot be negative.", exception.Message);
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
			Assert.AreEqual("Invalid dive average depth. Average depth cannot be negative.", exception.Message);
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
			Assert.AreEqual("Invalid dive average depth. Average depth cannot be greater than max depth.", exception.Message);
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
			Assert.AreEqual("Invalid WaterTemperature value. WaterTemperature cannot be negative.", exception.Message);
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
			Assert.AreEqual("Invalid Visibility value. Visibility cannot be negative.", exception.Message);
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
			Assert.AreEqual("Invalid Weight value. Weight cannot be negative.", exception.Message);
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
			Assert.AreEqual("Invalid TankSize value. TankSize cannot be negative.", exception.Message);
		}

		[TestMethod]
		public void ValidateDiveSucceedsWhenTimeSpanIsZeroTest() {
			Dive dive = new MockDive {
				SurfaceIntervalTime = new TimeSpan(0, 0, 0, 0)
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
		public void ValidateDiveSucceedsWhenSurfaceIntervalTimeIsValidTest() {
			Dive dive = new MockDive {
				SurfaceIntervalTime = new TimeSpan(1, 0, 0, 0)
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
		public void ValidateDiveFailsWhenSurfaceIntervalTimeIsNotValidTest() {
			Dive dive = new MockDive {
				SurfaceIntervalTime = new TimeSpan(-1, 0, 0, 0)
			};
			Exception exception = null;

			try {
				DiveValidator.ValidateDive(dive);
			} catch (Exception ex) {
				exception = ex;
			}

			Assert.IsNotNull(exception);
			Assert.IsTrue(exception is InvalidOperationException);
			Assert.AreEqual("Invalid SurfaceIntervalTime value. Days cannot be negative.", exception.Message);
		}

		[TestMethod]
		public void ValidateDiveSucceedsWhenBottomTimeIsValidTest() {
			Dive dive = new MockDive {
				BottomTime = new TimeSpan(0, 1, 0, 0)
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
		public void ValidateDiveFailsWhenBottomTimeIsNotValidTest() {
			Dive dive = new MockDive {
				BottomTime = new TimeSpan(0, -1, 0, 0)
			};
			Exception exception = null;

			try {
				DiveValidator.ValidateDive(dive);
			} catch (Exception ex) {
				exception = ex;
			}

			Assert.IsNotNull(exception);
			Assert.IsTrue(exception is InvalidOperationException);
			Assert.AreEqual("Invalid BottomTime value. Hours cannot be negative.", exception.Message);
		}

		[TestMethod]
		public void ValidateDiveSucceedsWhenSafetyStopTimeIsValidTest() {
			Dive dive = new MockDive {
				SafetyStopTime = new TimeSpan(0, 0, 3, 0)
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
		public void ValidateDiveFailsWhenSafetyStopTimeIsNotValidTest() {
			Dive dive = new MockDive {
				SafetyStopTime = new TimeSpan(0, 0, -3, 0)
			};
			Exception exception = null;

			try {
				DiveValidator.ValidateDive(dive);
			} catch (Exception ex) {
				exception = ex;
			}

			Assert.IsNotNull(exception);
			Assert.IsTrue(exception is InvalidOperationException);
			Assert.AreEqual("Invalid SafetyStopTime value. Minutes cannot be negative.", exception.Message);
		}

		[TestMethod]
		public void ValidateDiveSucceedsWhenResidualNitrogenTimeIsValidTest() {
			Dive dive = new MockDive {
				ResidualNitrogenTime = new TimeSpan(0, 0, 0, 10)
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
		public void ValidateDiveFailsWhenResidualNitrogenTimeIsNotValidTest() {
			Dive dive = new MockDive {
				ResidualNitrogenTime = new TimeSpan(0, 0, 0, -10)
			};
			Exception exception = null;

			try {
				DiveValidator.ValidateDive(dive);
			} catch (Exception ex) {
				exception = ex;
			}

			Assert.IsNotNull(exception);
			Assert.IsTrue(exception is InvalidOperationException);
			Assert.AreEqual("Invalid ResidualNitrogenTime value. Seconds cannot be negative.", exception.Message);
		}

		[TestMethod]
		public void ValidateDiveSucceedsWhenAbsoluteBottomTimeIsValidTest() {
			Dive dive = new MockDive {
				AbsoluteBottomTime = new TimeSpan(0, 0, 10, 0)
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
		public void ValidateDiveFailsWhenAbsoluteBottomTimeIsNotValidTest() {
			Dive dive = new MockDive {
				AbsoluteBottomTime = new TimeSpan(0, 0, -10, 0)
			};
			Exception exception = null;

			try {
				DiveValidator.ValidateDive(dive);
			} catch (Exception ex) {
				exception = ex;
			}

			Assert.IsNotNull(exception);
			Assert.IsTrue(exception is InvalidOperationException);
			Assert.AreEqual("Invalid AbsoluteBottomTime value. Minutes cannot be negative.", exception.Message);
		}

		[TestMethod]
		public void ValidateDiveSucceedsWhenTotalBottomTimeIsValidTest() {
			Dive dive = new MockDive {
				TotalBottomTime = new TimeSpan(0, 1, 0, 0)
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
		public void ValidateDiveFailsWhenTotalBottomTimeIsNotValidTest() {
			Dive dive = new MockDive {
				TotalBottomTime = new TimeSpan(0, -1, 0, 0)
			};
			Exception exception = null;

			try {
				DiveValidator.ValidateDive(dive);
			} catch (Exception ex) {
				exception = ex;
			}

			Assert.IsNotNull(exception);
			Assert.IsTrue(exception is InvalidOperationException);
			Assert.AreEqual("Invalid TotalBottomTime value. Hours cannot be negative.", exception.Message);
		}
	}
}
