using Application.Dtos.FilterMode;

namespace Application.Dtos;

public record GetAllBooksPageCountCriteriaDto(IFilterMode FilterMode, int Limit);