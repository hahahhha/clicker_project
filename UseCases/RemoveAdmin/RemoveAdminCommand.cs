using MediatR;

namespace CSharpClicker.UseCases.RemoveAdmin;

public record RemoveAdminCommand(Guid UserId) : IRequest<Guid>;
