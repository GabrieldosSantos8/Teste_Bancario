using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace TesteItau
{
    public class Ativo
    {
        [Key]
        [Column("pk_ativo")]
        public int PkAtivo { get; set; }
        [Column("ds_codigo")]
        public string DsCodigo { get; set; } = string.Empty;
        [Column("ds_nome")]
        public string DsNome { get; set; } = string.Empty;
        [NotMapped]
        public ICollection<Cotacao>? Cotacoes { get; set; }
        [NotMapped]
        public ICollection<Operacao>? Operacoes { get; set; }
        [NotMapped]
        public ICollection<Posicao>? Posicoes { get; set; }
    }
}
