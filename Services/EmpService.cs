using AssetsPro.Models;

namespace AssetsPro.Services;
using AssetsPro.Interfaces;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

public class EmpService : IEmpService
{
    private readonly IEmpRepo empRepo;
    private readonly IGenderRepo genderRepo;
    public EmpService(IEmpRepo empRepo, IGenderRepo genderRepo)
    {
        this.empRepo = empRepo;
        this.genderRepo = genderRepo;
    }
    public IEnumerable<Employee> GetAllEmp()
    {
        return empRepo.GetAll().ToList();
    }
    public IEnumerable<Gender> GetAllGender()
    {
        IEnumerable<Gender> genders = genderRepo.GetAllGender();
        return genders;
    }
    public bool AddEmp(Employee newEmp)
    {
        if (newEmp == null)
        {
            return false;
        }
        else
        {
            empRepo.Insert(newEmp);
            return true;
        }
    }
    public Employee GetbyId(int id)
    {
        return empRepo.GetById(id);
    }

    public bool SaveEdit(int id, Employee employee)
    {
        if (employee == null)
        {
            return false;
        }
        else
        {
            empRepo.Update(id, employee);
            return true;
        }

    }

    public bool deleteEmp(int id)
    {
        if (id == 0)
        {
            return false;
        }
        else
        {
            if(empRepo.Delete(id))
                return true;
            return false;

        }
    }
}
