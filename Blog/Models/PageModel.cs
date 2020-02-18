using System;

namespace Blog.Models
{
    public class PageModel
    {

        public PageModel(int totalItems, int currentPage, int pageSize, string pageAction, string pageController)
        {
            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            PageAction = pageAction;
            PageController = pageController;
        }

        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public string PageAction { get; set; }
        public string PageController { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);

        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;

    }
}