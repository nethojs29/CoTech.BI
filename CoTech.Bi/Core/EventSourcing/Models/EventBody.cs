namespace CoTech.Bi.Core.EventSourcing.Models
{
    public class EventBody {
      public virtual EventEntity ToEventEntity(Command cmd) {
        return new EventEntity {
          UserId = cmd.UserId,
          Body = this
        };
      }
    }
}