using Atak2.Models;
using Atak2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Atak2.Controllers
{
    [EnableCors("MyPolicy")]
    [ApiController]
    [Route("api/[controller]")]
    public class ExcelController : ControllerBase
    {
        private readonly GerarExcelService _gerarExcelService;
        private readonly EmailService _emailService;

        public ExcelController(GerarExcelService gearExcelService, EmailService emailService)
        {
            _gerarExcelService = gearExcelService;
            _emailService = emailService;
        }

        [Authorize]
        [HttpPost("enviar-email")]
        public async Task<IActionResult> EnviarEmail(DadosEmailModel dadosEmail)
        {
            if (dadosEmail.Quantidade <= 10 || dadosEmail.Quantidade >= 1000)
                return BadRequest("A quantidade deve ser de no minímo 10 e no máximo 1000.");

            try
            {
                var arquivoExcel = await _gerarExcelService.GerarArquivoExcelAsync(dadosEmail.Quantidade);

                await _emailService.EnviarEmailComAnexoAsync(arquivoExcel, dadosEmail);

                return Ok("Email enviado com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao enviar e-mail: {ex.Message}");
            }
        }
    }
}
