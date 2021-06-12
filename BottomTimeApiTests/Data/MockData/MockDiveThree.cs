using BottomTimeApi.Models;
using BottomTimeApi.Models.Enums;
using System;

namespace BottomTimeApiTests.Data.MockData {
	public class MockDiveThree : IMockDive {
		private readonly Dive _mockDive;

		public MockDiveThree() {
			_mockDive = new Dive {
				Id = 2000,
				Number = 51,
				Date = new DateTime(2008, 9, 17, 13, 8, 0),
				Location = "Monterey, CA, USA",
				DiveSite = "Mertridium Fields",
				StartAirPressure = 3100,
				EndAirPressure = 700,
				PressureType = PressureType.Psi,
				WearWetSuit = true,
				WetSuitType = WetSuitType.Full,
				WetSuitThickness = 7,
				MaxDepth = 51,
				SurfaceIntervalTime = new TimeSpan(2, 33, 0),
				BottomTime = new TimeSpan(1, 9, 0),
				DidSafetyStop = true,
				SafetyStopTime = new TimeSpan(0, 3, 0),
				PressureGroupStart = "B",
				PressureGroupEnd = "Z",
				ResidualNitrogenTime = new TimeSpan(0, 13, 0),
				AbsoluteBottomTime = new TimeSpan(1, 9, 0),
				TotalBottomTime = new TimeSpan(1, 22, 0),
				WaterTemperature = 55,
				TemperatureType = TemperatureType.Fahrenheit,
				Visibility = 40,
				VisibilityType = VisibilityType.Feet,
				Weight = 18,
				WeightType = WeightType.Pounds,
				TankSize = 100,
				TankType = TankType.HighPressureSteel,
				TankPressureType = TankPressureType.CubicFeet,
				DiveComments = "This was a really cool dive as well!",
				DiveBuddy = "Test diver 3",
				DiveBuddyCertificationNumber = "TEST003",
				DiveBuddyCertificationType = DiveBuddyCertificationType.SSI,
			};
		}

		public Dive GetMockDive() {
			return _mockDive;
		}
	}
}
