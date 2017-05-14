using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libellus.DataAccess.Domain
{
    public class Department
    {
        public Department()
        {
            Projects = new HashSet<Project>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public int FacultyId { get; set; }

        public virtual ICollection<Project> Projects { get; set; }

        public virtual Faculty Faculty { get; set; }
    }
}
