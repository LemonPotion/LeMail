using LeMail.Application.Interfaces.Repository;
using LeMail.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LeMail.Persistence.Repositories;

public class MessageRepository : IMessageRepository
{
    private readonly DatabaseContext _dbContext;

    public MessageRepository(DatabaseContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }
    
    public async Task<Message> CreateAsync(Message entity, CancellationToken cancellationToken)
    {
        _dbContext.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<Message> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var message = await _dbContext.FindAsync<Message>(id , cancellationToken);
        if (message is not null)
            return message;
        throw new ArgumentNullException(nameof(message));
    }

    //TODO: сделать рабочим
    public async Task<Message> UpdateAsync(Message entity, CancellationToken cancellationToken)
    {
        _dbContext.Messages.Update(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<bool> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var message = await GetByIdAsync(id, cancellationToken);
        _dbContext.Remove(message);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<List<Message>> GetAllListAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Set<Message>().ToListAsync(cancellationToken);
    }
}