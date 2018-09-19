using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TheBigScreen.Entities.Entities;
using TheBigScreen.Entities.Interfaces;

namespace TheBigScreen.DataAccess.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly TheBigScreenDbContext _context;
        private DbSet<User> _entities;
        string errorMessage = string.Empty;

        public UserRepository(TheBigScreenDbContext context)
        {
            this._context = context;
            _entities = context.Set<User>();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _entities.AsNoTracking().ToListAsync<User>();
        }

        public async Task<User> GetByIdAsync(string id)
        {
            return await _entities.FirstOrDefaultAsync(e => e.Id == id);
        }
        
        public void Update(User entity)
        {
            _entities.Update(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var user = await GetByIdAsync(id);
            if (user != null)
            {
                _entities.Remove(user);
                return true;
            }
            return false;
        }

        public async Task<User> CreateOrUpdateAsync(string userId, User user)
        {
            var userInDb = await GetByIdAsync(userId);
            if (userInDb != null)
            {
                _entities.Update(user);
                await _context.SaveChangesAsync();

                return await GetByIdAsync(userId);
            }
            else
            {
                await _entities.AddAsync(user);
                return await GetByIdAsync(userId);
            }
        }
    }
}
