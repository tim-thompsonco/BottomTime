using BottomTimeApi.Enums;
using System;

namespace BottomTimeApi.Models {
	public class DivePost {
		public short Number { get; set; }
		public DateTime Date { get; set; }
		public string Location { get; set; }
		public string DiveSite { get; set; }
		public short StartAirPressure { get; set; }
		public short EndAirPressure { get; set; }
		public PressureType PressureType { get; set; }
		public bool WearWetSuit { get; set; }
		public WetSuitType WetSuitType { get; set; }
		public short? WetSuitThickness { get; set; }
		public bool WearDrySuit { get; set; }
		public DrySuitType DrySuitType { get; set; }
		public short? DrySuitNumOfLiners { get; set; }
		public short MaxDepth { get; set; }
		public short AvgDepth { get; set; }
		public TimeSpan? SurfaceIntervalTime { get; set; }
		public TimeSpan BottomTime { get; set; }
		public bool DidSafetyStop { get; set; }
		public TimeSpan? SafetyStopTime { get; set; }
		public string PressureGroupStart { get; set; }
		public string PressureGroupEnd { get; set; }
		public TimeSpan? ResidualNitrogenTime { get; set; }
		public TimeSpan? AbsoluteBottomTime { get; set; }
		public TimeSpan? TotalBottomTime { get; set; }
		public short WaterTemperature { get; set; }
		public TemperatureType TemperatureType { get; set; }
		public short Visibility { get; set; }
		public VisibilityType VisibilityType { get; set; }
		public short Weight { get; set; }
		public WeightType WeightType { get; set; }
		public short TankSize { get; set; }
		public TankType TankType { get; set; }
		public TankPressureType TankPressureType { get; set; }
		public string DiveComments { get; set; }
		public string DiveBuddy { get; set; }
		public string DiveBuddyCertificationNumber { get; set; }
		public DiveBuddyCertificationType DiveBuddyCertificationType { get; set; }
	}
}