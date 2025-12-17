using ProductManagment.Application.Audits.Dtos;
using ProductManagment.Application.Common;
using ProductManagment.Application.Products.Dtos;
using ProductManagment.Domain.Entities;

namespace ProductManagment.Application.Audits.Mappers
{
    public class AuditDtoMapper
    {
        /// <summary>
        /// Converts a <see cref="ProductAudit"/> entity to its corresponding <see cref="GetAuditsDto"/>.
        /// </summary>
        /// <param name="audit">The phase entity to convert.</param>
        /// <returns>A <see cref="GetAuditsDto"/> representation of the entity.</returns>
        public static GetAuditsDto? ToDto(ProductAudit audit)
        {
            if (audit == null) return null;
            return new GetAuditsDto
            {
                Id = audit.Id,
                ProductId = audit.ProductId,
                UserId = audit.UserId,
                ChangeType = audit.ChangeType,
                ChangeDate = audit.ChangeDate,
                OldValues = audit.OldValues,
                NewValues = audit.NewValues
            };
        }
    }
}