namespace Ambev.DeveloperEvaluation.Domain.Dtos.Shared
{
    public class BaseDataPaginationDto<T>
    {
        public BaseDataPaginationDto() { }

        public BaseDataPaginationDto(IEnumerable<T> data, long total)
        {
            Data = data;   
            Total = total;
        }

        public IEnumerable<T> Data { get; set; } = default!;
        public long Total { get; set; } = 0;
    }
}
