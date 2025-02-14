using AssetsPro.Interfaces;
using AssetsPro.Models;
using Microsoft.EntityFrameworkCore;

namespace AssetsPro.Repos
{
    public class EmpRepo : IEmpRepo
    {
        MyDbContext context = new MyDbContext();
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
            Employee emp = context.Employees.FirstOrDefault(x => x.Id == id);
            if (emp == null)
            {
                return false;
            }
            context.Employees.Remove(emp);
            context.SaveChanges();
            return true;
        }
    }
}
