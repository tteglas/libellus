namespace Libellus.CommonConcerns.Constants
{
    public class CommonHelper
    {
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

        public enum FacultyRole
        {
            Professor = 1,
            Student = 2
        }
    }
}
