namespace ProjetoMEI.API.Entities
{
    public class Cliente
    {
        public Guid Id { get; set; }

        public int CNPJ { get; set; }
        public string Name { get; set; }
    }
}
