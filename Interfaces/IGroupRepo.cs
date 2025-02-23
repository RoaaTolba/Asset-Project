using AssetsPro.Models;

namespace AssetsPro.Interfaces
{
    public interface IGroupRepo
    {
        //public IEnumerable<Group> GetAllGroups();
        public Group GetById(int id);
        public void Update(int id, Group newGroup);
        public void Delete(int id);
        public void Insert(Group newGroup);
    }
}
