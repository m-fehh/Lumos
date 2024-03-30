using System.ComponentModel.DataAnnotations;

namespace Lumos.Application.Enums
{
    public enum EAccessLevel
    {
        [Display(Name = "Admin")]
        Admin = 0,          // Administrador com acesso completo

        [Display(Name = "Gerente")]
        Manager = 1,        // Gerente com permissões intermediárias

        [Display(Name = "Analista")]
        Analyst = 2       // Analista com permissões específicas
    }

    public static class EAccessLevelExtensions
    {
        public static string GetDisplayName(this EAccessLevel accessLevel)
        {
            var displayAttribute = accessLevel.GetType()
                .GetMember(accessLevel.ToString())
                .FirstOrDefault()?
                .GetCustomAttributes(typeof(DisplayAttribute), false)
                .OfType<DisplayAttribute>()
                .FirstOrDefault();

            return displayAttribute?.GetName() ?? accessLevel.ToString();
        }
    }
}
