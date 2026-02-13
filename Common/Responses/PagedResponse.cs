namespace PraPdBL_Backend.Common.Responses;

public class PagedResponse<T>
{
    public int Total { get; }
    public int Page { get; }
    public int PageSize { get; }
    public IReadOnlyList<T> Data { get; }

    public PagedResponse(int total, int page, int pageSize, IReadOnlyList<T> data)
    {
        Total = total;
        Page = page;
        PageSize = pageSize;
        Data = data;
    }
}
