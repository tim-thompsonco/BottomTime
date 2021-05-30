using BottomTimeApi.Data;
using BottomTimeApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BottomTimeApi.Controllers {
	[Route("api/dives")]
	[ApiController]
	public class DiveController : ControllerBase {
		private readonly DiveDataContext _context;

		public DiveController(DiveDataContext context) {
			_context = context;
		}

		// GET: api/dives
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Dive>>> GetAllDives() {
			return await _context.Dives.ToListAsync();
		}

		// GET: api/dives/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Dive>> GetSingleDive(int id) {
			Dive dive = await _context.Dives.FindAsync(id);

			return dive == null ? NotFound() : (ActionResult<Dive>)dive;
		}

		// POST: api/dives
		[HttpPost]
		public async Task<ActionResult<Dive>> PostDive(Dive dive) {
			_context.Dives.Add(dive);

			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(GetSingleDive), new { id = dive.Id }, dive);
		}
	}
}