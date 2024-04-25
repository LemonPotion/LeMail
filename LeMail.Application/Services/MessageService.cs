using AutoMapper;
using LeMail.Application.Dto_s.Message.Requests;
using LeMail.Application.Dto_s.Message.Responses;
using LeMail.Application.Interfaces.Repository;
using LeMail.Application.Interfaces.Services;
using LeMail.Domain.Entities;

namespace LeMail.Application.Services;

public class MessageService : IMessageService
{
    private readonly IMessageRepository _messageRepository;
    private readonly IMapper _mapper;

    public MessageService(IMessageRepository messageRepository, IMapper mapper)
    {
        _messageRepository = messageRepository;
        _mapper = mapper;
    }
    
    public async Task<CreateMessageResponse> CreateMessageAsync(CreateMessageRequest request, CancellationToken cancellationToken)
    {
        var message = _mapper.Map<Message>(request);
        var createdMessage = await _messageRepository.CreateAsync(message,cancellationToken);
        var response = _mapper.Map<CreateMessageResponse>(createdMessage);
        
        return response;
    }

    public async Task<GetMessageResponse> GetMessageByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var message = await _messageRepository.GetByIdAsync(id,cancellationToken);
        var response = _mapper.Map<GetMessageResponse>(message);
        
        return response;
    }

    public async Task<UpdateMessageResponse> UpdateMessageAsync(UpdateMessageRequest request, CancellationToken cancellationToken)
    {
        var message = await _messageRepository.GetByIdAsync(request.Id, cancellationToken);
        var updatedMessage = await _messageRepository.UpdateAsync(message, cancellationToken);
        var response = _mapper.Map<UpdateMessageResponse>(updatedMessage);
        
        return response;
    }

    public async Task<bool> DeleteMessageByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _messageRepository.DeleteByIdAsync(id, cancellationToken);
    }

    public async Task<List<GetMessageResponse>> GetAllMessagesAsync(CancellationToken cancellationToken)
    {
        var messageEntities = await _messageRepository.GetAllListAsync(cancellationToken);
        var response = _mapper.Map<List<GetMessageResponse>>(messageEntities);
        
        return response;
    }
}