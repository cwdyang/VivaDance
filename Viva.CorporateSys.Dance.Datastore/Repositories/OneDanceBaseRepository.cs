using System;
using Viva.CorporateSys.Dance.Datastore.Contexts;

namespace Viva.CorporateSys.Dance.Datastore.Repositories
{
    public class OneDanceBaseRepository : IOneDanceBaseRepository
    {

        public IOneDanceMainContext DbContext { get; set; }

        public OneDanceBaseRepository()
        {
            DbContext = new OneDanceMainContext();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    DbContext.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}