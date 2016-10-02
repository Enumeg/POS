using System;

namespace POS.Domain.Infrastructure
{
    public interface IInitializer : IDisposable
    {
        void Initialize(PosContext context);
        void SaveChanges();
    }
}
