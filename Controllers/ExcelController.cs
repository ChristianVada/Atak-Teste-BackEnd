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
        public async Task<IActionResult> EnviarEmail([FromQuery] int quantidade)
        {
            if (quantidade < 10 || quantidade > 1000)
                return BadRequest("A quantidade deve ser entre 10 e 1000.");

            try
            {
                var arquivoExcel = await _gerarExcelService.GerarArquivoExcelAsync(quantidade);

                await _emailService.EnviarEmailComAnexoAsync(arquivoExcel);

                return Ok("Email enviado com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao enviar e-mail: {ex.Message}");
            }
        }

        //[Authorize]
        //[HttpPost("gerar-excel")]
        //public async Task<IActionResult> GerarExcel([FromQuery] int quantidade)
        //{
        //    if (quantidade <= 10 || quantidade >= 1000)
        //        return BadRequest("A quantidade deve ser no mínimo 10 e no máximo 1000.");

        //    var arquivoExcel = await _gerarExcelService.GerarArquivoExcelAsync(quantidade);
        //    return File(arquivoExcel, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "clientes.xlsx");
        //}
    }
}
