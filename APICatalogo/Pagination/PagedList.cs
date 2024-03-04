namespace APICatalogo.Pagination
{
    public class PagedList<T> : List<T> where T : class 
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public bool HasPrevious { get; set; }
        public bool HasNext { get; set; }

        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)PageSize);

            AddRange(items);
        }

        //A fonte de dados é do tipo IQueryable<T> em muitos casos de paginação,
        //porque IQueryable oferece várias vantagens ao realizar consultas a bancos de dados.
        //A principal razão para isso é a capacidade do IQueryable de criar consultas SQL
        //eficientes que são executadas no banco de dados
        public static PagedList<T> ToPagedList(IQueryable<T> source, int pageNumber, int PageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * PageSize).Take(PageSize).ToList();

            return new PagedList<T>(items, count, pageNumber, PageSize);
        }
    }
}
