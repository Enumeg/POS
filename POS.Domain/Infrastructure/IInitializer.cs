using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace POS.Domain.Infrastructure
{
    public interface IInitializer : IDisposable
    {
        void Initialize(PosContext context);
        void SaveChanges();
    }
}
