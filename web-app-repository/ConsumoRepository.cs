using MongoDB.Driver;
using MongoDB.Bson;
using web_energy_domain;

namespace web_app_repository
{
    public class ConsumoRepository : IConsumoRepository
    {
        private readonly IMongoCollection<Consumo> _collection;

        public ConsumoRepository(MongoSettings mongoSettings)
        {
            var client = new MongoClient(mongoSettings.ConnectionString);
            var database = client.GetDatabase(mongoSettings.DatabaseName);
            _collection = database.GetCollection<Consumo>("Consumo");
        }

        public async Task<IEnumerable<Consumo>> ListarConsumos()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<Consumo> ObterConsumo(string id)
        {
            // Convertendo o ID de string para ObjectId
            if (ObjectId.TryParse(id, out var objectId))
            {
                return await _collection.Find(c => c.Id == objectId).FirstOrDefaultAsync();
            }
            return null;  // Se o ID não for válido, retorna null
        }

        public async Task SalvarConsumo(Consumo consumo)
        {
            // Inserindo o consumo, o MongoDB vai gerar automaticamente um ObjectId
            await _collection.InsertOneAsync(consumo);
        }

        public async Task AtualizarConsumo(string id, Consumo consumoAtualizado)
        {
            // Convertendo o ID de string para ObjectId
            if (ObjectId.TryParse(id, out var objectId))
            {
                await _collection.ReplaceOneAsync(c => c.Id == objectId, consumoAtualizado);
            }
        }

        public async Task RemoverConsumo(string id)
        {
            // Convertendo o ID de string para ObjectId
            if (ObjectId.TryParse(id, out var objectId))
            {
                await _collection.DeleteOneAsync(c => c.Id == objectId);
            }
        }
    }
}
