using Microsoft.EntityFrameworkCore;

namespace TodoApp.API.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.Migrate();

            if (context.Tarefas.Any())
            {
                return;
            }

            var tarefas = new[]
            {
                new Models.Tarefa { Titulo = "Tarefa teste 1", Descricao = "Teste de Tarefa 1", Concluida = true },
                new Models.Tarefa { Titulo = "Tarefa teste 2", Descricao = "Tarefa teste 2" },
                new Models.Tarefa { Titulo = "Tarefa teste 3", Descricao = "Teste de Tarefa 3" }
            };

            context.Tarefas.AddRange(tarefas);
            context.SaveChanges();
        }
    }
}