using Microsoft.AspNetCore.Mvc;
using TarefasApi.Models;

namespace TarefasApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TarefasController : ControllerBase
    {
        private static List<Tarefa> tarefas = new List<Tarefa>();

        [HttpGet]
        public ActionResult<IEnumerable<Tarefa>> Get()
        {
            return Ok(tarefas);
        }

        [HttpPost]
        public ActionResult<Tarefa> Post(Tarefa novaTarefa)
        {
            novaTarefa.Id = tarefas.Count + 1;
            tarefas.Add(novaTarefa);
            return CreatedAtAction(nameof(Get), new { id = novaTarefa.Id }, novaTarefa);
        }
    }
}
