using AssetsPro.Interfaces;
using AssetsPro.Models;
using AssetsPro.Repos;

namespace AssetsPro.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepo attendanceRepo;

        public AttendanceService(IAttendanceRepo attendanceRepo)
        {
            this.attendanceRepo = attendanceRepo;
        }
        public void DeleteById(int id) => attendanceRepo.DeleteById(id);


        public void Edit(int id, Attendance newAttendance)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Attendance> GetAll()
        {
            return attendanceRepo.GetAll();
        }

        public Attendance GetById(int id) => attendanceRepo.GetById(id);

        public IEnumerable<Employee> GetEmpNames()
        {
            return attendanceRepo.GetEmpNames();
        }

        public async Task<bool> SaveRecords(List<Attendance> Records)
        {
            if (Records == null || !Records.Any())
            {
                return false; // Handle empty or null input gracefully
            }

            try
            {
                return await attendanceRepo.SaveRecords(Records);
            }
            catch (Exception)
            {
                // Log the exception if needed
                return false; // Return false if any error occurs
            }

        }

    }
}
