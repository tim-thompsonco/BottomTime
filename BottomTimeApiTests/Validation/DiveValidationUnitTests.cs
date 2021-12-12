﻿using BottomTimeApi.Models;
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
		public void ValidateDiveFailsWhenDiveLocationEmptyTest() {
			Dive dive = new MockDive {
				Location = string.Empty
			};
			Exception exception = null;

			try {
				DiveValidator.ValidateDive(dive);
			} catch (Exception ex) {
				exception = ex;
			}

			Assert.IsNotNull(exception);
			Assert.IsTrue(exception is InvalidOperationException);
			Assert.IsTrue(exception.Message is "Invalid dive location. Location cannot be empty.");
		}


		[TestMethod]
		public void ValidateDiveFailsWhenDiveLocationMissingTest() {
			Dive dive = new MockDive {
				Location = null
			};
			Exception exception = null;

			try {
				DiveValidator.ValidateDive(dive);
			} catch (Exception ex) {
				exception = ex;
			}

			Assert.IsNotNull(exception);
			Assert.IsTrue(exception is InvalidOperationException);
			Assert.IsTrue(exception.Message is "Invalid dive location. Location cannot be empty.");
		}

		[TestMethod]
		public void ValidateDiveFailsWhenDiveSiteEmptyTest() {
			Dive dive = new MockDive {
				DiveSite = string.Empty
			};
			Exception exception = null;

			try {
				DiveValidator.ValidateDive(dive);
			} catch (Exception ex) {
				exception = ex;
			}

			Assert.IsNotNull(exception);
			Assert.IsTrue(exception is InvalidOperationException);
			Assert.IsTrue(exception.Message is "Invalid dive site. Dive site cannot be empty.");
		}

		[TestMethod]
		public void ValidateDiveFailsWhenDiveSiteMissingTest() {
			Dive dive = new MockDive {
				DiveSite = null
			};
			Exception exception = null;

			try {
				DiveValidator.ValidateDive(dive);
			} catch (Exception ex) {
				exception = ex;
			}

			Assert.IsNotNull(exception);
			Assert.IsTrue(exception is InvalidOperationException);
			Assert.IsTrue(exception.Message is "Invalid dive site. Dive site cannot be empty.");
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
	}
}
