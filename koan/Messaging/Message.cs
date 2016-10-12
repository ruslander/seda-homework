namespace Koan.Messaging
{
    public abstract class Message
    {
    }

    public class PublishEnvelope : IEnvelope
    {
        private readonly IPublisher _publisher;

        public PublishEnvelope(IPublisher publisher)
        {
            _publisher = publisher;
        }

        public void ReplyWith<T>(T message) where T : Message
        {
            _publisher.Publish(message);
        }
    }
    
    public interface IEnvelope
    {
        void ReplyWith<T>(T message) where T : Message;
    }
}