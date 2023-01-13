using FluentValidation;
using Google.Protobuf;
using Rosi.BMS.API.Business.Constants;
using Rosi.BMS.API.Entities.Dtos;
using Rosi.BMS.API.Entities.Dtos.Zone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rosi.BMS.API.Business.ValidationRules
{
    public class CreateZoneDtoValidator : AbstractValidator<CreateZoneDto>
    {
        public CreateZoneDtoValidator()
        {
            RuleFor(m => m.Name).NotEmpty().WithMessage(Messages.CannotBeEmpty);            
        }
    }
}
