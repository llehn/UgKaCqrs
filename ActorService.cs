using Akka.Actor;

namespace UgKaCqrs
{
    public class ActorService
    {
        public IActorRef ActorRef { get; }

        public ActorService(IActorRef actorRef) => ActorRef = actorRef;
    }
}