using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace APICatalog.Models
{
    public class Category : IValidatableObject
    {
        public int CategoryId { get; set; }

        private string? _name;

        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(100, ErrorMessage = "Nome deve ter até {1} caracteres")]
        public string? Name
        {
            get => _name;
            set => _name = value?.Trim();
        }

        [StringLength(255, ErrorMessage = "Descrição deve ter até {1} caracteres")]
        public string? Description { get; set; } = null;

        [Url(ErrorMessage = "Informe uma URL válida")]
        [StringLength(255, ErrorMessage = "URL deve ter até {1} caracteres")]
        public string? ImageUrl { get; set; } = null;

        [ValidateNever]
        public ICollection<Product> Products { get; set; } = [];

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Name))
                yield return new ValidationResult("Nome não pode ser vazio", new[] { nameof(Name) });
        }
    }
}
