using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AMP.Shared.Domain.Models
{
    public class PaginatedList<T>
    {
        public List<T> Data { get; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
        public int PageSize { get; }
        public int TotalCount { get; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
        public int From => ((CurrentPage - 1) * PageSize) + 1;
        public int To => (From + PageSize) - 1;
        public int CurrentPage { get; }

        public PaginatedList(List<T> data,
            int totalCount,
            int currentPage,
            int pageSize)
        {
            Data = data;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalCount = totalCount;
        }
    }

    //public class PaginatedList<T>
    //{
    //    public List<T> Data { get; }
    //    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    //    public int PageSize { get; }
    //    public int TotalCount { get; }
    //    public int RemainingCount { get; }
    //    public int CurrentPage => (int)Math.Ceiling((TotalCount - RemainingCount) / (double)PageSize);
    //    public bool HasPrevious => CurrentPage > 1;
    //    public bool HasNext => CurrentPage < TotalPages;
    //    public int From => ((CurrentPage - 1) * PageSize) + 1 > 0 ? ((CurrentPage - 1) * PageSize) + 1 : 0;
    //    public int To => (From + PageSize) - 1 > 0 ? (From + PageSize) - 1 : 0;

    //    public PaginatedList(List<T> data,
    //        int totalCount,
    //        int remainingCount,
    //        int pageSize)
    //    {
    //        Data = data;
    //        RemainingCount = remainingCount;
    //        PageSize = pageSize;
    //        TotalCount = totalCount;
    //    }
}
