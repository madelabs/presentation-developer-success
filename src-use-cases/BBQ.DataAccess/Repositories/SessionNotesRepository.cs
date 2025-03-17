using BBQ.DataAccess.Entities;
using BBQ.DataAccess.Persistence;

namespace BBQ.DataAccess.Repositories;

public class SessionNotesRepository : BaseRepository<SessionNote>, ISessionNotesRepository
{
    public SessionNotesRepository(DatabaseContext context) : base(context) { }
}
