using Microsoft.Extensions.Options;
using MongoDB.Driver;
using web_energy_domain;

namespace web_energy_repository
{
    public class MongoConsumoRepository : IConsumoRepository
    {
        private readonly IMongoCollection<Consumo> _consumoCollection;

        public MongoConsumoRepository(IOptions<MongoSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _consumoCollection = database.GetCollection<Consumo>("consumos");
        }

        public async Task<IEnumerable<Consumo>> ListarConsumos()
        {
            return await _consumoCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Consumo?> ObterConsumo(string id)
        {
            return await _consumoCollection.Find(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task RegistrarConsumo(Consumo consumo)
        {
            await _consumoCollection.InsertOneAsync(consumo);
        }
    }
}
