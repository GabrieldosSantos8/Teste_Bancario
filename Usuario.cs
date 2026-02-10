using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TesteItau
{
    public class Usuario
    {
        [Key]
        [Column("pk_usuario")]
        public int PkUsuario { get; set; }
        [Column("ds_nome")]
        public string DsNome { get; set; } = string.Empty;
        [Column("ds_email")]
        public string DsEmail { get; set; } = string.Empty;
        [Column("vl_perc_corretagem")]
        public decimal VlPercCorretagem { get; set; }
        [NotMapped]
        public ICollection<Operacao>? Operacoes { get; set; }
        [NotMapped]
        public ICollection<Posicao>? Posicoes { get; set; }
    }
}
