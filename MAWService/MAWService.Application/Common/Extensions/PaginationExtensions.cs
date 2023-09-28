using Microsoft.EntityFrameworkCore;

namespace MAWService.Application.Common.Extensions;

public static class PaginationExtensions
{
    public static async Task<PageModel<T>> PaginateAsync<T>(
        this IQueryable<T> query,
        int page,
        int pageSize,
        CancellationToken cancellationToken)
    {
        var pageModel = new PageModel<T>();

        pageModel.CurrentPage = page <= 0 ? 1 : page;
        pageModel.PageSize = pageSize;
        pageModel.TotalItems = await query.CountAsync(cancellationToken);

        var startRow = (page - 1) * pageSize;
        pageModel.Data = await query
            .Skip(startRow)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        pageModel.TotalPages = (int)Math.Ceiling(pageModel.TotalItems / (double)pageSize);

        return pageModel;
    }

    public class PageModel<T>
    {
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        private int _pageSize { get; set; }

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > 0 ? value : 1;
        }

        public int CurrentPage { get; set; }

        public IList<T> Data { get; set; } = new List<T>();
    }
}