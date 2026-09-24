using System.Linq.Expressions;

namespace JobApplication.Application.Interfaces
{
    public interface IBackgroundJobScheduler
    {
        void Enqueue<T>(Expression<Action<T>> methodCall);

        void Schedule<T>(
            Expression<Action<T>> methodCall,
            TimeSpan delay);

        void Recurring<T>(
            string jobId,
            Expression<Action<T>> methodCall,
            string cronExpression);
    }
}