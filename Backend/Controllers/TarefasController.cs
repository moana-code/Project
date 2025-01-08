using GestorDeTarefasAPI.Data;
using GestorDeTarefasAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace GestorDeTarefasAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TarefasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TarefasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult ListarTarefas()
        {
            var tarefas = _context.Tarefas.ToList();
            return Ok(tarefas);
        }

        [HttpGet("{id}")]
        public IActionResult ObterTarefa(int id)
        {
            var tarefa = _context.Tarefas.Find(id);
            if (tarefa == null)
            {
                return NotFound("Tarefa não encontrada.");
            }
            return Ok(tarefa);
        }

        [HttpPost]
        public IActionResult CriarTarefa(Tarefa tarefa)
        {
            _context.Tarefas.Add(tarefa);
            _context.SaveChanges();
            return Ok("Tarefa criada com sucesso.");
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarTarefa(int id, Tarefa tarefaAtualizada)
        {
            var tarefa = _context.Tarefas.Find(id);
            if (tarefa == null)
            {
                return NotFound("Tarefa não encontrada.");
            }

            tarefa.Titulo = tarefaAtualizada.Titulo;
            tarefa.Descricao = tarefaAtualizada.Descricao;
            tarefa.Status = tarefaAtualizada.Status;
            tarefa.Prioridade = tarefaAtualizada.Prioridade;
            tarefa.DataConclusao = tarefaAtualizada.DataConclusao;

            _context.Tarefas.Update(tarefa);
            _context.SaveChanges();
            return Ok("Tarefa atualizada com sucesso.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeletarTarefa(int id)
        {
            var tarefa = _context.Tarefas.Find(id);
            if (tarefa == null)
            {
                return NotFound("Tarefa não encontrada.");
            }

            _context.Tarefas.Remove(tarefa);
            _context.SaveChanges();
            return Ok("Tarefa deletada com sucesso.");
        }
        using Microsoft.AspNetCore.Authorization;

[Authorize]
[HttpPost]
public IActionResult CriarTarefa(Tarefa tarefa)
{
    _context.Tarefas.Add(tarefa);
    _context.SaveChanges();
    return Ok("Tarefa criada com sucesso.");
}
[HttpPost("Registrar")]
public IActionResult Registrar([FromBody] Usuario usuario)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    if (_context.Usuarios.Any(u => u.Email == usuario.Email))
    {
        return BadRequest("E-mail já cadastrado.");
    }

    _context.Usuarios.Add(usuario);
    _context.SaveChanges();
    return Ok("Usuário registrado com sucesso.");
}
[HttpGet("PorUsuario")]
public IActionResult TarefasPorUsuario()
{
    var tarefasPorUsuario = _context.Tarefas
        .GroupBy(t => t.UsuarioId)
        .Select(g => new
        {
            UsuarioId = g.Key,
            QuantidadeTarefas = g.Count()
        });

    return Ok(tarefasPorUsuario);
}
[HttpGet("PrioridadeAlta")]
public IActionResult PrioridadeAlta()
{
    var tarefas = _context.Tarefas.Where(t => t.Prioridade == "Alta" && t.Status != "Concluída").ToList();
    return Ok(tarefas);
}
[HttpGet("PorStatus/{status}")]
public IActionResult ListarPorStatus(string status)
{
    // Filtra as tarefas pelo status informado
    var tarefas = _context.Tarefas.Where(t => t.Status == status).ToList();

    if (tarefas.Count == 0)
    {
        return NotFound("Nenhuma tarefa encontrada com o status especificado.");
    }

    return Ok(tarefas); // Retorna a lista de tarefas.
}
[HttpGet("TarefasPorUsuario")]
public IActionResult TarefasPorUsuario()
{
    var tarefasPorUsuario = _context.Tarefas
        .GroupBy(t => t.UsuarioId) // Agrupa as tarefas por id de usuário
        .Select(g => new
        {
            UsuarioId = g.Key,
            QuantidadeTarefas = g.Count() // Conta quantas tarefas tem por usuário
        }).ToList();

    if (tarefasPorUsuario.Count == 0)
    {
        return NotFound("Nenhum dado encontrado.");
    }

    return Ok(tarefasPorUsuario); // Retorna os resultados.
}
[HttpGet("TarefasPrioridadeAlta")]
public IActionResult TarefasPrioridadeAlta()
{
    var tarefas = _context.Tarefas
        .Where(t => t.Prioridade == "Alta" && t.Status != "Concluída")
        .ToList();

    if (tarefas.Count == 0)
    {
        return NotFound("Nenhuma tarefa de alta prioridade encontrada.");
    }

    return Ok(tarefas); // Retorna as tarefas com prioridade alta.
}
[HttpGet("TarefasPorPeriodo")]
public IActionResult TarefasPorPeriodo(DateTime dataInicio, DateTime dataFim)
{
    var tarefas = _context.Tarefas
        .Where(t => t.DataConclusao >= dataInicio && t.DataConclusao <= dataFim)
        .ToList();

    if (tarefas.Count == 0)
    {
        return NotFound("Nenhuma tarefa encontrada no período especificado.");
    }

    return Ok(tarefas); // Retorna as tarefas no período informado.
}
[HttpGet("ResumoTarefas")]
public IActionResult ResumoTarefas()
{
    var resumo = new
    {
        TotalTarefas = _context.Tarefas.Count(),
        TarefasPorStatus = _context.Tarefas
            .GroupBy(t => t.Status)
            .Select(g => new { Status = g.Key, Quantidade = g.Count() })
            .ToList(),
        TarefasPorPrioridade = _context.Tarefas
            .GroupBy(t => t.Prioridade)
            .Select(g => new { Prioridade = g.Key, Quantidade = g.Count() })
            .ToList(),
    };

    return Ok(resumo); // Retorna um relatório consolidado com totais e agrupamentos.
}

    }
}
