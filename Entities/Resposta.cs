using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PesquisaMongoAPI.Entities
{
    public class Resposta
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null;

        public string PesquisaId { get; set; }

        public List<Registro> Registros { get; set; }
    }
}
