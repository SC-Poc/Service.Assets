﻿using Assets.WebApi.Models.Assets;
using FluentValidation;
using JetBrains.Annotations;

namespace Assets.WebApi.Validators
{
    [UsedImplicitly]
    public class AssetEditValidator : AbstractValidator<AssetEdit>
    {
        public AssetEditValidator()
        {
            RuleFor(o => o.Symbol)
                .NotEmpty()
                .WithMessage("Symbol required.");

            RuleFor(o => o.Description)
                .MaximumLength(500)
                .WithMessage("Description must be less than 500 characters.");

            RuleFor(o => o.Accuracy)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Accuracy must be greater than or equal to 0.");
        }
    }
}
