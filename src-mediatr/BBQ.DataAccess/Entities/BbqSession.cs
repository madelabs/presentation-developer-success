using BBQ.DataAccess.Common;

namespace BBQ.DataAccess.Entities
{
    public class BbqSession : BaseEntity, IAuditedEntity
    {
        public string Description { get; set; }
        
        public string Result { get; set; }
        public Guid TenantId { get; set; }

        public Guid UserId { get; set; }
        public List<SessionNote> Notes { get; } = new List<SessionNote>();

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
