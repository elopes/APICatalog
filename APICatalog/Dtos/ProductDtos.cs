namespace APICatalog.Dtos;

public record ProductItemDto(
    int ProductId,
    string? Name,
    decimal Price,
    float Stock,
    string? ImageUrl
);

public record ProductDto(
    int ProductId,
    string? Name,
    string? Description,
    decimal Price,
    string? ImageUrl,
    float Stock,
    int CategoryId,
    string? CategoryName
);

public record CreateUpdateProductDto(
    int CategoryId,
    string? Name,
    string? Description,
    decimal Price,
    string? ImageUrl,
    float Stock
);
