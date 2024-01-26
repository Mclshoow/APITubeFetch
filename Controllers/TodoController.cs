using System.Threading.Tasks;
using APITubefetch.Interfaces.ITodoBusiness;
using APITubefetch.ViewModels;
using Microsoft.AspNetCore.Mvc;

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
        private readonly ITodoBusiness _business;

        public TodoController(ITodoBusiness business)
        {
            _business = business;
        }

        /* Este atributo indica que o método Get responde a solicitações HTTP GET. 
            Este método será invocado quando uma solicitação GET for feita para o endpoint definido. */
        [HttpGet]

        /* Este atributo define o caminho do endpoint específico para este método. No caso, o endpoint 
            deste método será acessado através do caminho "v1/todos". */
        [Route(template: "todos")]

        /*  Este é o método Get que será chamado quando a requisição HTTP GET for feita no endpoint 
            "v1/todos". 
            Ele recebe um parâmetro context do tipo AppDbContext injetado por meio do serviço.*/
        public async Task<IActionResult> GetListAsync()
        {
            var todos = await _business.GetListAsync();
            /* Este método retorna uma resposta HTTP 200 (OK) juntamente com os dados retornados pela consulta ao banco de dados. Os dados são passados como conteúdo da resposta, representados pela lista de todos. */
            return Ok(todos);
        }

        // Get para trazer algo
        [HttpGet]
        [Route(template: "todos/{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var todo = await _business.GetByIdAsync(id);
            return todo == null ? NotFound() : Ok(todo);
        }

        // Post para adicionar.
        [HttpPost]
        [Route(template: "todos")]
        public async Task<IActionResult> PostAsync([FromBody] CreateTodoViewModel model)
        {
            // ModelState para valida o(s) campo(s) da ViewModel (model).
            if (!ModelState.IsValid)
                return BadRequest();

            var todo = await _business.PostAsync(model);

            return Ok(todo);
        }

        // Put modificar.
        [HttpPut]
        [Route(template: "todos/{id}")]
        public async Task<IActionResult> PutAsync(
                [FromBody] CreateTodoViewModel model,
                [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var todo = await _business.PutAsync(model, id);

            if (todo == null)
                return BadRequest();

            return Ok("Editado com sucesso!");
        }

        // Delete excluir.
        [HttpDelete]
        [Route(template: "todos/{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var todoDeletar = await _business.DeleteAsync(id);

            if (todoDeletar == null)
                return BadRequest();

            return Ok("Deletada com sucesso!");
        }
    }
}

