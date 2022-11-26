namespace AxlBookCatalog.Business.Models.Book
{
    public class EditBookRequest
    {
        public string Id { get; set; }

        public string AuthorId { get; set; }

        public string Title { get; set; }

        public DateTime Year { get; set; }

        public string Description { get; set; }

        public string Cover { get; set; }

        public int PageCount { get; set; }

        //public ICollection<BookProperty> Books { get; set; }

        public class BookProperty
        {
            public string Name { get; set; }

            public string Value { get; set; }
        }
    }
}
