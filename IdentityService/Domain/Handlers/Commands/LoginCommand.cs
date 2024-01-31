using Core.Models;
using Core.ViewModels;
using MediatR;

namespace Domain.Handlers.Commands
{
    public class LoginCommand : IRequest<ResultCommand<TokenInfo>>
    {
        public LoginData LoginData { get; set; }
    }
}
