using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace TesteItau
{
    public class Cotacao
    {
        [Key]
        [Column("pk_cotacao")]
        public int PkCotacao { get; set; }
        [Column("fk_ativo")]
        public int FkAtivo { get; set; }
        [Column("vl_preco_unitario")]
        public decimal VlPrecoUnitario { get; set; }
        [Column("dt_cotacao")]
        public DateTime DtCotacao { get; set; }
        [ForeignKey("FkAtivo")]
        public Ativo? Ativo { get; set; }
    }
}
