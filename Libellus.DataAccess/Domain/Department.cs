using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libellus.DataAccess.Domain
{
    public class Department
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
        
        public virtual ICollection<User> User { get; set; }

        public virtual Faculty Faculty { get; set; }
    }
}
