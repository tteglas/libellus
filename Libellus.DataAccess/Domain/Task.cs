using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libellus.DataAccess.Domain
{
    public class Task
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int ProjectId { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public decimal Progress { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual Project Project { get; set; }
    }
}
