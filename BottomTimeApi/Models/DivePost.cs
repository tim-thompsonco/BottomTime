using BottomTimeApi.Enums;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BottomTimeApi.Models {
	public class DivePost {
		[Required]
		[DefaultValue(100)]
		public short Number { get; set; }
		[Required]
		public DateTime Date { get; set; }
		[Required]
		[DefaultValue("Sample dive location")]
		public string Location { get; set; }
		[Required]
		[DefaultValue("Sample dive site")]
		public string DiveSite { get; set; }
		[Required]
		[DefaultValue(3000)]
		public short StartAirPressure { get; set; }
		[Required]
		[DefaultValue(1000)]
		public short EndAirPressure { get; set; }
		[Required]
		[DefaultValue(PressureType.Psi)]
		public PressureType PressureType { get; set; }
		[Required]
		[DefaultValue(WetSuitType.None)]
		public WetSuitType WetSuitType { get; set; }
		public short WetSuitThickness { get; set; }
		[Required]
		[DefaultValue(DrySuitType.None)]
		public DrySuitType DrySuitType { get; set; }
		public short DrySuitNumOfLiners { get; set; }
		[Required]
		[DefaultValue(45)]
		public short MaxDepth { get; set; }
		[DefaultValue(30)]
		public short AvgDepth { get; set; }
		[DefaultValue("00:00:00")]
		public TimeSpan SurfaceIntervalTime { get; set; }
		[DefaultValue("00:45:00")]
		public TimeSpan BottomTime { get; set; }
		[Required]
		[DefaultValue("00:03:00")]
		public TimeSpan SafetyStopTime { get; set; }
		[DefaultValue("A")]
		public string PressureGroupStart { get; set; }
		[DefaultValue("Z")]
		public string PressureGroupEnd { get; set; }
		[Required]
		[DefaultValue("00:00:00")]
		public TimeSpan ResidualNitrogenTime { get; set; }
		[Required]
		[DefaultValue("00:00:00")]
		public TimeSpan AbsoluteBottomTime { get; set; }
		[Required]
		[DefaultValue("00:00:00")]
		public TimeSpan TotalBottomTime { get; set; }
		[DefaultValue(50)]
		public short WaterTemperature { get; set; }
		public TemperatureType TemperatureType { get; set; }
		[DefaultValue(20)]
		public short Visibility { get; set; }
		public VisibilityType VisibilityType { get; set; }
		[DefaultValue(20)]
		public short Weight { get; set; }
		public WeightType WeightType { get; set; }
		[DefaultValue(80)]
		public short TankSize { get; set; }
		public TankType TankType { get; set; }
		public TankPressureType TankPressureType { get; set; }
		[DefaultValue("What a great dive!")]
		public string DiveComments { get; set; }
		[DefaultValue("Sample Dive Buddy")]
		public string DiveBuddy { get; set; }
		[DefaultValue("PADI001")]
		public string DiveBuddyCertificationNumber { get; set; }
		public DiveBuddyCertificationType DiveBuddyCertificationType { get; set; }
	}
}