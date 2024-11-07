namespace e_shop_server.DTOs
{
    public class ItemsListWithPagination<P>
    {
        public P? ItemsList { get; set; }
        public PagerModel Pager { get; set; }
    }
}
