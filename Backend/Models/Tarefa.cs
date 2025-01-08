using System.ComponentModel.DataAnnotations;

public class Tarefa
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O título é obrigatório.")]
    [MaxLength(100, ErrorMessage = "O título deve ter no máximo 100 caracteres.")]
    public string Titulo { get; set; }

    [MaxLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres.")]
    public string Descricao { get; set; }

    [Required(ErrorMessage = "O status é obrigatório.")]
    public string Status { get; set; }

    [Required(ErrorMessage = "A prioridade é obrigatória.")]
    public string Prioridade { get; set; }

    public DateTime? DataConclusao { get; set; }
}
