using Atak2.Models;
using Atak2.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Atak2.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly AutenticacaoService _autenticacaoService;

        public AutenticacaoController(AutenticacaoService autenticacaoService)
        {
            _autenticacaoService = autenticacaoService;
        }

        [HttpPost("registro")]
        public async Task<IActionResult> Registrar(RequisicaoRegistroModel requisicao)
        {
            try
            {
                var usuario = await _autenticacaoService.RegistrarAsync(requisicao);
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(RequisicaoLoginModel requisicao)
        {
            try
            {
                var usuario = await _autenticacaoService.LoginAsync(requisicao);
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}
