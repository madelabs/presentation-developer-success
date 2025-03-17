using BBQ.DataAccess.Entities;
using BBQ.DataAccess.Persistence;

namespace BBQ.DataAccess.Repositories;

public class BbqSessionRepository : BaseRepository<BbqSession>, IBbqSessionRepository
{
    public BbqSessionRepository(DatabaseContext context) : base(context) { }
}
