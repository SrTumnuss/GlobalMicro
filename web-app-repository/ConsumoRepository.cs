using Dapper;
using MySqlConnector;
using web_energy_domain;

namespace web_energy_repository
{
    public class ConsumoRepository : IConsumoRepository
    {
        private readonly MySqlConnection _connection;

        public ConsumoRepository()
        {
            string connectionString = "Server=localhost;Database=energia;User=root;Password=123;";
            _connection = new MySqlConnection(connectionString);
        }

        public async Task<IEnumerable<Consumo>> ListarConsumos()
        {
            await _connection.OpenAsync();
            string query = @"SELECT Id, NomeDoEletronico, HoraMonitoramento, EnergiaConsumida, PotenciaDoAparelho, TempoDeUso 
                             FROM consumos;";
            var consumos = await _connection.QueryAsync<Consumo>(query);
            await _connection.CloseAsync();
            return consumos;
        }

        public async Task RegistrarConsumo(Consumo consumo)
        {
            await _connection.OpenAsync();
            string sql = @"INSERT INTO consumos (NomeDoEletronico, HoraMonitoramento, EnergiaConsumida, PotenciaDoAparelho, TempoDeUso) 
                           VALUES (@NomeDoEletronico, @HoraMonitoramento, @EnergiaConsumida, @PotenciaDoAparelho, @TempoDeUso);";
            await _connection.ExecuteAsync(sql, consumo);
            await _connection.CloseAsync();
        }

        public async Task<Consumo?> ObterConsumo(int id)
        {
            await _connection.OpenAsync();
            string sql = @"SELECT Id, NomeDoEletronico, HoraMonitoramento, EnergiaConsumida, PotenciaDoAparelho, TempoDeUso 
                           FROM consumos WHERE Id = @id;";
            var consumo = await _connection.QueryFirstOrDefaultAsync<Consumo>(sql, new { id });
            await _connection.CloseAsync();
            return consumo;
        }
    }
}
