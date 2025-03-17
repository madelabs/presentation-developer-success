using BBQ.DataAccess.Common;

namespace BBQ.DataAccess.Entities
{
    public class SessionNote : BaseEntity, IAuditedEntity
    {
        public string ActivityDescription { get; set; }
        
        public string Note { get; set; }

        public decimal PitTemperature { get; set; }

        public virtual BbqSession Session { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
