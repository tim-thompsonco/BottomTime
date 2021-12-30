using BottomTimeDives.Models;
using System;

namespace BottomTimeDives.Validation {
	public class DiveValidator {
		public static void ValidateDive(Dive dive) {
			ValidateDiveDate(dive.DiveStartTime, nameof(dive.DiveStartTime));
			ValidateDiveDate(dive.DiveEndTime, nameof(dive.DiveEndTime));
			ValidateDiveStartAndEndTime(dive);
			ValidateDiveNumber(dive);
			ValidateDiveStartAirPressure(dive);
			ValidateDiveEndAirPressure(dive);
			ValidateDiveMaxDepth(dive);
			ValidateDiveAvgDepth(dive);
			ValidateDiveTimeSpan(dive.SafetyStopTime, nameof(dive.SafetyStopTime));
			ValidateDiveTimeSpan(dive.ResidualNitrogenTime, nameof(dive.ResidualNitrogenTime));
			ValidateDiveTimeSpan(dive.AbsoluteBottomTime, nameof(dive.AbsoluteBottomTime));
			ValidateDiveTimeSpan(dive.TotalBottomTime, nameof(dive.TotalBottomTime));
			ValidateDivePropertyIsNotNegative(dive.WaterTemperature, nameof(dive.WaterTemperature));
			ValidateDivePropertyIsNotNegative(dive.DrySuitNumOfLiners, nameof(dive.DrySuitNumOfLiners));
			ValidateDivePropertyIsNotNegative(dive.Visibility, nameof(dive.Visibility));
			ValidateDivePropertyIsNotNegative(dive.Weight, nameof(dive.Weight));
			ValidateDivePropertyIsNotNegative(dive.TankSize, nameof(dive.TankSize));
		}

		private static void ValidateDiveDate(DateTime diveDate, string diveDateProperty) {
			if (diveDate > DateTime.UtcNow) {
				throw new InvalidOperationException($"Invalid {diveDateProperty}. Date cannot be a date in the future.");
			}
		}

		private static void ValidateDiveStartAndEndTime(Dive dive) {
			if (dive.DiveStartTime > dive.DiveEndTime) {
				throw new InvalidOperationException($"Invalid dive start time. Dive start time cannot be after dive end time.");
			}
		}

		private static void ValidateDiveNumber(Dive dive) {
			if (dive.Number > 10000) {
				throw new InvalidOperationException("Dive number is too high. The maximum dive number is 10,000.");
			}

			if (dive.Number < 1) {
				throw new InvalidOperationException("Invalid dive number. The dive number must be 1 or higher.");
			}
		}

		private static void ValidateDiveStartAirPressure(Dive dive) {
			if (dive.StartAirPressure == 0) {
				throw new InvalidOperationException("Invalid dive start air pressure. Start air pressure cannot be 0.");
			}

			if (dive.StartAirPressure < 0) {
				throw new InvalidOperationException("Invalid dive start air pressure. Start air pressure cannot be negative.");
			}
		}

		private static void ValidateDiveEndAirPressure(Dive dive) {
			if (dive.EndAirPressure < 0) {
				throw new InvalidOperationException("Invalid dive end air pressure. End air pressure cannot be negative.");
			} else if (dive.EndAirPressure > dive.StartAirPressure) {
				throw new InvalidOperationException("Invalid dive end air pressure. End air pressure cannot be greater than start air pressure.");
			}
		}

		private static void ValidateDiveAvgDepth(Dive dive) {
			if (dive.AvgDepth < 0) {
				throw new InvalidOperationException("Invalid dive average depth. Average depth cannot be negative.");
			} else if (dive.AvgDepth > dive.MaxDepth) {
				throw new InvalidOperationException("Invalid dive average depth. Average depth cannot be greater than max depth.");
			}
		}

		private static void ValidateDiveMaxDepth(Dive dive) {
			if (dive.MaxDepth < 0) {
				throw new InvalidOperationException("Invalid dive max depth. Max depth cannot be negative.");
			}
		}

		private static void ValidateDiveTimeSpan(TimeSpan divePropertyValue, string divePropertyName) {
			if (divePropertyValue.Days < 0) {
				throw new InvalidOperationException($"Invalid {divePropertyName} value. {nameof(divePropertyValue.Days)} cannot be negative.");
			} else if (divePropertyValue.Hours < 0) {
				throw new InvalidOperationException($"Invalid {divePropertyName} value. {nameof(divePropertyValue.Hours)} cannot be negative.");
			} else if (divePropertyValue.Minutes < 0) {
				throw new InvalidOperationException($"Invalid {divePropertyName} value. {nameof(divePropertyValue.Minutes)} cannot be negative.");
			} else if (divePropertyValue.Seconds < 0) {
				throw new InvalidOperationException($"Invalid {divePropertyName} value. {nameof(divePropertyValue.Seconds)} cannot be negative.");
			}
		}

		private static void ValidateDivePropertyIsNotNegative(short divePropertyValue, string divePropertyName) {
			if (divePropertyValue < 0) {
				throw new InvalidOperationException($"Invalid {divePropertyName} value. {divePropertyName} cannot be negative.");
			}
		}
	}
}
