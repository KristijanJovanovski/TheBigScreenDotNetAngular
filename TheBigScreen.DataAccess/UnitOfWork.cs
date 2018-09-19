using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TheBigScreen.Entities.Interfaces;

namespace TheBigScreen.DataAccess
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly TheBigScreenDbContext _context;

        public UnitOfWork(TheBigScreenDbContext context)
        {
            this._context = context;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
