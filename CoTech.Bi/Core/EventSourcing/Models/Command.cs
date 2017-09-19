namespace CoTech.Bi.Core.EventSourcing.Models
{
    public abstract class Command {
      public long UserId { get; set; }
    }
}