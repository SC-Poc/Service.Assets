﻿using Assets.WebApi.Models.Assets;
using FluentValidation;
using JetBrains.Annotations;

namespace Assets.WebApi.Validators
{
    [UsedImplicitly]
    public class AssetValidator : AbstractValidator<Asset>
    {
        public AssetValidator()
        {
            RuleFor(o => o.Symbol)
                .NotEmpty()
                .WithMessage("Symbol required.")
                .MaximumLength(16)
                .WithMessage("Symbol shouldn't be longer than 16 characters.");

            RuleFor(o => o.Description)
                .MaximumLength(500)
                .WithMessage("Description shouldn't be longer than 500 characters.");

            RuleFor(o => o.Accuracy)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Accuracy should be greater than or equal to 0.");
        }
    }
}
