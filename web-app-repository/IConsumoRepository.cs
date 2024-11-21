using web_energy_domain;

namespace web_energy_repository
{
    public interface IConsumoRepository
    {
        Task<IEnumerable<Consumo>> ListarConsumos();
        Task<Consumo?> ObterConsumo(string id);
        Task RegistrarConsumo(Consumo consumo);
    }
}
