using System;

namespace Infrastructure.CrossCutting.Transaction.Interfaces
{
    public interface ITransactions : IDisposable
    {
        void Commit();
    }
}