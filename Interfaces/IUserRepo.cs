using AssetsPro.Models;

namespace AssetsPro.Interfaces
{
    public interface IUserRepo
    {
        public IEnumerable<User> GetAllGroups();
        public User GetById(int id);
        public void Update(int id, User newUser);
        public void Delete(int id);
        public void Insert(User newUser);
    }
}
