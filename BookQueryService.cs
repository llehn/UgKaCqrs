using System;
using System.Collections.Generic;
using Akka;
using Akka.Actor;
using Akka.Persistence.Query;
using Akka.Persistence.Query.Sql;
using Akka.Streams;
using Akka.Streams.Dsl;

namespace UgKaCqrs
{
    public class BookQueryService
    {
        private readonly SqlReadJournal readJournal;
        private readonly ActorSystem actorSystem;
        private Dictionary<string, BookQueryModel> books = new Dictionary<string, BookQueryModel>();

        public BookQueryService(SqlReadJournal readJournal, ActorSystem actorSystem)
        {
            this.readJournal = readJournal;
            this.actorSystem = actorSystem;
            var source = readJournal.EventsByTag("book");
            var mat = ActorMaterializer.Create(actorSystem);
            source.RunForeach(envelope => Handle(envelope.Event), mat);
        }

        public IEnumerable<BookQueryModel> Books => books.Values;

        private void Handle(object evt)
        {
            switch (evt)
            {
                case BookAddedEvent bookAdded:
                    books[bookAdded.ISBN] = new BookQueryModel
                    {
                        ISBN = bookAdded.ISBN,
                        Title = bookAdded.Title,
                        Author = bookAdded.Author
                    };
                    break;
            }
        }
    }
}