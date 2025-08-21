using Agenda.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agenda.Infrastructure.Configuration.Base;

public abstract class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity<TEntity>
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id");

        builder.Property(x => x.CreatedDate)
            .HasColumnName("data_criacao")
            .IsRequired();

        builder.Property(x => x.ChangedDate)
            .HasColumnName("data_atualizacao")
            .IsRequired(false);

        builder.Property(x => x.IsActive)
            .HasColumnName("ativo")
            .IsRequired();
    }
}