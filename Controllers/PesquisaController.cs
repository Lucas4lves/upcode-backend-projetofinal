using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using PesquisaMongoAPI.Db;
using PesquisaMongoAPI.Entities;

namespace PesquisaMongoAPI.Controllers
{
    [ApiController]
    public class PesquisaController : ControllerBase
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<Pesquisa> _collection;

        public PesquisaController()
        {
            _database = DbConnection.Client.GetDatabase("pesquisas");
            _collection = _database.GetCollection<Pesquisa>("pesquisa-collection");
        }

        [HttpPost("/api/criar")]
        public IActionResult Post(Pesquisa pesquisa)
        {
            if (pesquisa == null)
            {
                return BadRequest();
            }

            _collection.InsertOne(pesquisa);

            return Ok(pesquisa);
        }

        [HttpGet("api/getAllPesquisas")]
        public IActionResult GetAll()
        {
            var result = _collection.Find(r => true).ToList();

            if(result is null)
            {
                return BadRequest();
            }

            return Ok(result);
        }
        

    }
}
