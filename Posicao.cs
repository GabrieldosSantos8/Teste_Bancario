using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TesteItau
{
    public class Posicao
    {
        [Key]
        [Column("pk_posicao")]
        public int PkPosicao { get; set; }
        [Column("fk_usuario")]
        public int FkUsuario { get; set; }
        [Column("fk_ativo")]
        public int FkAtivo { get; set; }
        [Column("nr_quantidade_total")]
        public int NrQuantidadeTotal { get; set; }
        [Column("vl_preco_medio")]
        public decimal VlPrecoMedio { get; set; }
        [Column("vl_lucro_prejuizo")]
        public decimal VlLucroPrejuizo { get; set; }
        [NotMapped]
        public Usuario? Usuario { get; set; }
        [NotMapped]
        public Ativo? Ativo { get; set; }
    }
}
