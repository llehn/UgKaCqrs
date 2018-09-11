using Akka.Cluster.Sharding;

namespace UgKaCqrs
{
    public class MessageExtractor : HashCodeMessageExtractor
    {
        public MessageExtractor() : base(10)
        {
        }

        public override string EntityId(object message) 
            => (message as IHasEntityId)?.EntityId;
    }
}