using UgKaCqrs.Events;

namespace UgKaCqrs
{
    public class BookAddedEvent : IEvent
    {
        public string ISBN { get; }
        public string Title { get; }
        public string Author { get; }

        public BookAddedEvent(string isbn, string title, string author)
        {
            ISBN = isbn;
            Title = title;
            Author = author;
        }
    }
}