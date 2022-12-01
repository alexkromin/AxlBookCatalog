namespace AxlBookCatalog.Domain
{
    public class Book
    {
        public string Id { get; set; }

        /// <summary>
        /// Автор
        /// </summary>
        public string AuthorId { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Год
        /// </summary>
        public DateTime Year { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// URL обложки
        /// </summary>
        public string Cover { get; set; }

        /// <summary>
        /// Количество страниц
        /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        /// То, что в задании называется "Динамические категории"
        /// </summary>
        //public ICollection<BookProperty> AdditionalProperties { get; set; } = null;
    }
}