namespace LearningSystem.Services.Admin
{
    using System;
    using System.Threading.Tasks;

    public interface IAdminCourseService
    {
        // NB Do not user async void! (no control) => Task
        Task Create(
            string name,
            string description,
            DateTime startDate,
            DateTime endDate,
            string trainerId);
    }
}
