using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.Queries.GetProductByIdQuery
{
    public class GetProductByIdQuery : IRequest<GetProductByIdQueryResult>
    {
        public GetProductByIdQuery(string id) 
        { 
            Id = id; 
        }

        public string Id { get; set; }  
    }
}
