

using GrpcServer.Model;
using Microsoft.EntityFrameworkCore;

namespace GrpcServer.DatabaseContext;

public class EntityModelContext: DbContext
{
    public DbSet<ConferenceDataModel> ConferenceDataModel { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //optionsBuilder.UseSqlServer(@"Data Source=(localdb)\localdb;Initial Catalog=Speakers;Integrated Security=True;");
        optionsBuilder.UseSqlServer(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=Speakers;Integrated Security=True;TrustServerCertificate=true");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ConferenceDataModel>().ToTable("SpeakerList");
    }
}