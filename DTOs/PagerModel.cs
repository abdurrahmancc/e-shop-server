namespace e_shop_server.DTOs
{
    public class PagerModel
    {
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPage { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }

    }
}
