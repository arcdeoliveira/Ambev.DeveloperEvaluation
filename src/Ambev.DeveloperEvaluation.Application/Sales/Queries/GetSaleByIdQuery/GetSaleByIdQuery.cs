using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Queries.GetSaleByIdQuery
{
    public class GetSaleByIdQuery : IRequest<GetSaleByIdQueryResult>
    {
        public GetSaleByIdQuery(string id) 
        { 
            Id = id; 
        }

        public string Id { get; set; }  
    }
}
