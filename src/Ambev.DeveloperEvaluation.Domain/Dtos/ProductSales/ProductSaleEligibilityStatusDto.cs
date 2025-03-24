namespace Ambev.DeveloperEvaluation.Domain.Dtos.ProductSales
{
    public class ProductSaleEligibilityStatusDto
    {
        public ProductSaleEligibilityStatusDto(List<ProductSaleDto> eligibleProducts, List<string> ineligibleProductIds, List<string> notFoundProductIds)
        {
            EligibleProducts = eligibleProducts;
            IneligibleProductIds = ineligibleProductIds;
            NotFoundProductIds = notFoundProductIds;
        }

        public List<ProductSaleDto> EligibleProducts { get; set; } = [];
        public List<string> IneligibleProductIds { get; set; } = [];
        public List<string> NotFoundProductIds { get; set; } = [];
    }
}
