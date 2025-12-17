using MediatR;
using ProductManagment.Application.Audits.Dtos;

namespace ProductManagment.Application.Audits.Queries
{
    public class GetAuditsQuery : IRequest<List<GetAuditsDto>>
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}
