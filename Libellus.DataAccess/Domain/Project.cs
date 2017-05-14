using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libellus.DataAccess.Domain
{
    public class Project
    {
        public Project()
        {
            Tasks = new HashSet<Task>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        public string AddedBy { get; set; }

        public decimal Progress { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public int DepartmentId { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public virtual Department Department { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
