using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PND.Areas.Identity.Data;

namespace PND.Areas.Identity.Data;

public class PNDContext : IdentityDbContext<PNDUser>
{
    public PNDContext(DbContextOptions<PNDContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new AppUser());
    }
}

public class AppUser : IEntityTypeConfiguration<PNDUser>
{
    public void Configure(EntityTypeBuilder<PNDUser> builder)
    {
        builder.Property(x=> x.FirstName).HasMaxLength(255); 
        builder.Property(x=> x.LastName).HasMaxLength(255); 
    }
}