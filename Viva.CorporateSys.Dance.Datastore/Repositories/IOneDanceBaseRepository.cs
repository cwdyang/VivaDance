using System;
using Viva.CorporateSys.Dance.Datastore.Contexts;

namespace Viva.CorporateSys.Dance.Datastore.Repositories
{
    public interface IOneDanceBaseRepository:IDisposable
    {
        IOneDanceMainContext DbContext { get; set; }
    
    }
}