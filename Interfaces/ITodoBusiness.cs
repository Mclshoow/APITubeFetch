using System.Collections.Generic;
using System.Threading.Tasks;
using APITubefetch.Models;
using APITubefetch.Utils;
using APITubefetch.ViewModels;

namespace APITubefetch.Interfaces.ITodoBusiness
{
    public interface ITodoBusiness
    {
        public Task<List<Todo>> GetListAsync();
        public Task<Todo> GetByIdAsync(int id);
        public Task<OperationResult<Todo>> PostAsync(CreateTodoViewModel model);
        public Task<OperationResult<Todo>> PutAsync(CreateTodoViewModel model, int id);
        public Task<OperationResult<Todo>> DeleteAsync(int id);
    }
}