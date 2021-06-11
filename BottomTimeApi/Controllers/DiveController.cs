using BottomTimeApi.DataAccess;
using BottomTimeApi.Models;
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
		public async Task<ActionResult<List<Dive>>> GetDivesAsync() {
			IEnumerable<Dive> dives = await _diveRepository.GetDivesAsync();

			return Ok(dives.ToList());
		}

		// POST: api/dives
		[HttpPost]
		public async Task<ActionResult<Dive>> AddDiveAsync(Dive dive) {
			await _diveRepository.AddDiveAsync(dive);

			return CreatedAtAction(nameof(AddDiveAsync), new { number = dive.Number }, dive);
		}

		// GET: api/dives/5
		[HttpGet("{number}")]
		public async Task<ActionResult<Dive>> GetDiveByDiveNumberAsync(int number) {
			Dive dive = await _diveRepository.GetDiveByDiveNumberAsync(number);

			return dive == null ? NotFound() : Ok(dive);
		}

		// PUT: api/dives/5
		[HttpPut("{number}")]
		public async Task<ActionResult<Dive>> UpdateDiveAsync(int number, Dive dive) {
			if (number != dive.Number) {
				return BadRequest();
			}

			await _diveRepository.UpdateDiveAsync(dive);

			return NoContent();
		}

		// DELETE: api/dives/5
		[HttpDelete("{number}")]
		public async Task<ActionResult<Dive>> DeleteDiveByNumberAsync(int number) {
			Dive dive = await _diveRepository.GetDiveByDiveNumberAsync(number);
			if (dive == null) {
				return NotFound();
			}

			await _diveRepository.DeleteDiveAsync(dive);

			return NoContent();
		}
	}
}