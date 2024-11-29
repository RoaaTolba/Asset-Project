using AssetsPro.Models;
using AssetsPro.Repos;

namespace AssetsPro.Interfaces
{
    public interface IAttendanceRepo
    {
        public IEnumerable<Attendance> GetAll();
        public IEnumerable<Employee> GetEmpNames();
        public Attendance GetById(int id);
        public Task<bool> SaveRecords(List<Attendance> records);
        public void Edit (int id,Attendance newAttendance);
        public void DeleteById(int id);
    }
}
