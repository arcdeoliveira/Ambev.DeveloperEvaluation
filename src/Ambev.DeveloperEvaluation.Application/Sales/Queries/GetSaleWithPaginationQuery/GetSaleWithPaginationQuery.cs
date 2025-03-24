using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Queries.GetSaleWithPaginationQuery
{
    public class GetSaleWithPaginationQuery : IRequest<GetSaleWithPaginationQueryResponse>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public DateTime? InitialDate { get; set; }
        public DateTime? FinalDate { get; set; }
        public Guid? UserId { get; set; }
        public decimal? Total { get; set; }
        public SaleStatus? Status { get; set; }
        public int? AfiliateId { get; set; }
        public bool OnlyNotCanceledItem { get; set; } = true;
    }
}
