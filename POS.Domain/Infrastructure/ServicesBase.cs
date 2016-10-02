using System;

namespace POS.Domain.Infrastructure
{
    public class ServicesBase : IDisposable 
    {
        protected PosContext context;
        protected CRUDService crudService;
        #region Implementation of IInitializer
        public void Initialize(PosContext _context)
        {
            context = _context;
            crudService = new CRUDService(context);
        }
        #endregion
        public void SaveChanges()
        {
            context.SaveChanges();
        }
        // Public implementation of Dispose pattern callable by consumers. 
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        // Flag: Has Dispose already been called? 
        private bool disposed = false;
        // Protected implementation of Dispose pattern. 
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                context.Dispose();
            }

            disposed = true;
        }
    }
}
