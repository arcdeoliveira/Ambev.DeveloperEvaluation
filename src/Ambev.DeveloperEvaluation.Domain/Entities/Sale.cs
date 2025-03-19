using System.Collections.ObjectModel;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale : BaseMongoDBEntity
    {
        public Sale() { }

        public Sale(SaleStatus status, decimal total, Guid userId, short afiliateId)
        {
            Status = status;
            Total = total;
            UserId = userId;
            AfiliateId = afiliateId;
        }

        public SaleStatus Status { get; private set; } = SaleStatus.Ordered;
        public decimal Total { get; private set; } = 0;
        public Guid UserId { get; private set; } = Guid.Empty;
        public int AfiliateId { get; private set; } = 0;


        public virtual Collection<ProductSale> Itens { get; private set; } = [];


        public void AlterStatus(SaleStatus status)
        {
            Status = status;
        }   

        public void AlterTotal(decimal total)
        {
            Total = total;
        }

        public void AlterCostumer(Guid userId, User? costumer)
        {
            UserId = userId;          
        }

        public void AlterAfiliate(short afiliateId, Afiliate? afiliate)
        {
            AfiliateId = afiliateId;
        }

        public void AlterItens(Collection<ProductSale> itens)
        {
            Itens = itens;
        }   
    }
}
