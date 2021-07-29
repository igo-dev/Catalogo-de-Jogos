using Catalogo_de_jogos.Dtos;
using Catalogo_de_jogos.Exceptions;
using Catalogo_de_jogos.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Catalogo_de_jogos.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class JogosController : ControllerBase
    {
        private readonly IJogoService _jogoService;

        public JogosController(IJogoService jogoService)
        {
            _jogoService = jogoService;
        }

        /// <summary>
        /// Buscar todos os jogos de forma paginada.
        /// </summary>
        /// <param name="pag">Indica qual a página sendo requisitada.</param>
        /// <param name="qnt">Indica a quantidade de registros por página.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JogoDto>>> Obter([FromQuery, Range(1, int.MaxValue)] int pag = 1, [FromQuery, Range(1, 50)] int qnt = 5)
        {
            var jogos = await _jogoService.Obter(pag, qnt);

            if (jogos.Count == 0) 
                return NoContent();

            return Ok(jogos);
        }
        
        /// <summary>
        /// Buscar jogo pelo id.
        /// </summary>
        /// <param name="idJogo">Indica o id do jogo a ser requisitado.</param>
        /// <returns></returns>
        [HttpGet("{idJogo:guid}")]
        public async Task<ActionResult<JogoDto>> Obter([FromRoute] Guid idJogo )
        {
            var jogo = await _jogoService.Obter(idJogo);

            if (jogo == null)
                return NoContent();

            return Ok(jogo);
        }

        /// <summary>
        /// Inserir novo jogo.
        /// </summary>
        /// <param name="jogoDto">Indica as propriedades do jogo.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<JogoDto>> InserirJogo([FromBody] JogoDto jogoDto)
        {
            try
            {
                var jogo = await _jogoService.Inserir(jogoDto);
                return Ok(jogo);
            }
            catch (JogoJaCadastradoException)
            {
                return UnprocessableEntity("Já existe um jogo com este nome para esta produtora.");
            }
            
        }

        /// <summary>
        /// Atualizar todas as informações de um jogo.
        /// </summary>
        /// <param name="idJogo">Indica o id do jogo a ser requisitado.</param>
        /// <param name="jogoDto">Indica as propriedades do jogo.</param>
        /// <returns></returns>
        [HttpPut("{idJogo:guid}")]
        public async Task<ActionResult> AtualizarJogo([FromRoute] Guid idJogo, [FromBody] JogoDto jogoDto)
        {
            try
            {
                await _jogoService.Atualizar(idJogo, jogoDto);
                return Ok();
            }
            catch (JogoJaCadastradoException)
            {
                return NotFound("Este jogo não existe.");
            }
        }

        /// <summary>
        /// Atualizar apenas o preco de um jogo.
        /// </summary>
        /// <param name="idJogo">Indica o id do jogo a ser requisitado.</param>
        /// <param name="preco">Indica o preco a ser atualizado.</param>
        /// <returns></returns>
        [HttpPatch("{idJogo:guid}/preco/{preco:double}")]
        public async Task<ActionResult> AtualizarPreco([FromRoute] Guid idJogo, [FromRoute] double preco)
        {
            try
            {
                await _jogoService.AtualizarPreco(idJogo, preco);
                return Ok();
            }
            catch (Exception)
            {

               return NotFound("Este jogo não existe.");
            }
        }

        /// <summary>
        /// Remover um jogo.
        /// </summary>
        /// <param name="idJogo">Indica o id do jogo a ser requisitado.</param>
        /// <returns></returns>
        [HttpDelete("{idJogo:guid}")]
        public async Task<ActionResult> RemoverJogo([FromRoute] Guid idJogo)
        {
            try
            {
                await _jogoService.Remover(idJogo);
                return Ok();
            }
            catch (Exception)
            {
                return NotFound("Este jogo não existe.");
            }
        }
    }
}
