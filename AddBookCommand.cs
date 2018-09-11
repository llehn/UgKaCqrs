namespace UgKaCqrs
{
    public class AddBookCommand : IHasEntityId
    {
        public AddBookCommand(string isbn, string title, string author)
        {
            ISBN = isbn;
            Title = title;
            Author = author;
        }

        public string ISBN { get; }
        public string Title { get; }
        public string Author { get; }
        public string EntityId => ISBN;
    }
}