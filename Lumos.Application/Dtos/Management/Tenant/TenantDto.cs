﻿using Lumos.Data.Enums;
using Lumos.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace Lumos.Application.Dtos.Management.Tenant
{
    public class TenantDto : LumosBaseModel
    {
        [Required(ErrorMessage = "O nome do tenant é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do tenant deve ter no máximo 100 caracteres.")]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "O tipo é obrigatório.")]
        public ETenantType Type { get; set; }

        [Required(ErrorMessage = "O ramo é obrigatório.")]
        public string Branch { get; set; }

        [Required(ErrorMessage = "A cidade é obrigatória.")]
        [StringLength(50, ErrorMessage = "A cidade deve ter no máximo 50 caracteres.")]
        public string City { get; set; }

        [Required(ErrorMessage = "O UF é obrigatório.")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "O UF deve ter 2 caracteres.")]
        public string Uf { get; set; }

        public List<OrganizationDto> Organizations { get; set; }

    }
}