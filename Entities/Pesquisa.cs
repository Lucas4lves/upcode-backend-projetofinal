using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace PesquisaMongoAPI.Entities
{
    public class Pesquisa
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Categoria { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public List<int> Lojas { get; set; }
        
        public List<Produto> Produtos { get; set; }
    }
}
