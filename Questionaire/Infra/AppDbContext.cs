using Microsoft.EntityFrameworkCore;
using Questionaire.Domain;

namespace Questionaire.Infra
{
    public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<ProgramQuestion> Programs { get; set; }
    public DbSet<CandidateResponse> CandidateResponses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProgramQuestion>()
        .ToContainer(nameof(ProgramQuestion))
        .HasPartitionKey(c => c.Id)
        .HasNoDiscriminator();

        modelBuilder.Entity<CandidateResponse>()
        .ToContainer(nameof(CandidateResponse))
        .HasPartitionKey(c => c.Id)
        .HasNoDiscriminator();
    }
}
}