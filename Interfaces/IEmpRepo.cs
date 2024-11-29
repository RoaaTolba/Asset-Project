using AssetsPro.Models;

namespace AssetsPro.Interfaces
{
    public interface IEmpRepo
    {
        public IEnumerable<Employee> GetAll();
        public Employee GetById(int id);
        public void Update(int id, Employee employee);
        public bool Delete(int id);
        public void Insert(Employee newDept);
    }
}
