using Microsoft.AspNetCore.Mvc;
using TodoApp.API.DTOs;
using TodoApp.API.Models;
using TodoApp.API.Services;

namespace TodoApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TarefasController : ControllerBase
    {
        private readonly ITarefaService _tarefaService;
        private readonly ILogger<TarefasController> _logger;

        public TarefasController(ITarefaService tarefaService, ILogger<TarefasController> logger)
        {
            _tarefaService = tarefaService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<TarefaResponseDto>>>> GetTarefas()
        {
            try
            {
                var tarefas = await _tarefaService.ObterTodasTarefasAsync();
                return Ok(ApiResponse<IEnumerable<TarefaResponseDto>>.Ok(tarefas));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter tarefas");
                return StatusCode(500, ApiResponse<IEnumerable<TarefaResponseDto>>.Fail("Erro interno do servidor"));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<TarefaResponseDto>>> GetTarefa(int id)
        {
            try
            {
                var tarefa = await _tarefaService.ObterTarefaPorIdAsync(id);

                if (tarefa == null)
                {
                    return NotFound(ApiResponse<TarefaResponseDto>.Fail("Tarefa não encontrada"));
                }

                return Ok(ApiResponse<TarefaResponseDto>.Ok(tarefa));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter tarefa com ID {Id}", id);
                return StatusCode(500, ApiResponse<TarefaResponseDto>.Fail("Erro interno do servidor"));
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<TarefaResponseDto>>> PostTarefa(TarefaCreateDto tarefaDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ApiResponse<TarefaResponseDto>.Fail("Dados inválidos"));
                }

                var novaTarefa = await _tarefaService.CriarTarefaAsync(tarefaDto);

                return CreatedAtAction(nameof(GetTarefa),
                    new { id = novaTarefa.Id },
                    ApiResponse<TarefaResponseDto>.Ok(novaTarefa, "Tarefa criada com sucesso"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar tarefa");
                return StatusCode(500, ApiResponse<TarefaResponseDto>.Fail("Erro interno do servidor"));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<TarefaResponseDto>>> PutTarefa(int id, TarefaUpdateDto tarefaDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ApiResponse<TarefaResponseDto>.Fail("Dados inválidos"));
                }

                if (id <= 0)
                {
                    return BadRequest(ApiResponse<TarefaResponseDto>.Fail("ID inválido"));
                }

                var tarefaAtualizada = await _tarefaService.AtualizarTarefaAsync(id, tarefaDto);

                if (tarefaAtualizada == null)
                {
                    return NotFound(ApiResponse<TarefaResponseDto>.Fail("Tarefa não encontrada"));
                }

                return Ok(ApiResponse<TarefaResponseDto>.Ok(tarefaAtualizada, "Tarefa atualizada com sucesso"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar tarefa com ID {Id}", id);
                return StatusCode(500, ApiResponse<TarefaResponseDto>.Fail("Erro interno do servidor"));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse>> DeleteTarefa(int id)
        {
            try
            {
                var resultado = await _tarefaService.ExcluirTarefaAsync(id);

                if (!resultado)
                {
                    return NotFound(ApiResponse.Fail("Tarefa não encontrada"));
                }

                return Ok(ApiResponse.Ok("Tarefa excluída com sucesso"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao excluir tarefa com ID {Id}", id);
                return StatusCode(500, ApiResponse.Fail("Erro interno do servidor"));
            }
        }

        [HttpPatch("{id}/concluida")]
        public async Task<ActionResult<ApiResponse>> PatchTarefaConcluida(int id, [FromBody] bool concluida)
        {
            try
            {
                var resultado = await _tarefaService.MarcarComoConcluidaAsync(id, concluida);

                if (!resultado)
                {
                    return NotFound(ApiResponse.Fail("Tarefa não encontrada"));
                }

                var mensagem = concluida ? "Tarefa marcada como concluída" : "Tarefa marcada como pendente";
                return Ok(ApiResponse.Ok(mensagem));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar status da tarefa com ID {Id}", id);
                return StatusCode(500, ApiResponse.Fail("Erro interno do servidor"));
            }
        }
    }
}