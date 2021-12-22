using AutoMapper;
using BottomTimeDives.Data;
using BottomTimeDives.Models;
using BottomTimeDives.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BottomTimeDives.Controllers {
	[Route("dives")]
	[ApiController]
	public class DiveController : ControllerBase {
		private readonly IDiveRepository _diveRepository;
		private readonly IMapper _mapper;

		public DiveController(IDiveRepository diveRepository, IMapper mapper) {
			_diveRepository = diveRepository;
			_mapper = mapper;
		}

		// GET: dives
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<List<Dive>>> GetDivesAsync(int pageNumber = 1, int divesPerPage = 10) {
			IEnumerable<Dive> dives = await _diveRepository.GetDivesAsync(pageNumber, divesPerPage);

			return Ok(dives.ToList());
		}

		// POST: dives
		[HttpPost]
		[ProducesResponseType(typeof(Dive), StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<Dive>> AddDiveAsync(DivePost divePost) {
			Dive dive = _mapper.Map<Dive>(divePost);

			DiveValidator.ValidateDive(dive);

			await _diveRepository.AddDiveAsync(dive);

			return CreatedAtRoute("GetDiveByDiveId", new { id = dive.Id }, dive);
		}

		// GET: dives/{id}
		[HttpGet("{id}", Name = "GetDiveByDiveId")]
		[ProducesResponseType(typeof(Dive), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<Dive>> GetDiveByDiveIdAsync(int id) {
			Dive dive = await _diveRepository.GetDiveByDiveIdAsync(id);

			return dive == null ? NotFound() : Ok(dive);
		}

		// PUT: dives
		[HttpPut]
		[ProducesResponseType(typeof(Dive), StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<Dive>> UpdateDiveAsync(Dive dive) {
			DiveValidator.ValidateDive(dive);

			await _diveRepository.UpdateDiveAsync(dive);

			return NoContent();
		}

		// DELETE: dives/{id}
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