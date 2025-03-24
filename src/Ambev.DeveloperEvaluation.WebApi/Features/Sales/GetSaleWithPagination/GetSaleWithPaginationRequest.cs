using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.WebApi.Common.Request;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSaleWithPagination
{
    public class GetSaleWithPaginationRequest : BasePaginationRequest
    {
        public DateTime? InitialDate { get; set; }
        public DateTime? FinalDate { get; set; }    
        public Guid? UserId { get; set; }
        public decimal? Total { get; set; }
        public SaleStatus? Status { get; set; }
        public int? AfiliateId { get; set; }
        public bool OnlyNotCanceledItem { get; set; } = true;

    }
}
