using Microsoft.EntityFrameworkCore.Storage;
using OKA.Application;
using OKA.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OKA.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OKAStoreDbContext _context;
        private IDbContextTransaction _transaction;


        public UnitOfWork(OKAStoreDbContext context)
        {
            _context = context;
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            try
            {
                await _transaction?.CommitAsync();
            }
            finally
            {
                if (_transaction != null)
                {
                    await _transaction.DisposeAsync();
                }
                _transaction = null;
            }
        }

        public async Task RollbackAsync()
        {
            try
            {
                await _transaction?.RollbackAsync();
            }
            finally
            {
                if (_transaction != null)
                {
                    await _transaction.DisposeAsync();
                }
                _transaction = null;
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
            }
            await _context.DisposeAsync();
        }
    }
}
