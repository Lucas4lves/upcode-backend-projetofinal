using PesquisaMongoAPI.Entities;
using PesquisaMongoAPI.Db;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Bson;

namespace PesquisaMongoAPI.Controllers
{
    [ApiController]
    public class AggregationController : ControllerBase
    {
        private readonly IMongoCollection<Pesquisa> _pesquisas;
        private readonly IMongoCollection<Resposta> _respostas;
        private readonly IMongoDatabase _database;

        public AggregationController()
        {
            _database = DbConnection.Client.GetDatabase("pesquisas");
            _pesquisas = _database.GetCollection<Pesquisa>("pesquisa-collection");
            _respostas = _database.GetCollection<Resposta>("respostas");
        }

        [HttpGet("/api/respostaPorPesquisa/{pesquisaId}")]
        public IActionResult Get(string pesquisaId)
        {
            var results = _respostas.Find(r => r.PesquisaId == pesquisaId).ToList();

            if(results is null)
            {
                return BadRequest();
            }

            return Ok(results);

        }
    }
}
