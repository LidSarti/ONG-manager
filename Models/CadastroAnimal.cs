using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONGManager.Models;

public class CadastroAnimal
{
    [Key]
    [Required]
    public int id { get; set; } = 0;
    [Required]
    [StringLength(200)]
    public string nome { get; set; } = string.Empty;
    [Required]
    [StringLength(200)]
    public string raca { get; set; } = string.Empty;
    [Required]
    public int idade { get; set; } = 0;
    [Required]
    public int disponivel { get; set; } = 0;
    public string? biografia { get; set; }
    [Required]
    public string cidade { get; set; } = string.Empty;
    [Required]
    public string estado { get; set; } = string.Empty;
    [Required]
    public int tipo_animal { get; set; } = 0;
    [ForeignKey("tipo_animal")]
    public TipoAnimal? TipoAnimal { get; set; }
    [Required]
    public int porte_animal { get; set; } = 0;
    [ForeignKey("porte_animal")]
    public Porte? Porte { get; set; }
    public virtual ICollection<Imagem>? Imagens { get; set; } = [];

}
