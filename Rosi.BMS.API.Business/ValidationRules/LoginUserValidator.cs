using FluentValidation;
using Google.Protobuf;
using Rosi.BMS.API.Business.Constants;
using Rosi.BMS.API.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rosi.BMS.API.Business.ValidationRules
{
    public class LoginUserValidator : AbstractValidator<UserForLoginDto>
    {
        public LoginUserValidator()
        {
            RuleFor(m => m.Password).NotEmpty().WithMessage(Messages.CannotBeEmpty);
            RuleFor(m => m.Email).EmailAddress().WithMessage(Messages.Invalid);
        }
    }
}
