using ExpPayment.Base.Response;
using ExpPayment.Schema;
using MediatR;

namespace ExpPayment.Business.Cqrs;

public record CreateTokenCommand(TokenRequest Model) : IRequest<ApiResponse<TokenResponse>>;
