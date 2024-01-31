using Core.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IdentityRepository
{
    public class IdentityAppDBContext : IdentityDbContext<AppUser, AppRole, Guid>, IDisposable
    {
        private IDbContextTransaction _transaction;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public IdentityAppDBContext(DbContextOptions<IdentityAppDBContext> options) : base(options) { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>(b =>
            {
                b.HasOne(u => u.UserDetails).WithOne(ud => ud.User).HasForeignKey<UserDetails>(ud => ud.UserId).OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<UserDetails>(b =>
            {
                b.ToTable("UserDetails");
                b.HasKey(ud => ud.Id);
                b.HasIndex(ud => ud.UserId).HasDatabaseName("UserDetailsUserIdIndex").IsUnique();
                b.HasOne(ud => ud.User).WithOne(u => u.UserDetails).HasForeignKey<UserDetails>(ud => ud.UserId);
                b.Property(ud => ud.UserId);
                b.Property(ud => ud.Description).HasMaxLength(500);
            });
        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            _transaction = await Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await SaveChangesAsync(cancellationToken);
                await _transaction.CommitAsync(cancellationToken);
            }
            finally
            {
                await _transaction.DisposeAsync();
            }
        }

        public async Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            await _transaction.RollbackAsync(cancellationToken);
            await _transaction.DisposeAsync();
        }

        public override void Dispose()
        { 
            _transaction?.Dispose();
            base.Dispose();
        }
    }
}
