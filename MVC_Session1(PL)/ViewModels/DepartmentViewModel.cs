using MVC_Session1_DAL_.Models;
using System.ComponentModel.DataAnnotations;
using System;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace MVC_Session1_PL_.ViewModels
{

    public class DepartmentViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Code is Required Yaa Rahma!!")]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime DateOfCreation { get; set; }

        //Navigation
        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
