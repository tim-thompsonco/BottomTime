using AutoMapper;
using BottomTimeApi.Data;
using BottomTimeApi.Models;
using BottomTimeApi.Validation;
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
		private readonly IMapper _mapper;

		public DiveController(IDiveRepository diveRepository, IMapper mapper) {
			_diveRepository = diveRepository;
			_mapper = mapper;
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
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<Dive>> AddDiveAsync(DivePost divePost) {
			Dive dive = _mapper.Map<Dive>(divePost);

			DiveValidator.ValidateDive(dive);

			await _diveRepository.AddDiveAsync(dive);

			return CreatedAtRoute("GetDiveByDiveId", new { id = dive.Id }, dive);
		}

		// GET: api/dives/5
		[HttpGet("{id}", Name = "GetDiveByDiveId")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<Dive>> GetDiveByDiveIdAsync(int id) {
			Dive dive = await _diveRepository.GetDiveByDiveIdAsync(id);

			return dive == null ? NotFound() : Ok(dive);
		}

		// PUT: api/dives/5
		[HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<Dive>> UpdateDiveAsync(int id, Dive dive) {
			if (id != dive.Id) {
				return BadRequest();
			}

			DiveValidator.ValidateDive(dive);

			await _diveRepository.UpdateDiveAsync(dive);

			return NoContent();
		}

		// DELETE: api/dives/5
		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<Dive>> DeleteDiveByDiveId(int id) {
			Dive dive = await _diveRepository.GetDiveByDiveIdAsync(id);
			if (dive == null) {
				return NotFound();
			}

			await _diveRepository.DeleteDiveAsync(dive);

			return NoContent();
		}
	}
}