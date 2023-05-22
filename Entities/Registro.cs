namespace PesquisaMongoAPI.Entities
{
    public class Registro
    {
        public string LojaId {get; set;}

        public string ProdutoId { get; set;}

        public string ProdutoNome { get; set;}

        public decimal? PrecoRegular { get; set;}

        public decimal? PrecoPromo { get; set;}

        public DateTime? Data { get; set;}
    }
}
