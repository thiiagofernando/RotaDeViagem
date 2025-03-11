using Microsoft.AspNetCore.Mvc;
using RotaDeViagemApi.DTOs;
using RotaDeViagemApi.Services;

namespace RotaDeViagemApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RotaController : ControllerBase
    {
        private readonly RotaService _rotaService;

        public RotaController(RotaService rotaService)
        {
            _rotaService = rotaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RotaResponseDto>>> GetRotas()
        {
            var rotas = await _rotaService.GetAllRotasAsync();
            return Ok(rotas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RotaResponseDto>> GetRota(int id)
        {
            var rota = await _rotaService.GetRotaByIdAsync(id);
            if (rota == null)
            {
                return NotFound();
            }
            return Ok(rota);
        }

        [HttpPost]
        public async Task<ActionResult<RotaResponseDto>> PostRota(RotaRequestDto rotaDto)
        {
            var createdRota = await _rotaService.CreateRotaAsync(rotaDto);
            return CreatedAtAction(
                nameof(GetRota),
                new { id = createdRota.Id },
                createdRota);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RotaResponseDto>> PutRota(int id, RotaRequestDto rotaDto)
        {
            var updatedRota = await _rotaService.UpdateRotaAsync(id, rotaDto);
            if (updatedRota == null)
            {
                return NotFound();
            }
            return Ok(updatedRota);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRota(int id)
        {
            var result = await _rotaService.DeleteRotaAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet("consulta")]
        public async Task<ActionResult<ConsultaRotaResponseDto>> ConsultarMelhorRota([FromQuery] ConsultaRotaRequestDto request)
        {
            var resultado = await _rotaService.ConsultarMelhorRotaAsync(request);
            if (resultado == null)
            {
                return NotFound("Não foi possível encontrar uma rota entre os pontos especificados.");
            }
            return Ok(resultado);
        }
    }
}
