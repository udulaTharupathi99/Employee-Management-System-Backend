using AutoMapper;
using EmpManagement.Core.Models;
using EmpManagement.Core.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpManagement.BusinessLogic.Mappers
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<DepartmentModel, AddDepartmentRequest>()
           .ReverseMap();
        }
    }
}
