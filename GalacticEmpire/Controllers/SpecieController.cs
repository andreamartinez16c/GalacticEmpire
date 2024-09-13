using GalacticEmpire.Models.DTOs;
using GalacticEmpire.Models.Entities;
using GalacticEmpire.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GalacticEmpire.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecieController : ControllerBase
    {
        private readonly ISpecieRepository _specieRepository;
        private readonly ILogger<SpecieController> _logger;

        public SpecieController(ISpecieRepository specieRepository, ILogger<SpecieController> logger)
        {
            _specieRepository = specieRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpecieDto>>> GetAll()
        {
            var species = await _specieRepository.GetAllAsync();
            return Ok(species);
        }

        [HttpPost]
        public async Task<ActionResult> Create(SpecieDto specieDto)
        {
            try
            {
                await _specieRepository.AddAsync(specieDto);
                _logger.LogInformation($"Specie {specieDto.Name} created successfully.");
                return CreatedAtAction(nameof(GetByName), new { name = specieDto.Name }, specieDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating specie.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<SpecieDto>> GetByName(string name)
        {
            _logger.LogInformation($"Getting specie by name: {name}.");
            var specie = await _specieRepository.GetSpecieByNameAsync(name);
            if (specie == null)
            {
                _logger.LogWarning($"Specie {name} not found.");
                return NotFound();
            }
            return Ok(specie);
        }
    }
}
