using System.Collections.Generic;
using Akka.Persistence.Journal;

namespace UgKaCqrs
{
    public class TaggingAdapter : IWriteEventAdapter
    {
        public string Manifest(object evt) => string.Empty;

        public object ToJournal(object evt) 
            => new Tagged(evt, new List<string>() {"book"});
    }
}