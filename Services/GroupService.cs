using AssetsPro.Interfaces;
using AssetsPro.Models;

namespace AssetsPro.Services
{
    public class GroupService : IGroupService
    {
        MyDbContext context = new MyDbContext();

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Group> GetAllGroups() => context.Groups.ToList();

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
