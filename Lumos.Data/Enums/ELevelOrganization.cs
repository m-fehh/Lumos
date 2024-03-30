﻿using System.ComponentModel.DataAnnotations;

namespace Lumos.Data.Enums
{
    public enum ELevelOrganization
    {
        [Display(Name = "Matriz")]
        Matriz = 0,          

        [Display(Name = "Filial")]
        Filial = 1,

        [Display(Name = "PF")]
        Pf = 2,
    }

    public static class ELevelOrganizationExtensions
    {
        public static string GetDisplayNameLevel(this ELevelOrganization level)
        {
            var displayAttribute = level.GetType()
                .GetMember(level.ToString())
                .FirstOrDefault()?
                .GetCustomAttributes(typeof(DisplayAttribute), false)
                .OfType<DisplayAttribute>()
                .FirstOrDefault();

            return displayAttribute?.GetName() ?? level.ToString();
        }
    }
}