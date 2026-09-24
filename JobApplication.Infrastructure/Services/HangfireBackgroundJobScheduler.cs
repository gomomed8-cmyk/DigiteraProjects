using Hangfire;
using JobApplication.Application.Interfaces;
using System.Linq.Expressions;

namespace JobApplication.Infrastructure.Services
{
    public class HangfireBackgroundJobScheduler : IBackgroundJobScheduler
    {
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IRecurringJobManager _recurringJobManager;

        public HangfireBackgroundJobScheduler(
            IBackgroundJobClient backgroundJobClient,
            IRecurringJobManager recurringJobManager)
        {
            _backgroundJobClient = backgroundJobClient;
            _recurringJobManager = recurringJobManager;
        }

        public void Enqueue<T>(Expression<Action<T>> methodCall)
        {
            _backgroundJobClient.Enqueue<T>(methodCall);
        }

        public void Schedule<T>(
            Expression<Action<T>> methodCall,
            TimeSpan delay)
        {
            _backgroundJobClient.Schedule<T>(
                methodCall,
                delay);
        }

        public void Recurring<T>(
            string jobId,
            Expression<Action<T>> methodCall,
            string cronExpression)
        {
            _recurringJobManager.AddOrUpdate(
                jobId,
                methodCall,
                cronExpression);
        }
    }
}