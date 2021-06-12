using BottomTimeApi.Models;
using System;

namespace BottomTimeApi.Validation {
	public class DiveValidator {
		public static void ValidateDive(Dive dive) {
			ValidateDiveNumber(dive);
		}

		private static void ValidateDiveNumber(Dive dive) {
			if (dive.Number > 10000) {
				throw new Exception("Dive number is too high. The maximum dive number is 10,000.");
			}

			if (dive.Number < 1) {
				throw new Exception("Invalid dive number. The dive number must be 1 or higher.");
			}
		}
	}
}
