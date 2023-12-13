using CustomerApp.Application.CustomerFeatures.Queries.GetOrders;
using FluentValidation;

namespace CustomerApp.Application.UserFeatures.Validators;

public class GetOrdersQueryValidator : AbstractValidator<GetOrdersQuery>
{
    public GetOrdersQueryValidator()
    {
        RuleFor(x => x.Page)
            .NotEmpty();

        RuleFor(x => x.PageSize)
            .NotEmpty();
    }
}
