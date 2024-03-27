using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Session1_DAL_.Models
{
    // Model
    public class Department : ModelBase
    {
       
        [Required (ErrorMessage = "Name is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Code is Required")]
        public string Code { get; set; }

        [Display(Name = "Date Of Creation")] 
        public DateTime DateOfCreation { get; set; }

        // Navigational Proberty => [Many]
        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
