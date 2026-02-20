namespace ApplyVault.Domain.Shared.ComplexTypes { 
    public record Pagination
    {
        public int PageNumber { get; init; }
        public int PageSize { get; init; }
        public static Pagination Default => new Pagination();
        public Pagination()
        {
            PageNumber = 1;
            PageSize = 10;
        }
        public Pagination(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
