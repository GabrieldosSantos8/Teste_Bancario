using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace TesteItau
{
    public class Operacao
    {
        [Key]
        [Column("pk_operacao")]
        public int PkOperacao { get; set; }
        [Column("fk_usuario")]
        public int FkUsuario { get; set; }
        [Column("fk_ativo")]
        public int FkAtivo { get; set; }
        [Column("nr_quantidade")]
        public int NrQuantidade { get; set; }
        [Column("vl_preco_unitario")]
        public decimal VlPrecoUnitario { get; set; }
        [Column("tp_operacao")]
        public string TpOperacao { get; set; } = string.Empty;
        [Column("vl_corretagem")]
        public decimal VlCorretagem { get; set; }
        [Column("dt_operacao")]
        public DateTime DtOperacao { get; set; }
        [ForeignKey("FkUsuario")]
        [NotMapped]
        public Usuario? Usuario { get; set; }
        [ForeignKey("FkAtivo")]
        [NotMapped]
        public Ativo? Ativo { get; set; }
    }
}
