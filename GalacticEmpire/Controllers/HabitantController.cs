using GalacticEmpire.Models;
using GalacticEmpire.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GalacticEmpire.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HabitantController : ControllerBase
    {
        private readonly IHabitantRepository _habitantRepository;

        public HabitantController(IHabitantRepository habitantRepository)
        {
            _habitantRepository = habitantRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Habitant>>> GetAllHabitants()
        {
            var habitants = await _habitantRepository.GetAllHabitantsAsync();
            return Ok(habitants);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Habitant>> GetHabitantById(int id)
        {
            var habitant = await _habitantRepository.GetHabitantByIdAsync(id);

            if (habitant == null)
            {
                return NotFound();
            }

            return Ok(habitant);
        }

        [HttpPost]
        public async Task<ActionResult> AddHabitant(Habitant habitant)
        {
            await _habitantRepository.AddHabitantAsync(habitant);
            return CreatedAtAction(nameof(GetHabitantById), new { id = habitant.Id }, habitant);
        }

        [HttpGet("rebels")]
        public async Task<ActionResult<IEnumerable<Habitant>>> GetRebels()
        {
            var rebels = await _habitantRepository.GetRebelsAsync();
            return Ok(rebels);
        }

        [HttpGet("planets")]
        public async Task<ActionResult<IEnumerable<Planet>>> GetPlanets()
        {
            var planets = await _habitantRepository.GetPlanetsAsync();
            return Ok(planets);
        }

        [HttpGet("species")]
        public async Task<ActionResult<IEnumerable<Specie>>> GetSpecies()
        {
            var species = await _habitantRepository.GetSpeciesAsync();
            return Ok(species);
        }
    }
}
