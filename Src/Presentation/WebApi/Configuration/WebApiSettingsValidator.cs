using FluentValidation;

namespace WebApi.Configuration
{
    internal sealed class WebApiSettingsValidator : AbstractValidator<WebApiSettings>
    {
        public WebApiSettingsValidator()
        {
            RuleFor(settings => settings.AllowedOrigins).NotNull();
        }
    }
}