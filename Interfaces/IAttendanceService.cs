using AssetsPro.Models;

namespace AssetsPro.Interfaces
{
    public interface IAttendanceService
    {
        public IEnumerable<Attendance> GetAll();
        public IEnumerable<Employee> GetEmpNames();
        public Attendance GetById(int id);
        public Task<bool> SaveRecords(List<Attendance> records);
        public void Edit(int id, Attendance newAttendance);
        public void DeleteById(int id);
    }
}
