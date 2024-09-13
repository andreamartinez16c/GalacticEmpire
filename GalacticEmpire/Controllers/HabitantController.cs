using GalacticEmpire.Models.DTOs;
using GalacticEmpire.Models.Entities;
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
        private readonly ILogger<HabitantController> _logger;

        public HabitantController(IHabitantRepository habitantRepository, ILogger<HabitantController> logger)
        {
            _habitantRepository = habitantRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HabitantDto>>> GetAll()
        {
            var habitants = await _habitantRepository.GetAllAsync();
            return Ok(habitants);
        }

        [HttpPost]
        public async Task<ActionResult> Create(HabitantDto habitantDto)
        {
            try
            {
                await _habitantRepository.AddAsync(habitantDto);
                _logger.LogInformation($"Habitant {habitantDto.Name} created successfully.");
                return CreatedAtAction(nameof(GetByName), new { name = habitantDto.Name }, habitantDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating habitant.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<HabitantDto>> GetByName(string name)
        {
            var habitant = await _habitantRepository.GetHabitantByNameAsync(name);
            if (habitant == null)
            {
                return NotFound();
            }
            return Ok(habitant);
        }

        [HttpGet("planet/{planetName}")]
        public async Task<ActionResult<IEnumerable<HabitantDto>>> GetByPlanetName(string planetName)
        {
            var habitants = await _habitantRepository.GetHabitantsByPlanetNameAsync(planetName);
            return Ok(habitants);
        }

        [HttpGet("specie/{specieName}")]
        public async Task<ActionResult<IEnumerable<HabitantDto>>> GetBySpecieName(string specieName)
        {
            var habitants = await _habitantRepository.GetHabitantsBySpecieNameAsync(specieName);
            return Ok(habitants);
        }

        [HttpGet("rebels")]
        public async Task<ActionResult<IEnumerable<HabitantDto>>> GetRebels()
        {
            var rebels = await _habitantRepository.GetRebelsAsync();
            return Ok(rebels);
        }
    }
}
