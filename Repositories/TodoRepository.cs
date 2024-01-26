using System.Collections.Generic;
using System.Threading.Tasks;
using APITubefetch.Data;
using APITubefetch.Interfaces.ITodoRepository;
using APITubefetch.Models;
using Microsoft.EntityFrameworkCore;

namespace APITubefetch.Repositories.TodoRepository
{
    public class TodoRepository : ITodoRepository
    {
        private readonly AppDbContext _context;
        public TodoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Todo>> GetListAsync()
        {
            /* Aqui é feita uma consulta ao banco de dados usando o contexto AppDbContext. Todos é provavelmente um DbSet dentro do contexto e AsNoTracking() é usado para desabilitar o rastreamento de mudanças das entidades retornadas.
            ToListAsync() realiza a consulta e retorna a lista de todos os objetos da tabela 'Todos'. */

            return await _context
                        .Todos
                        .AsNoTracking()
                        .ToListAsync();
        }

        public async Task<Todo> GetByIdAsync(int id)
        {
            return await _context
                        .Todos
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Todo> PostAsync(Todo todo)
        {
            // AddAsync salva na memória
            await _context
                .Todos
                .AddAsync(todo);

            // SaveChangesAsync salva no banco
            await _context
                .SaveChangesAsync();

            return todo;
        }

        public async Task<Todo> PutAsync(Todo todo)
        {
            // Update salva a edição na memória
            _context.Todos.Update(todo);

            // SaveChangesAsync salva no banco
            await _context.SaveChangesAsync();

            return todo;
        }

        public async Task<Todo> DeleteAsync(Todo todo)
        {
            // Remove na memória
            _context.Todos.Remove(todo);

            // SaveChangesAsync salva no banco
            await _context.SaveChangesAsync();

            return todo;
        }
    }
}