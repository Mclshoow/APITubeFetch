using System.Collections.Generic;
using System.Threading.Tasks;
using APITubefetch.Models;

namespace APITubefetch.Interfaces.ITodoRepository
{
    public interface ITodoRepository
    {
        public Task<List<Todo>> GetListAsync();
        public Task<Todo> GetByIdAsync(int id);
        public Task<Todo> PostAsync(Todo todo);
        public Task<Todo> PutAsync(Todo todo);
        public Task<Todo> DeleteAsync(Todo todo);
    }
}