using System.ComponentModel.DataAnnotations;

namespace Lumos.Data.Models
{
    public abstract class LumosBaseModel
    {
        [Key]
        public long Id { get; protected set; }

        [Required]
        public DateTime CreatedAt { get; protected set; }

        [Required]
        public DateTime UpdatedAt { get; protected set; }

        public bool IsDeleted { get; set; }

        protected LumosBaseModel()
        {
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            IsDeleted = false;
        }

        // Método virtual para validar o modelo
        public virtual bool Validate(out List<ValidationResult> validationResults)
        {
            var validationContext = new ValidationContext(this);
            validationResults = new List<ValidationResult>();

            // Verifica se os atributos de validação do modelo estão corretos
            if (!Validator.TryValidateObject(this, validationContext, validationResults, true))
                return false;

            // Verifica se há propriedades marcadas com Required que estão vazias
            var requiredProperties = GetType().GetProperties()
                .Where(prop => prop.GetCustomAttributes(typeof(RequiredAttribute), true).Any())
                .ToList();

            foreach (var property in requiredProperties)
            {
                var value = property.GetValue(this);

                if (value == null || (value is string stringValue && string.IsNullOrWhiteSpace(stringValue)))
                {
                    validationResults.Add(new ValidationResult($"{property.Name} é obrigatório."));
                    return false;
                }
            }

            return true;
        }

        // Método para atualizar a data de atualização
        public void UpdateUpdatedAt()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
