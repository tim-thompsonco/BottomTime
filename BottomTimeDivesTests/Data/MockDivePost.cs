using BottomTimeDives.Models;
using BottomTimeDives.Enums;
using System;

namespace BottomTimeDivesTests.Data.MockData {
	public class MockDivePost : DivePost {
		public MockDivePost() {
			Number = 149;
			Date = new DateTime(2012, 9, 7, 10, 45, 0);
			Location = "Point Lobos, CA, USA";
			DiveSite = "Whaler's Cove";
			StartAirPressure = 3200;
			EndAirPressure = 1200;
			PressureType = PressureType.Psi;
			DrySuitType = DrySuitType.Shell;
			DrySuitNumOfLiners = 1;
			MaxDepth = 30;
			AvgDepth = 30;
			BottomTime = new TimeSpan(0, 45, 0);
			SafetyStopTime = new TimeSpan(0, 3, 0);
			WaterTemperature = 52;
			TemperatureType = TemperatureType.Fahrenheit;
			Visibility = 15;
			VisibilityType = VisibilityType.Feet;
			Weight = 20;
			WeightType = WeightType.Pounds;
			TankSize = 80;
			TankType = TankType.HighPressureSteel;
			TankPressureType = TankPressureType.CubicFeet;
			DiveComments = "This was a really cool dive too!";
			DiveBuddy = "Test diver 2";
			DiveBuddyCertificationNumber = "TEST002";
			DiveBuddyCertificationType = DiveBuddyCertificationType.NAUI;
		}
	}
}
