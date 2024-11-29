using AssetsPro.Models;

namespace AssetsPro.Interfaces
{
    public interface IGenderRepo
    {
        IEnumerable<Gender> GetAllGender();
    }
}
