using FluentValidation;

namespace Persistence.EntityFramework.Configuration
{
    internal sealed class PersistenceSettingsValidator : AbstractValidator<PersistenceSettings>
    {
        public PersistenceSettingsValidator()
        {
            When(settings => settings.UseInMemoryContext, () =>
                RuleFor(s => !string.IsNullOrWhiteSpace(s.DbConnection)));
        }
    }
}