using System;

namespace POS.Domain.Infrastructure
{
    public class ServicesBase : IDisposable
    {
        protected PosContext Context;
        protected CrudService CrudService;
        public ServicesBase(PosContext context)
        {
            Context = context;
            CrudService = new CrudService(context);
        }
        // Public implementation of Dispose pattern callable by consumers. 
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        // Flag: Has Dispose already been called? 
        private bool _disposed;
        // Protected implementation of Dispose pattern. 
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                Context.Dispose();
            }

            _disposed = true;
        }
    }
}
