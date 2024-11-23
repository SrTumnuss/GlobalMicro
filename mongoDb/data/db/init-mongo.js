db = connect("mongodb://localhost:27017/admin");

db.createUser({
    user: "energia",
    pwd: "1234",
    roles: [{ role: "readWrite", db: "EnergyMonitorDB" }]
});

db = db.getSiblingDB('EnergyMonitorDB');

db.createCollection('consumo');

db.consumo.insertMany([
    {
        nomeDoEletronico: "celular",
        horaMonitoramento: new Date(),
        energiaConsumida: 150,
        potenciaDoAparelho: 100,
        tempoDeUso: 3
    }
]);
