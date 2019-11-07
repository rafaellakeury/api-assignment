using System.Threading.Tasks;
using Assignment.API.Domain.Models;
using Assignment.API.Domain.Services;
using Assignment.API.Resources;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Supermarket.API.Extensions;

namespace Assignment.API.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class DiffController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly ILogger<DiffController> _logger;
        private readonly IMapper _mapper;

        public DiffController(IPersonService personService, ILogger<DiffController> logger, IMapper mapper)
        {
            _personService = personService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDifferencesAsync(int id)
        {
            var getDiffResponse = await _personService.ListDifferencesAsync(id);

            if (!getDiffResponse.Success)
                return BadRequest(getDiffResponse.Message);
            var resources = _mapper.Map<DiffResult, PeopleDifferenceResource>(getDiffResponse.DiffResult);
            return Ok(resources);
        }

        [HttpPost("{id}/left")]
        public async Task<IActionResult> PostLeftAsync(int id, [FromBody] SavePersonResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var person = _mapper.Map<SavePersonResource, LeftPerson>(resource);

            var result = await _personService.SavePersonAsync(id, person);

            if (!result.Success)
                return BadRequest(result.Message);

            var personResource = _mapper.Map<Person, LeftPersonResource>(result.Person);
            return Ok(personResource);
        }

        [HttpPost("{id}/right")]
        public async Task<IActionResult> PostRightAsync(int id, [FromBody] SavePersonResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var person = _mapper.Map<SavePersonResource, RightPerson>(resource);

            var result = await _personService.SavePersonAsync(id, person);

            if (!result.Success)
                return BadRequest(result.Message);

            var personResource = _mapper.Map<Person, RightPersonResource>(result.Person);
            return Ok(personResource);
        }
    }
}