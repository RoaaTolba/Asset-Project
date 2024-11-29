using AssetsPro.Interfaces;
using AssetsPro.Models;

namespace AssetsPro.Repos
{
    public class GenderRepo : IGenderRepo
    {
        MyDbContext context = new MyDbContext();
        public IEnumerable<Gender> GetAllGender()
        {
            return context.Genders.ToList();
        }
    }
}
