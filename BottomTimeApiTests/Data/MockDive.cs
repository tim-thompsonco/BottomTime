using BottomTimeApi.Enums;
using BottomTimeApi.Models;
using System;

namespace BottomTimeApiTests.Data.MockData {
	public class MockDive : Dive {
		public MockDive() {
			Id = 342;
			Number = 79;
			Date = new DateTime(2009, 7, 4, 15, 41, 0);
			Location = "Anacapa; CA; USA";
			DiveSite = "Underwater Island";
			StartAirPressure = 2600;
			EndAirPressure = 1000;
			PressureType = PressureType.Psi;
			WetSuitType = WetSuitType.Full;
			WetSuitThickness = 7;
			MaxDepth = 51;
			AvgDepth = 34;
			SurfaceIntervalTime = new TimeSpan(1, 12, 0);
			BottomTime = new TimeSpan(0, 39, 0);
			SafetyStopTime = new TimeSpan(0, 3, 0);
			WaterTemperature = 61;
			TemperatureType = TemperatureType.Fahrenheit;
			Visibility = 20;
			VisibilityType = VisibilityType.Feet;
			Weight = 18;
			WeightType = WeightType.Pounds;
			TankSize = 80;
			TankType = TankType.HighPressureSteel;
			TankPressureType = TankPressureType.CubicFeet;
			DiveComments = "This was a really cool dive!";
			DiveBuddy = "Test diver";
			DiveBuddyCertificationNumber = "TEST001";
			DiveBuddyCertificationType = DiveBuddyCertificationType.PADI;
		}
	}
}
