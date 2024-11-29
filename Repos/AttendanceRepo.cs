using AssetsPro.Interfaces;
using AssetsPro.Models;
using Microsoft.AspNetCore.Mvc;

namespace AssetsPro.Repos
{
    public class AttendanceRepo : IAttendanceRepo
    {
        MyDbContext context = new MyDbContext();
        public void DeleteById(int id) => context.Attendances.Remove(GetById(id));
        

        public void Edit(int id, Attendance newAttendance)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Attendance> GetAll()
        {
            return context.Attendances.ToList();
        }

        public Attendance GetById(int id) => context.Attendances.FirstOrDefault(x => x.Id == id);

        public IEnumerable<Employee> GetEmpNames()
        {
            return context.Employees.ToList();
        }

        public async Task<bool> SaveRecords(List<Attendance> records)
        {
            try
            {
                foreach (var record in records)
                {
                    // Ensure record validity before adding
                    if (record != null)
                    {
                        context.Attendances.Add(record);
                    }
                }

                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                // Log the exception if necessary
                return false; // Return false if an error occurs while saving
            }
        }

    }
}
