namespace Domain.Entities
{
    public abstract class Entity
    {
        public long Identificador { get; }
        public bool Ativo { get; private set; } = true;

        protected Entity()
        {
        }

        protected void Desativar()
        {
            Ativo = false;
        }
    }
}