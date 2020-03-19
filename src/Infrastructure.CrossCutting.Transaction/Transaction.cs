using Infrastructure.CrossCutting.Transaction.Interfaces;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Data.Common;

namespace Infrastructure.CrossCutting.Transaction
{
    public class Transaction : ITransactions
    {
        private readonly DbTransaction _dbTransaction;

        public Transaction(IServiceProvider serviceProvider)
        {
            _dbTransaction = serviceProvider.GetService<DbTransaction>();
        }

        public void Commit()
        {
            _dbTransaction.Commit();
        }

        public void Dispose()
        {
            _dbTransaction.Dispose();
        }
    }
}