using BottomTimeApi.Models;
using System;

namespace BottomTimeApi.Validation {
	public class DiveValidator {
		public static void ValidateDive(Dive dive) {
			ValidateDiveNumber(dive);
			ValidateDiveStartAirPressure(dive);
			ValidateDiveEndAirPressure(dive);
			ValidateDiveDrySuitNumOfLiners(dive);
			ValidateDiveMaxDepth(dive);
			ValidateDiveAvgDepth(dive);
			ValidateDiveWaterTemperature(dive);
			ValidateDiveVisibility(dive);
			ValidateDiveWeight(dive);
			ValidateDiveTankSize(dive);
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

		private static void ValidateDiveDrySuitNumOfLiners(Dive dive) {
			if (dive.DrySuitNumOfLiners < 0) {
				throw new InvalidOperationException("Invalid dry suit number of liners. Dry suit number of cannot be negative.");
			}
		}

		private static void ValidateDiveMaxDepth(Dive dive) {
			if (dive.MaxDepth < 0) {
				throw new InvalidOperationException("Invalid dive max depth. Max depth cannot be negative.");
			}
		}

		private static void ValidateDiveAvgDepth(Dive dive) {
			if (dive.AvgDepth < 0) {
				throw new InvalidOperationException("Invalid dive average depth. Average depth cannot be negative.");
			} else if (dive.AvgDepth > dive.MaxDepth) {
				throw new InvalidOperationException("Invalid dive average depth. Average depth cannot be greater than max depth.");
			}
		}

		private static void ValidateDiveWaterTemperature(Dive dive) {
			if (dive.WaterTemperature < 0) {
				throw new InvalidOperationException("Invalid water temperature. Water temperature cannot be negative.");
			}
		}

		private static void ValidateDiveVisibility(Dive dive) {
			if (dive.Visibility < 0) {
				throw new InvalidOperationException("Invalid dive visibility. Visibility cannot be negative.");
			}
		}

		private static void ValidateDiveWeight(Dive dive) {
			if (dive.Weight < 0) {
				throw new InvalidOperationException("Invalid dive weight. Weight cannot be negative.");
			}
		}

		private static void ValidateDiveTankSize(Dive dive) {
			if (dive.TankSize < 0) {
				throw new InvalidOperationException("Invalid tank size. Tank size cannot be negative.");
			}
		}
	}
}
