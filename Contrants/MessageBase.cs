using System;

namespace Contrants
{
    public abstract class MessageBase<TPayload>
    {
        public Guid CorrelationId { get; set; }
        public TPayload Payload { get; set; }
    }
}