using AssetsPro.Interfaces;
using AssetsPro.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AssetsPro.Repos
{
    public class EmpRepo : IEmpRepo
    {
        private readonly MyDbContext context;

        public EmpRepo(MyDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<Employee> GetAll()
        {
            IEnumerable<Employee> employees = context.Employees.Include(e => e.Gender)
            .ToList() ?? new List<Employee>();
            return employees;
        }
        public Employee GetById(int id)=> context.Employees.Include(e => e.Gender).FirstOrDefault(x => x.Id == id);
        public void Insert(Employee newEmp)
        {
            context.Employees.Add(newEmp);
            context.SaveChanges();
        }
        public void Update(int id, Employee employee)
        {
            Employee newEmployee = context.Employees.FirstOrDefault(x => x.Id == id);
            
            newEmployee.Name = employee.Name;
            newEmployee.Email = employee.Email;
            newEmployee.Address = employee.Address;
            newEmployee.Salary = employee.Salary;
            newEmployee.ContactNumber = employee.ContactNumber;
            newEmployee.start_time = employee.start_time;
            newEmployee.end_time = employee.end_time;
            newEmployee.Gender = employee.Gender;
            newEmployee.BirthDate = employee.BirthDate;
            newEmployee.date_of_contract = employee.date_of_contract;
            newEmployee.Nationality = employee.Nationality;
            newEmployee.SSN = employee.SSN;
            newEmployee.Note = employee.Note;
            context.SaveChanges();
        }
        public bool Delete(int id)
        {
            var emp = context.Employees.FirstOrDefault(x => x.Id == id);
            if (emp == null)
            {
                return false;
            }

            var att = context.Attendances
                        .Where(e => e.Emp_Id == id)
                        .ToList();

            try
            {
                if (att != null)
                {
                    context.Attendances.RemoveRange(att); // Delete related attendances
                    context.Employees.Remove(emp); // Delete the employee
                    context.SaveChanges();
                }
            } catch (Exception ex) { Debug.WriteLine(ex.Message); return false; }
            return true;
        }
    }
}
