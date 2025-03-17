using BBQ.DataAccess.Entities;
using BBQ.DataAccess.Persistence;

namespace BBQ.DataAccess.Repositories;

public class SessionNoteRepository : BaseRepository<SessionNote>, ISessionNoteRepository
{
    public SessionNoteRepository(DatabaseContext context) : base(context) { }
}
