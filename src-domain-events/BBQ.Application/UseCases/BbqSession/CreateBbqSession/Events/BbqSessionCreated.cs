using MediatR;

namespace BBQ.Application.UseCases.BbqSession.CreateBbqSession.Events;

public class BbqSessionCreated : INotification
{
    public DataAccess.Entities.BbqSession Payload { get; private set; }

    public BbqSessionCreated(DataAccess.Entities.BbqSession bbqSession)
    {
        this.Payload = bbqSession;
    }
}
