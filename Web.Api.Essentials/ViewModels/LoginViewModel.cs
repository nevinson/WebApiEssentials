using FluentValidation.Attributes;
using Web.Api.Essentials.ViewModels.Validations;

namespace Web.Api.Essentials.ViewModels
{
    [Validator(typeof(LoginViewModelValidator))]
    public class LoginViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
