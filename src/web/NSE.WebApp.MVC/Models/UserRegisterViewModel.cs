using NSE.WebApp.MVC.Extensions;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NSE.WebApp.MVC.Models
{
    public class UserRegisterViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Nome Completo")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("CPF")]
        [Cpf]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(16, ErrorMessage = "A senha {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        [DisplayName("Senha")]
        public string Password { get; set; }

        [DisplayName("Confirme sua senha")]
        [Compare("Password", ErrorMessage = "As senhas não coincidem")]
        public string ConfirmPassword { get; set; }
    }
}
