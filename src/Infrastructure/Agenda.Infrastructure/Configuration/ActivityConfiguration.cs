using Agenda.Domain.Entities;
using Agenda.Infrastructure.Configuration.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agenda.Infrastructure.Configuration;

public class ActivityConfiguration : BaseEntityConfiguration<Activity>
{
    public override void Configure(EntityTypeBuilder<Activity> builder)
    {
        builder.ToTable("atividade");

        base.Configure(builder);

        builder.Property(a => a.Title)
            .HasColumnName("titulo")
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.Description)
            .HasColumnName("descricao")
            .HasMaxLength(500);

        builder.Property(a => a.Status)
            .HasColumnName("status_atividade")
            .IsRequired()
            .HasConversion<string>().HasMaxLength(20);

        builder.Property(a => a.Priority)
            .HasColumnName("prioridade")
            .IsRequired()
            .HasConversion<string>().HasMaxLength(20);

        builder.Property(a => a.DueDate)
            .HasColumnName("data_vencimento");

        builder.Property(a => a.ActualStartDate)
            .HasColumnName("data_inicio_real");

        builder.Property(a => a.ActualCompletionDate)
            .HasColumnName("data_conclusao_real");

        builder.Property(a => a.ActualCancellationDate)
            .HasColumnName("data_cancelamento_real");

        builder.Property(a => a.ElapsedSinceCreation)
            .HasColumnName("decorrido_desde_criaçao");

        builder.Property(a => a.TimeToStart)
            .HasColumnName("tempo_ate_inicio");

        builder.Property(a => a.FinalWorkedTime)
            .HasColumnName("tempo_ativo_total");

        builder.Property(a => a.DelayDuration)
            .HasColumnName("duracao_atraso");

        //Ignore
        builder.Ignore(a => a.ElapsedSinceCreationNow);
        builder.Ignore(a => a.WorkedDurationUntilNow);
        builder.Ignore(a => a.DelayDurationUntilNow);

        // Log
        builder.OwnsMany(a => a.TimeLogs, timeLogBuilder =>
        {
            timeLogBuilder.ToTable("atividade_logs_tempo");
            timeLogBuilder.WithOwner().HasForeignKey("atividade_Id");

            timeLogBuilder.HasKey("atividade_Id", "StartTime");

            timeLogBuilder.Property(tl => tl.StartTime)
                         .HasColumnName("inicio")
                         .IsRequired();

            timeLogBuilder.Property(tl => tl.EndTime)
                         .HasColumnName("fim");

            timeLogBuilder.Ignore(tl => tl.Duration);
            timeLogBuilder.Ignore(tl => tl.IsOpen);
        });
    }
}