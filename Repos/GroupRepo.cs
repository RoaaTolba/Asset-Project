using AssetsPro.Interfaces;
using AssetsPro.Models;

namespace AssetsPro.Repos
{
    public class GroupRepo : IGroupRepo
    {
        MyDbContext context = new MyDbContext();

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Group> GetAllGroups()
        {
            return context.Groups.ToList();
        }

        public Group GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Group newGroup)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, Group newGroup)
        {
            throw new NotImplementedException();
        }
    }
}
