using MongoDB.Driver;
using Microsoft.Extensions.Options;
using web_energy_domain;

namespace web_app_repository
{
    public class ConsumoRepository : IConsumoRepository
    {
        private readonly IMongoCollection<Consumo> _collection;

        public ConsumoRepository(IOptions<MongoSettings> mongoSettings)
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
            return await _collection.Find(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task SalvarConsumo(Consumo consumo)
        {
            await _collection.InsertOneAsync(consumo);
        }

        public async Task AtualizarConsumo(string id, Consumo consumoAtualizado)
        {
            await _collection.ReplaceOneAsync(c => c.Id == id, consumoAtualizado);
        }

        public async Task RemoverConsumo(string id)
        {
            await _collection.DeleteOneAsync(c => c.Id == id);
        }
    }
}
