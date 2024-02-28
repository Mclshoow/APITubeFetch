using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using APITubefetch.Interfaces.ITodoBusiness;
using APITubefetch.Interfaces.ITodoRepository;
using APITubefetch.Models;
using APITubefetch.Utils;
using APITubefetch.ViewModels;

namespace APITubefetch.Business.TodoBusiness
{
    public class TodoBusiness : ITodoBusiness
    {
        private readonly ITodoRepository _repository;

        public TodoBusiness(ITodoRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Todo>> GetListAsync()
        {
            return await _repository.GetListAsync();
        }

        public async Task<Todo> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<OperationResult<Todo>> PostAsync(CreateTodoViewModel model)
        {
            var result = new OperationResult<Todo>();

            try
            {
                var todo = new Todo
                {
                    Date = DateTime.Now,
                    Done = false,
                    Title = model.Title,
                    Name = model.Name,
                    Email = model.Email
                };

                await _repository.PostAsync(todo);

                result.Success = true;
                result.Data = todo;
                result.Message = "Tarefa adicionada com sucesso!";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao tentar adicionar tarefa: {ex.Message}");

                result.Success = false;
                result.Message = "Erro ao tentar adicionar tarefa!";
            }

            return result;
        }

        public async Task<OperationResult<Todo>> PutAsync(CreateTodoViewModel model, int id)
        {
            var result = new OperationResult<Todo>();
            var todoAchado = await _repository.GetByIdAsync(id);

            if (todoAchado == null)
                return null;

            try
            {
                todoAchado.Title = model.Title;
                var todoEditar = await _repository.PutAsync(todoAchado);

                result.Success = true;
                result.Message = "Tarefa atualizada com sucesso!";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao tentar atualizar tarefa: {ex.Message}" + $"ID Tarefa: {id}");

                result.Success = false;
                result.Message = "Erro ao tentar atualizar tarefa!";
            }

            return result;
        }

        public async Task<OperationResult<Todo>> DeleteAsync(int id)
        {
            var result = new OperationResult<Todo>();
            var todoAchado = await _repository.GetByIdAsync(id);

            if (todoAchado == null)
                return null;

            try
            {
                var todoDeletar = await _repository.DeleteAsync(todoAchado);

                result.Success = true;
                result.Message = "Tarefa deletada com sucesso!";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao tentar deletar tarefa: {ex.Message}" + $"ID Tarefa: {id}");

                result.Success = false;
                result.Message = "Erro ao tentar deletar tarefa!";
            }

            return result;
        }
    }
}