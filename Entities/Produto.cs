namespace PesquisaMongoAPI.Entities
{
    public class Produto
    {
        public string Id { get; set; }
        public string Nome { get; set; }

        public bool Generico{ get; set;}

        public string Categoria { get; set;}

    }
}
