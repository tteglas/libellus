using Libellus.DataAccess.Domain;
using System.Collections.Generic;
using PagedList;

namespace Libellus.Models
{
    public class ProjectViewModel
    {
        public ProjectViewModel()
        {
            AvailableProfessors = new List<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Progress { get; set; }
        public string Status { get; set; }
        public string SelectedProfessorId { get; set; }
        public PagedList<Project> AllProjects { get; set; }
        public PagedList<Project> UserProjects { get; set; }
        public PagedList<Project> UserInProgressProjects { get; set; }
        public List<User> AvailableProfessors { get; set; }
        public Subscription Subscription { get; set; }

        public User User { get; set; }
    }
}