using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using PesquisaMongoAPI.Entities;
using PesquisaMongoAPI.Db;
using MongoDB.Bson;

namespace PesquisaMongoAPI.Controllers
{
    [ApiController]
    public class RespostaController : ControllerBase
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<Resposta> _collection;
        public RespostaController()
        {
            _database = DbConnection.Client.GetDatabase("pesquisa");
            _collection = _database.GetCollection<Resposta>("respostas");
        }
        [Route("api/criarResposta")]
        [HttpPost]
        public IActionResult CriarResposta(Resposta resposta)
        {
            _collection.InsertOne(resposta);

            return Ok(resposta);
        }
        [HttpGet]
        [Route("api/getAll")]
        public IActionResult GetAll()
        {
            var queryResult = _collection.Find(r => true).ToList();

            if(queryResult is null)
            {
                return BadRequest();
            }

            return Ok(queryResult);
        }
        [HttpGet("api/getRespostaById/{id:length(24)}")]

        public IActionResult GetByPesquisaId(string id)
        {
            var queryResult = _collection.Find(r => r.Id == id).FirstOrDefault();

            if(queryResult is null)
            {
                return BadRequest();
            }

            return Ok(queryResult);

        }
        [HttpDelete("api/delete/{id}")]
        public IActionResult DeleteResposta(string id)
        {
            var deletedResposta = _collection.DeleteOne(r => r.Id == id);

            if(deletedResposta is null)
            {
                return BadRequest();
            }

            return NoContent();
        }
        [HttpPut("api/put/{id}")]
        public IActionResult AdicionarRegistro (string id, List<Registro> registros)
        {
            var filter = Builders<Resposta>.Filter.AnyEq("_id", new ObjectId(id));

           foreach(var registro in registros)
            {
                var update = Builders<Resposta>.Update.AddToSet("Registros", registro);
                _collection.UpdateOne(filter, update);
            }


            return NoContent();
        }
    }
}
