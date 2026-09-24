using JobApplication.Application.Interfaces;
using JobApplication.Domain.Entities;

namespace JobApplication.Application.Services
{
    public class JobMaintenanceService
    {
        private readonly IRepository<Job> _jobRepository;

        public JobMaintenanceService(
            IRepository<Job> jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public void CloseInactiveJobs()
        {
            var cutoffDate = DateTime.UtcNow.AddDays(-30);

            var jobs = _jobRepository
                .Get()
                .Where(j =>
                    j.IsActive &&
                    j.CreatedAt <= cutoffDate)
                .ToList();

            foreach (var job in jobs)
            {
                job.IsActive = false;

                _jobRepository.Update(job);
            }

            if (jobs.Any())
            {
                _jobRepository.SaveChangesAsync()
                    .GetAwaiter()
                    .GetResult();
            }
        }
    }
}