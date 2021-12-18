using BottomTimeApi.Models;
using System;

namespace BottomTimeApi.Validation {
	public class DiveValidator {
		public static void ValidateDive(Dive dive) {
			ValidateDiveDate(dive);
			ValidateDiveNumber(dive);
			ValidateDiveStartAirPressure(dive);
			ValidateDiveEndAirPressure(dive);
			ValidateDiveMaxDepth(dive);
			ValidateDiveAvgDepth(dive);
			ValidateDivePropertyIsNotNegative(dive.WaterTemperature, nameof(dive.WaterTemperature));
			ValidateDivePropertyIsNotNegative(dive.DrySuitNumOfLiners, nameof(dive.DrySuitNumOfLiners));
			ValidateDivePropertyIsNotNegative(dive.Visibility, nameof(dive.Visibility));
			ValidateDivePropertyIsNotNegative(dive.Weight, nameof(dive.Weight));
			ValidateDivePropertyIsNotNegative(dive.TankSize, nameof(dive.TankSize));
		}

		private static void ValidateDiveDate(Dive dive) {
			if (dive.Date > DateTime.UtcNow) {
				throw new InvalidOperationException("Dive date cannot be a date in the future.");
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

		private static void ValidateDivePropertyIsNotNegative(short divePropertyValue, string divePropertyName) {
			if (divePropertyValue < 0) {
				throw new InvalidOperationException($"Invalid {divePropertyName} value. {divePropertyName} cannot be negative.");
			}
		}
	}
}
