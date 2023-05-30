using Microsoft.AspNetCore.Mvc;
using System.Linq;
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
            _database = DbConnection.Client.GetDatabase("pesquisa");
            _collection = _database.GetCollection<Pesquisa>("pesquisas-collection");
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
        [HttpGet("/api/pesquisaPorIdDeLoja/{lojaId}")]
        public IActionResult GetPesquisaPorLoja(int lojaId)
        {
            
            var results = _collection.Find(p => p.Lojas.Contains(lojaId)).ToList();


            return Ok(results);
        }
        [HttpGet("api/getPesquisasValidas")]
        public IActionResult GetPesquisasValidas()
        {
            DateTime dataAtual = DateTime.UtcNow;

            var pesquisasValidas = _collection.Find(p => p.StartDate <= dataAtual && p.EndDate >= dataAtual).ToList();

            return Ok(pesquisasValidas);
        }

        [HttpGet("api/getPesquisasValidas/{id}")]
        public IActionResult GetPesquisasValidas(int id)
        {
            DateTime dataAtual = DateTime.UtcNow;

            var pesquisasPorId = _collection.Find(p => p.Lojas.Contains(id)).ToList();

            var pesquisasValidas = from pesquisa in pesquisasPorId
                                   where pesquisa.StartDate <= dataAtual && pesquisa.EndDate >= dataAtual
                                   select pesquisa;

        /*
         _collection.Find(p => p.StartDate <= d && p.EndDate >= d).ToList();
         */
            return Ok(pesquisasValidas);
        }


    }
}
