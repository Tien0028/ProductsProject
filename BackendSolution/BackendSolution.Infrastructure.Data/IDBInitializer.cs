using System;
using System.Collections.Generic;
using System.Text;

namespace BackendSolution.Infrastructure.Data
{
    public interface IDBInitializer
    {
        void SeedDB(BackendContext btx);
    }
}
