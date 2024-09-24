using System.Linq.Expressions;
using System.Text;
using Application.Dtos;
using Domain.Entities;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Configs;

public static class MapsterConfig
{
    public static void RegisterMapsterConfig(this IServiceCollection serviceCollection)
    {
        Expression<Func<BookDto, byte[]>> convertFromBase64Expression = source =>
            Convert.FromBase64String(
                new StringBuilder(source.Image)
                    .Replace("data:image/", "")
                    .Replace("jpeg;base64,", "")
                    .Replace("png;base64,", "")
                    .ToString()
            );

        Expression<Func<Book, string>> convertToBase64Expression = source =>
            new StringBuilder("data:image/jpeg;base64,")
                .Append(Convert.ToBase64String(source.Image))
                .ToString();

        TypeAdapterConfig.GlobalSettings.NewConfig<BookDto, Book>()
            .Map(destination => destination.Image, convertFromBase64Expression);

        TypeAdapterConfig.GlobalSettings.NewConfig<Book, BookDto>()
            .Map(destination => destination.Image, convertToBase64Expression);
    }
}