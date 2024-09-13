using GalacticEmpire.Models.DTOs;
using GalacticEmpire.Models.Entities;
using GalacticEmpire.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GalacticEmpire.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanetController : ControllerBase
    {
        private readonly IPlanetRepository _planetRepository;
        private readonly ILogger<PlanetController> _logger;

        public PlanetController(IPlanetRepository planetRepository, ILogger<PlanetController> logger)
        {
            _planetRepository = planetRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlanetDTO>>> GetAll()
        {
            var planets = await _planetRepository.GetAllAsync();
            return Ok(planets);
        }

        [HttpPost]
        public async Task<ActionResult> Create(PlanetDTO planetDto)
        {
            await _planetRepository.AddAsync(planetDto);
            return CreatedAtAction(nameof(GetByName), new { name = planetDto.Name }, planetDto);
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<PlanetDTO>> GetByName(string name)
        {
            _logger.LogInformation($"Getting planet by name: {name}.");
            try
            {
                var planet = await _planetRepository.GetPlanetByNameAsync(name);
                if (planet == null)
                {
                    _logger.LogWarning($"Planet {name} not found.");
                    return NotFound();
                }
                return Ok(planet);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving planet {name}.");
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}
