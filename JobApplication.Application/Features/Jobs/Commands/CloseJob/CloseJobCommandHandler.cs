using JobApplication.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobApplication.Application.Features.Commands.Jobs.CloseJob
{
    public class CloseJobCommandHandler
        : IRequestHandler<CloseJobCommand, Unit>
    {
        private readonly IJobRepository _jobRepository;
        private readonly ICurrentUserService _currentUserService;

        public CloseJobCommandHandler(
            IJobRepository jobRepository,
            ICurrentUserService currentUserService)
        {
            _jobRepository = jobRepository;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(
            CloseJobCommand request,
            CancellationToken cancellationToken)
        {
            var currentUserId = _currentUserService.UserId;

            var job = await _jobRepository
                .Get()
                .FirstOrDefaultAsync(
                    x => x.Id == request.JobId,
                    cancellationToken);

            if (job == null)
            {
                throw new KeyNotFoundException("Job not found.");
            }

            if (job.RecruiterId != currentUserId)
            {
                throw new UnauthorizedAccessException(
                    "You are not allowed to close this job.");
            }

            job.IsActive = false;
            job.ClosedAt = DateTime.UtcNow;
            job.ClosedBy = currentUserId;

            _jobRepository.Update(job);

            await _jobRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}