namespace APICatalog.Dtos;

public record CategoryItemDto(
    int CategoryId,
    string? Name,
    string? ImageUrl
);

public record CategoryDto(
    int CategoryId,
    string? Name,
    string? Description,
    string? ImageUrl,
    List<ProductItemDto> Products
);

public record CreateUpdateCategoryDto(
    string? Name,
    string? Description,
    string? ImageUrl
);
