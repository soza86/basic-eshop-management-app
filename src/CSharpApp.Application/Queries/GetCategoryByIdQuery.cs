using MediatR;

namespace CSharpApp.Application.Queries
{
    public record GetCategoryByIdQuery(int Id) : IRequest<Category?>;
}
