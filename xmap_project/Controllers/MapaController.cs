using Microsoft.AspNetCore.Mvc;
using xmap_project.Data;
using xmap_project.Modules;
using xmap_project.Services.Interfaces;


using Microsoft.AspNetCore.Mvc;
using xmap_project.Services;
using xmap_project.Modules;  
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace xmap_project.Controllers 
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MapasController : ControllerBase
    {
        private readonly IMapaService _mapaService;
        
        public MapasController(IMapaService mapaService)
        {
            _mapaService = mapaService;
        }

    
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Mapa>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Mapa>>> ListarTodos()
        {
            try
            {
                var mapas = await _mapaService.ListarMapasAsync();
                return Ok(mapas);
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao processar sua solicitação.");
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Mapa), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Mapa>> ObterPorId(int id)
        {
            try
            {
                var mapa = await _mapaService.ObterMapaPorIdAsync(id);
                if (mapa == null)
                {
                    return NotFound($"Mapa com ID {id} não encontrado.");
                }
                return Ok(mapa);
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao processar sua solicitação.");
            }
        }

        // Você pode adicionar outros endpoints aqui (POST para criar, PUT para atualizar, DELETE para remover)
        // Exemplo de endpoint POST para criar um novo mapa:
        /*
        [HttpPost]
        [ProducesResponseType(typeof(Mapa), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Mapa>> CriarMapa([FromBody] Mapa novoMapa)
        {
            if (novoMapa == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var mapaCriado = await _mapaService.CriarMapaAsync(novoMapa); // Supondo que você criou este método no serviço
                return CreatedAtAction(nameof(ObterPorId), new { id = mapaCriado.id }, mapaCriado);
            }
            catch (System.Exception ex)
            {
                // Logar o erro
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao criar o mapa.");
            }
        }
        */
    }
}