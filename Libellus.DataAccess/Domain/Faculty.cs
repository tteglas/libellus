using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libellus.DataAccess.Domain
{
    public class Faculty
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
        
        public ICollection<Department> Departments { get; set; }
    }
}
