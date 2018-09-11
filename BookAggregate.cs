using System;
using Akka.Actor;
using Akka.Persistence;
using UgKaCqrs.Events;

namespace UgKaCqrs
{
    public class BookAggregate : ReceivePersistentActor
    {
        private bool isIsbnSet;

        public BookAggregate()
        {
            PersistenceId = Self.Path.Name;

            Command<AddBookCommand>(command =>
            {
                if (isIsbnSet)
                    throw new InvalidOperationException("NO WAY DUDE!!!");



                var bookAdded = new BookAddedEvent(command.ISBN, command.Title, command.Author);
                Persist(bookAdded, @event =>
                {
                    Context.System.EventStream.Publish(@event);
                    Apply(@event);
                });
            });

            Recover<IEvent>(@event =>
            {
                Apply(@event);
                Console.WriteLine("Recovering...");
            });
        }

        private void Apply(IEvent @event)
        {
            switch (@event)
            {
                case BookAddedEvent bookAdded:
                    isIsbnSet = true;
                    break;
            }
        }

        public override string PersistenceId { get; }
    }
}