using LeMail.Domain.Entities;

namespace LeMail.Application.Interfaces.Repository;

public interface IContactRepository : IBaseRepository<Contact>
{
    Task<List<Contact>> GetAllListByUserIdAsync(Guid id,CancellationToken cancellationToken);
}