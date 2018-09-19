using System.Collections.Generic;
using System.Threading.Tasks;
using TheBigScreen.Entities.Entities;

namespace TheBigScreen.Entities.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();

        Task<User> GetByIdAsync(string id);
        
        void Update(User entity);

        Task<bool> DeleteAsync(string id);

        Task<User> CreateOrUpdateAsync(string userId, User user);
    }
}