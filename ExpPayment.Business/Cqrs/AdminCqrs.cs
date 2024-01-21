using ExpPayment.Base.Response;
using ExpPayment.Data.Entity;
using ExpPayment.Schema;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpPayment.Business.Cqrs;

public record UserCreateCommand(ApplicationUserRequest Model) : IRequest<ApiResponse>;

public record CreateTokenCommand(TokenRequest Model) : IRequest<ApiResponse<TokenResponse>>;
