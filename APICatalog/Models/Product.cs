using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace APICatalog.Models
{
    public class Product : IValidatableObject
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Categoria é obrigatória")]
        [Range(1, int.MaxValue, ErrorMessage = "Categoria inválida")]
        public int CategoryId { get; set; } = 0;

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

        [Range(0.01, 9_999_999, ErrorMessage = "Preço deve ser maior que zero")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; } = 0;

        [Url(ErrorMessage = "Informe uma URL válida")]
        [StringLength(255, ErrorMessage = "URL deve ter até {1} caracteres")]
        public string? ImageUrl { get; set; } = null;

        [Range(0, float.MaxValue, ErrorMessage = "Estoque deve ser maior ou igual a zero")]
        public float Stock { get; set; } = 0;

        [DataType(DataType.DateTime)]
        public DateTime Created { get; set; } = DateTime.UtcNow;

        [ValidateNever]
        public Category? Category { get; set; } = null;

        // Validações de regra de negócio cruzadas
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Name))
                yield return new ValidationResult("Nome não pode ser vazio", [nameof(Name)]);

            if (Created > DateTime.UtcNow.AddMinutes(1))
                yield return new ValidationResult("Data de criação não pode ser futura", [nameof(Created)]);

            if (!string.IsNullOrWhiteSpace(ImageUrl)
                && Uri.TryCreate(ImageUrl, UriKind.Absolute, out var uri)
                && !(uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps))
            {
                yield return new ValidationResult("A URL da imagem deve usar http ou https", [nameof(ImageUrl)]);
            }
        }
    }
}
