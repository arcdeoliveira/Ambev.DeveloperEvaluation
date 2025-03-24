using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Dtos.Sales
{
    public class SalePaginationFilter
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
