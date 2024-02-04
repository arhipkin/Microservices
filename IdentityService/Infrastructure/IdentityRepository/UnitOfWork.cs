using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IdentityRepository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IdentityAppDBContext _identityAppDBContext;

        public UnitOfWork(
            IdentityAppDBContext identityAppDBContext
            )
        {
            _identityAppDBContext = identityAppDBContext;
        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            await _identityAppDBContext.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            await _identityAppDBContext.CommitAsync(cancellationToken);
        }

        public async Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            await _identityAppDBContext.RollbackAsync(cancellationToken);
        }
    }
}
