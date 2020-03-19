using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Context.Mappings
{
    public abstract class EntityMap<T> : IEntityTypeConfiguration<T> where T : Entity
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder
                .Property(x => x.Identificador)
                .IsRequired()
                .UseHiLo();

            builder
                .Property(p => p.Ativo)
                .IsRequired();

            builder
                .ToTable(GetTableName())
                .HasKey(x => x.Identificador);
        }

        protected abstract string GetTableName();
    }
}