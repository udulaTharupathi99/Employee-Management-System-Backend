using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpManagement.Core.Requests
{
    public class AddDepartmentRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
    }
}
