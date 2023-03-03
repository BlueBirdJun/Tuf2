using Finbuckle.MultiTenant.Stores;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUF.Api.Infra.Persistence.Configuration;

namespace TUF.Api.Infra.Multitenancy;

public class TenantDbContext : EFCoreStoreDbContext<TufTenantInfo>
{
    public TenantDbContext(DbContextOptions<TenantDbContext> options)
           : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TufTenantInfo>().ToTable("Tenants", SchemaNames.MultiTenancy);
    }
}
