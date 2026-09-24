using MediatR;

namespace JobApplication.Application.Features.Commands.Jobs.CloseJob
{
    public record CloseJobCommand(int JobId) : IRequest<Unit>;
}
