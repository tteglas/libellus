using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libellus.DataAccess.Domain
{
    public class FacultyRole
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Type { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
