using AssetsPro.Models;

namespace AssetsPro.Interfaces
{
    public interface IEmpService
    {
        public IEnumerable<Employee> GetAllEmp();
        public Employee GetbyId(int id);
        public IEnumerable<Gender> GetAllGender();
        public bool AddEmp(Employee newEmp);
        public bool deleteEmp(int id);
        public bool SaveEdit(int id,Employee employee);

    }
}
