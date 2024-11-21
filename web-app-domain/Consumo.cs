using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace web_energy_domain
{
    public class Consumo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string NomeDoEletronico { get; set; }
        public DateTime HoraMonitoramento { get; set; }
        public double EnergiaConsumida { get; set; }
        public double PotenciaDoAparelho { get; set; }
        public double TempoDeUso { get; set; }
    }
}
