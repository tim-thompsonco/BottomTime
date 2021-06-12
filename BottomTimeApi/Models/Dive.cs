using BottomTimeApi.Models.Enums;
using System;
using System.Collections.Generic;

namespace BottomTimeApi.Models {
	public class Dive {
		public ushort Number { get; set; }
		public DateTime Date { get; set; }
		public string Location { get; set; }
		public string DiveSite { get; set; }
		public DateTime TimeIn { get; set; }
		public ushort StartAirPressure { get; set; }
		public ushort EndAirPressure { get; set; }
		public PressureType PressureType { get; set; }
		public List<WeatherAttributes> WeatherAttributes { get; set; }
		public List<DiveConditions> DiveConditions { get; set; }
		public List<DiveBottomAttributes> DiveBottomAttributes { get; set; }
		public bool WearWetSuit { get; set; }
		public WetSuitType? WetSuitType { get; set; }
		public byte? WetSuitThickness { get; set; }
		public bool WearDrySuit { get; set; }
		public DrySuitType? DrySuitType { get; set; }
		public byte? DrySuitNumOfLiners { get; set; }
		public ushort MaxDepth { get; set; }
		public ushort AvgDepth { get; set; }
		public DateTime? SurfaceIntervalTime { get; set; }
		public DateTime BottomTime { get; set; }
		public bool DidSafetyStop { get; set; }
		public DateTime? SafetyStopTime { get; set; }
		public char? PressureGroupStart { get; set; }
		public char? PressureGroupEnd { get; set; }
		public DateTime? ResidualNitrogenTime { get; set; }
		public DateTime? AbsoluteBottomTime { get; set; }
		public DateTime? TotalBottomTime { get; set; }
		public sbyte WaterTemperature { get; set; }
		public TemperatureType TemperatureType { get; set; }
		public byte Visibility { get; set; }
		public VisibilityType VisibilityType { get; set; }
		public byte Weight { get; set; }
		public WeightType WeightType { get; set; }
		public byte TankSize { get; set; }
		public TankType TankType { get; set; }
		public TankPressureType TankPressureType { get; set; }
		public float SurfaceAirConsumption { get; set; }
		public List<string> AquaticLifeObserved { get; set; }
		public string DiveComments { get; set; }
		public string DiveBuddy { get; set; }
		public string DiveBuddyCertificationNumber { get; set; }
		public DiveBuddyCertificationType DiveBuddyCertificationType { get; set; }
	}
}