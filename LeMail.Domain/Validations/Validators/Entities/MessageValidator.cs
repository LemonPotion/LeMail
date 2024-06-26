using FluentValidation;
using LeMail.Domain.Entities;
using LeMail.Domain.Validations.Validators.Common;

namespace LeMail.Domain.Validations.Validators.Entities;
/// <summary>
/// Message entity validator
/// </summary>
public class MessageValidator : AbstractValidator<Message>
{
    public MessageValidator(string paramName)
    {
        RuleFor(param => param.User).SetValidator(new UserObjValidator(nameof(Message.User)));
        RuleFor(param => param.Body).SetValidator(new BodyValidator(nameof(Message.Body)));
        RuleFor(param => param.Subject).SetValidator(new SubjectValidator(nameof(Message.Subject)));
        RuleFor(param => param.To).SetValidator(new EmailValidator(nameof(Message.To)));
    }
}