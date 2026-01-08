using System;
using MediatR;

namespace CSharpClicker.UseCases.MakeAdmin;

public record MakeAdminCommand(Guid UserId) : IRequest<Unit>;
