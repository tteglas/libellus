namespace Libellus.CommonConcerns.Constants
{
    public class CommonHelper
    {
        public const string ROLE_Professor = "Professor";
        public const string ROLE_Student = "Student";

        public enum ProjectStatus
        {
            New,
            WaitingApproval,
            InProgress,
            Completed
        }

        public static string ReadValueForProjectStatus(ProjectStatus projecStatus)
        {
            return projecStatus.ToString();
        }

        public enum UniversityRole
        {
            Professor,
            Student
        }
    }
}
