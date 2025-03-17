using BBQ.DataAccess.Entities;
using BBQ.DataAccess.Persistence;

namespace BBQ.DataAccess.Repositories;

public class BbqSessionsRepository : BaseRepository<BbqSession>, IBbqSessionsRepository
{
    public BbqSessionsRepository(DatabaseContext context) : base(context) { }
}
