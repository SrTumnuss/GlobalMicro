using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.Extensions.Options;
using web_energy_domain;

namespace web_app_repository
{
    public class MongoConsumoRepository : IConsumoRepository
    {
        private readonly IMongoCollection<Consumo> _collection;

        public MongoConsumoRepository(IOptions<MongoSettings> mongoSettings)
        {
            var client = new MongoClient(mongoSettings.Value.ConnectionString);
            var database = client.GetDatabase(mongoSettings.Value.DatabaseName);
            _collection = database.GetCollection<Consumo>("Consumo");
        }

        public async Task<IEnumerable<Consumo>> ListarConsumos()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<Consumo> ObterConsumo(string id)
        {
            // Convertendo o string ID para ObjectId
            if (ObjectId.TryParse(id, out var objectId))
            {
                return await _collection.Find(c => c.Id == objectId).FirstOrDefaultAsync();
            }
            return null;  // Retorna null se o ID não for válido
        }

        public async Task SalvarConsumo(Consumo consumo)
        {
            // O MongoDB vai gerar automaticamente um ObjectId para o campo Id
            await _collection.InsertOneAsync(consumo);
        }

        public async Task AtualizarConsumo(string id, Consumo consumoAtualizado)
        {
            // Convertendo o string ID para ObjectId
            if (ObjectId.TryParse(id, out var objectId))
            {
                // Atualiza o consumo usando o ObjectId
                await _collection.ReplaceOneAsync(c => c.Id == objectId, consumoAtualizado);
            }
        }

        public async Task RemoverConsumo(string id)
        {
            // Convertendo o string ID para ObjectId
            if (ObjectId.TryParse(id, out var objectId))
            {
                // Remove o consumo usando o ObjectId
                await _collection.DeleteOneAsync(c => c.Id == objectId);
            }
        }
    }
}
