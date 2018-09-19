using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TheBigScreen.Entities.Interfaces
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
