

using GrpcServer.Model;
using Microsoft.EntityFrameworkCore;

namespace GrpcServer.DatabaseContext;

public class EntityModelContext: DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Data Source=(localdb)\localdb;Initial Catalog=Speakers;Integrated Security=True;");
    }

    public DbSet<ConferenceDataModel> ConferenceDataModel { get; set; }
}


public class AppDbContext : DbContext
{
    public DbSet<ConferenceDataModel> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Data Source=(localdb)\localdb;Initial Catalog=Speakers;Integrated Security=True;");
    }
}


