using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Common;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistance
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entity in ChangeTracker.Entries<EntityBase>())
            {
                switch (entity.State)
                {
                    case EntityState.Modified:
                        entity.Entity.LastModifiedDate = DateTime.Now;
                        entity.Entity.LastModifiedBy = "me";
                        break;
                    case EntityState.Added:
                        entity.Entity.CreatedDate = DateTime.Now;
                        entity.Entity.CreatedBy = "me";
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
