using Application.Dtos.FilterMode;

namespace Application.Dtos;

public record GetAllBooksCriteriaDto(IFilterMode FilterMode, int Page, int Limit);