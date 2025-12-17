using MediatR;
using ProductManagment.Application.Audits.Dtos;
using ProductManagment.Application.Audits.Mappers;
using ProductManagment.Domain.Interfaces;

namespace ProductManagment.Application.Audits.Queries
{
    public class GetAuditsQueryHandler : IRequestHandler<GetAuditsQuery, List<GetAuditsDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAuditsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<GetAuditsDto>> Handle(GetAuditsQuery request, CancellationToken cancellationToken)
        {
            var audits = await _unitOfWork.ProductAudits.GetAllAsync();

            // Apply filters
            if (request.From.HasValue)
                audits = audits.Where(a => a.ChangeDate >= request.From.Value).ToList();

            if (request.To.HasValue)
                audits = audits.Where(a => a.ChangeDate <= request.To.Value).ToList();

            return audits.Select(AuditDtoMapper.ToDto).ToList();
        }
    }
}
