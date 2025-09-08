using Microsoft.EntityFrameworkCore;
using TodoApp.API.Data;
using TodoApp.API.DTOs;
using TodoApp.API.Models;

namespace TodoApp.API.Services
{
    public interface ITarefaService
    {
        Task<IEnumerable<TarefaResponseDto>> ObterTodasTarefasAsync();
        Task<TarefaResponseDto> ObterTarefaPorIdAsync(int id);
        Task<TarefaResponseDto> CriarTarefaAsync(TarefaCreateDto tarefaDto);
        Task<TarefaResponseDto> AtualizarTarefaAsync(int id, TarefaUpdateDto tarefaDto);
        Task<bool> ExcluirTarefaAsync(int id);
        Task<bool> MarcarComoConcluidaAsync(int id, bool concluida);
    }

    public class TarefaService : ITarefaService
    {
        private readonly AppDbContext _context;

        public TarefaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TarefaResponseDto>> ObterTodasTarefasAsync()
        {
            var tarefas = await _context.Tarefas
                .OrderByDescending(t => t.DataCriacao)
                .ToListAsync();

            return tarefas.Select(t => MapToDto(t));
        }

        public async Task<TarefaResponseDto> ObterTarefaPorIdAsync(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);

            if (tarefa == null)
                return null;

            return MapToDto(tarefa);
        }

        public async Task<TarefaResponseDto> CriarTarefaAsync(TarefaCreateDto tarefaDto)
        {
            var tarefa = new Tarefa
            {
                Titulo = tarefaDto.Titulo,
                Descricao = tarefaDto.Descricao,
                DataCriacao = DateTime.UtcNow,
                Concluida = false
            };

            _context.Tarefas.Add(tarefa);
            await _context.SaveChangesAsync();

            return MapToDto(tarefa);
        }

        public async Task<TarefaResponseDto> AtualizarTarefaAsync(int id, TarefaUpdateDto tarefaDto)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);

            if (tarefa == null)
                return null;

            tarefa.Titulo = tarefaDto.Titulo;
            tarefa.Descricao = tarefaDto.Descricao;

            // Atualizar status de conclusão se necessário
            if (tarefa.Concluida != tarefaDto.Concluida)
            {
                tarefa.Concluida = tarefaDto.Concluida;
                tarefa.DataConclusao = tarefaDto.Concluida ? DateTime.UtcNow : (DateTime?)null;
            }

            await _context.SaveChangesAsync();

            return MapToDto(tarefa);
        }

        public async Task<bool> ExcluirTarefaAsync(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);

            if (tarefa == null)
                return false;

            _context.Tarefas.Remove(tarefa);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> MarcarComoConcluidaAsync(int id, bool concluida)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);

            if (tarefa == null)
                return false;

            tarefa.Concluida = concluida;
            tarefa.DataConclusao = concluida ? DateTime.UtcNow : (DateTime?)null;

            await _context.SaveChangesAsync();

            return true;
        }

        private TarefaResponseDto MapToDto(Tarefa tarefa)
        {
            return new TarefaResponseDto
            {
                Id = tarefa.Id,
                Titulo = tarefa.Titulo,
                Descricao = tarefa.Descricao,
                DataCriacao = tarefa.DataCriacao,
                Concluida = tarefa.Concluida,
                DataConclusao = tarefa.DataConclusao
            };
        }
    }
}