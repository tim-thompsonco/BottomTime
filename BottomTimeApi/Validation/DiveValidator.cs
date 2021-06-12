using BottomTimeApi.Models;
using System;

namespace BottomTimeApi.Validation {
	public class DiveValidator {
		public static void ValidateDive(Dive dive) {
			ValidateDiveNumber(dive);
			ValidateDiveLocation(dive);
			ValidateDiveSite(dive);
			ValidateDiveStartAirPressure(dive);
			ValidateDiveEndAirPressure(dive);
		}

		private static void ValidateDiveNumber(Dive dive) {
			if (dive.Number > 10000) {
				throw new Exception("Dive number is too high. The maximum dive number is 10,000.");
			}

			if (dive.Number < 1) {
				throw new Exception("Invalid dive number. The dive number must be 1 or higher.");
			}
		}

		private static void ValidateDiveLocation(Dive dive) {
			if (dive.Location == null || dive.Location.Length == 0) {
				throw new Exception("Invalid dive location. Location cannot be empty.");
			}
		}

		private static void ValidateDiveSite(Dive dive) {
			if (dive.DiveSite == null || dive.DiveSite.Length == 0) {
				throw new Exception("Invalid dive site. Dive site cannot be empty.");
			}
		}

		private static void ValidateDiveStartAirPressure(Dive dive) {
			if (dive.StartAirPressure == 0) {
				throw new Exception("Invalid dive start air pressure. Start air pressure cannot be 0.");
			}

			if (dive.StartAirPressure < 0) {
				throw new Exception("Invalid dive start air pressure. Start air pressure cannot be negative.");
			}
		}

		private static void ValidateDiveEndAirPressure(Dive dive) {
			if (dive.EndAirPressure < 0) {
				throw new Exception("Invalid dive end air pressure. End air pressure cannot be negative.");
			}
		}
	}
}
