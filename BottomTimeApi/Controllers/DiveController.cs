using BottomTimeApi.DataAccess;
using BottomTimeApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BottomTimeApi.Controllers {
	[Route("api/dives")]
	[ApiController]
	public class DiveController : ControllerBase {
		private readonly IDiveRepository _diveRepository;

		public DiveController(IDiveRepository diveRepository) {
			_diveRepository = diveRepository;
		}

		// GET: api/dives
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<List<Dive>>> GetDivesAsync() {
			IEnumerable<Dive> dives = await _diveRepository.GetDivesAsync();

			return Ok(dives.ToList());
		}

		// POST: api/dives
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		public async Task<ActionResult<Dive>> AddDiveAsync(DiveDto diveDto) {
			Dive dive = new Dive {
				DiveSite = diveDto.DiveSite,
				Location = diveDto.Location,
				Date = diveDto.Date,
				TimeIn = diveDto.TimeIn,
				StartAirPressure = diveDto.StartAirPressure,
				EndAirPressure = diveDto.EndAirPressure,
				PressureType = diveDto.PressureType,
				WearWetSuit = diveDto.WearWetSuit,
				WetSuitType = diveDto.WetSuitType,
				WetSuitThickness = diveDto.WetSuitThickness,
				WearDrySuit = diveDto.WearDrySuit,
				DrySuitType = diveDto.DrySuitType,
				DrySuitNumOfLiners = diveDto.DrySuitNumOfLiners,
				MaxDepth = diveDto.MaxDepth,
				AvgDepth = diveDto.AvgDepth,
				SurfaceIntervalTime = diveDto.SurfaceIntervalTime,
				BottomTime = diveDto.BottomTime,
				DidSafetyStop = diveDto.DidSafetyStop,
				SafetyStopTime = diveDto.SafetyStopTime,
				WaterTemperature = diveDto.WaterTemperature,
				TemperatureType = diveDto.TemperatureType,
				Visibility = diveDto.Visibility,
				VisibilityType = diveDto.VisibilityType,
				Weight = diveDto.Weight,
				WeightType = diveDto.WeightType,
				TankSize = diveDto.TankSize,
				TankType = diveDto.TankType,
				TankPressureType = diveDto.TankPressureType,
				DiveComments = diveDto.DiveComments,
				DiveBuddy = diveDto.DiveBuddy,
				DiveBuddyCertificationNumber = diveDto.DiveBuddyCertificationNumber,
				DiveBuddyCertificationType = diveDto.DiveBuddyCertificationType
			};

			await _diveRepository.AddDiveAsync(dive);

			return CreatedAtRoute("GetDiveByDiveNumber", new { id = dive.Id }, dive);
		}

		// GET: api/dives/5
		[HttpGet("{number}", Name = "GetDiveByDiveNumber")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<Dive>> GetDiveByDiveNumberAsync(int number) {
			Dive dive = await _diveRepository.GetDiveByDiveNumberAsync(number);

			return dive == null ? NotFound() : Ok(dive);
		}

		// PUT: api/dives/5
		[HttpPut("{number}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<Dive>> UpdateDiveAsync(int number, Dive dive) {
			if (number != dive.Id) {
				return BadRequest();
			}

			await _diveRepository.UpdateDiveAsync(dive);

			return NoContent();
		}

		// DELETE: api/dives/5
		[HttpDelete("{number}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<Dive>> DeleteDiveByDiveNumber(int number) {
			Dive dive = await _diveRepository.GetDiveByDiveNumberAsync(number);
			if (dive == null) {
				return NotFound();
			}

			await _diveRepository.DeleteDiveAsync(dive);

			return NoContent();
		}
	}
}