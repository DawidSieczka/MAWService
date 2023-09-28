using MAWService.Application.Common.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAWService.Application.Operations.Users.Queries;
public class GetUserByExternalIdQuery : IRequest
{

}
public class GetUserByExternalIdQueryHandler : IRequestHandler<GetUserByExternalIdQuery>
{
    public Task Handle(GetUserByExternalIdQuery request, CancellationToken cancellationToken)
    {
        throw new CustomException("dupa");
    }
}


