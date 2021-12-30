using BottomTimeDives.Enums;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BottomTimeDives.Models {
	public class DivePost {
		[Required]
		[DefaultValue(100)]
		public short Number { get; set; }
		[Required]
		[MaxLength(50)]
		[DefaultValue("Sample dive location")]
		public string Location { get; set; }
		[Required]
		[MaxLength(50)]
		[DefaultValue("Sample dive site")]
		public string DiveSite { get; set; }
		[Required]
		[DefaultValue("2021.12.30.08:00:00")]
		public DateTime DiveStartTime { get; set; }
		[Required]
		[DefaultValue("2021.12.30.08:30:00")]
		public DateTime DiveEndTime { get; set; }
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
		[Required]
		[DefaultValue("00:03:00")]
		public TimeSpan SafetyStopTime { get; set; }
		[MaxLength(1)]
		[DefaultValue("A")]
		public string PressureGroupStart { get; set; } = string.Empty;
		[MaxLength(1)]
		[DefaultValue("Z")]
		public string PressureGroupEnd { get; set; } = string.Empty;
		[DefaultValue("00:00:00")]
		public TimeSpan ResidualNitrogenTime { get; set; } = new TimeSpan(0, 0, 0);
		[DefaultValue("00:00:00")]
		public TimeSpan AbsoluteBottomTime { get; set; } = new TimeSpan(0, 0, 0);
		[DefaultValue("00:00:00")]
		public TimeSpan TotalBottomTime { get; set; } = new TimeSpan(0, 0, 0);
		[DefaultValue(50)]
		public short WaterTemperature { get; set; }
		public TemperatureType TemperatureType { get; set; }
		[DefaultValue(20)]
		public short Visibility { get; set; }
		public VisibilityType VisibilityType { get; set; }
		[Required]
		[DefaultValue(20)]
		public short Weight { get; set; }
		[Required]
		public WeightType WeightType { get; set; }
		[Required]
		[DefaultValue(80)]
		public short TankSize { get; set; }
		[Required]
		public TankType TankType { get; set; }
		[Required]
		public TankPressureType TankPressureType { get; set; }
		[MaxLength(2000)]
		[DefaultValue("What a great dive!")]
		public string DiveComments { get; set; }
		[MaxLength(50)]
		[DefaultValue("Sample Dive Buddy")]
		public string DiveBuddy { get; set; }
		[MaxLength(25)]
		[DefaultValue("PADI001")]
		public string DiveBuddyCertificationNumber { get; set; }
		public DiveBuddyCertificationType DiveBuddyCertificationType { get; set; }
	}
}