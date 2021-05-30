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
		public async Task<ActionResult<List<Dive>>> GetDives() {
			IEnumerable<Dive> dives = await _diveRepository.GetDivesAsync();

			return Ok(dives.ToList());
		}

		// GET: api/dives/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Dive>> GetDiveById(int id) {
			Dive dive = await _diveRepository.GetDiveByIdAsync(id);

			return dive == null ? NotFound() : Ok(dive);
		}

		// POST: api/dives
		[HttpPost]
		public async Task<ActionResult<Dive>> AddDive(Dive dive) {
			await _diveRepository.AddDiveAsync(dive);

			return Ok(dive);
		}
	}
}