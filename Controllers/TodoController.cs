using System;
using System.Threading.Tasks;
using APITubefetch.Data;
using APITubefetch.Models;
using APITubefetch.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APITubefetch.Controllers
{
    /* Essencialmente, este código representa um controlador de API com um endpoint HTTP GET 
        que retorna todos os itens da entidade 'Todos' do banco de dados 
        por meio do contexto AppDbContext. */
    [ApiController]

    /* [Route(template: "v1")]: Este atributo define o prefixo de rota para todos os endpoints dentro 
        desta  classe TodoController. 
        Aqui, todos os endpoints desta classe terão um prefixo de rota "v1". */
    [Route(template: "v1")]

    /* TodoController é um controlador de API. 
        Ele fornece comportamentos específicos para controladores da Web API no ASP.NET Core, como a inferência automática de validação de modelo e outros comportamentos. */
    public class TodoController : ControllerBase // este é um controlador
    {
        /* Este atributo indica que o método Get responde a solicitações HTTP GET. 
            Este método será invocado quando uma solicitação GET for feita para o endpoint definido. */
        [HttpGet]

        /* Este atributo define o caminho do endpoint específico para este método. No caso, o endpoint 
            deste método será acessado através do caminho "v1/todos". */
        [Route(template: "todos")]

        /*  Este é o método Get que será chamado quando a requisição HTTP GET for feita no endpoint 
            "v1/todos". 
            Ele recebe um parâmetro context do tipo AppDbContext injetado por meio do serviço.*/
        public async Task<IActionResult> GetAsync([FromServices] AppDbContext context)
        {
            /* Aqui é feita uma consulta ao banco de dados usando o contexto AppDbContext. Todos é provavelmente um DbSet dentro do contexto e AsNoTracking() é usado para desabilitar o rastreamento de mudanças das entidades retornadas.
                ToListAsync() realiza a consulta e retorna a lista de todos os objetos da tabela 'Todos'. */
            var todos = await context
                .Todos
                .AsNoTracking()
                .ToListAsync();

            /* Este método retorna uma resposta HTTP 200 (OK) juntamente com os dados retornados pela consulta ao banco de dados. Os dados são passados como conteúdo da resposta, representados pela lista de todos. */
            return Ok(todos);
        }

        // Get para trazer algo
        [HttpGet]
        [Route(template: "todos/{id}")]
        public async Task<IActionResult> GetByIdAsync(
                [FromServices] AppDbContext context,
                [FromRoute] int id)
        {
            var todo = await context
                .Todos
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            return todo == null
                ? NotFound()
                : Ok(todo);
        }

        // Post para adicionar.
        [HttpPost]
        [Route(template: "todos")]
        public async Task<IActionResult> PostAsync(
                [FromServices] AppDbContext context,
                [FromBody] CreateTodoViewModel model)
        {
            // ModelState para valida o(s) campo(s) da ViewModel (model).
            if (!ModelState.IsValid)
                return BadRequest();

            var todo = new Todo
            {
                Date = DateTime.Now,
                Done = false,
                Title = model.Title
            };

            try
            {
                // AddAsync salva na memória
                await context
                    .Todos
                    .AddAsync(todo);

                // SaveChangesAsync salva no banco
                await context
                    .SaveChangesAsync();

                return Created(uri: $"v1/todos/{todo.Id}", todo);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }

        // Put modificar.
        [HttpPut]
        [Route(template: "todos/{id}")]
        public async Task<IActionResult> PutAsync(
                [FromServices] AppDbContext context,
                [FromBody] CreateTodoViewModel model,
                [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var todo = await context
                .Todos
                .FirstOrDefaultAsync(x => x.Id == id);

            if (todo == null)
                return NotFound();

            try
            {
                todo.Title = model.Title;

                // Update salva a edição na memória
                context.Todos.Update(todo);

                // SaveChangesAsync salva no banco
                await context.SaveChangesAsync();

                return Ok("Editado com sucesso!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }

        // Delete excluir.
        [HttpDelete]
        [Route(template: "todos/{id}")]
        public async Task<IActionResult> DeleteAsync(
                [FromServices] AppDbContext context,
                [FromRoute] int id)
        {
            var todo = await context
                .Todos
                .FirstOrDefaultAsync(x => x.Id == id);

            if (todo == null)
                return NotFound();

            try
            {
                // Remove na memória
                context.Todos.Remove(todo);

                // SaveChangesAsync salva no banco
                await context.SaveChangesAsync();

                return Ok("Excluído com sucesso!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }

    }
}

