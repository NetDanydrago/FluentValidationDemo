using FluentValidation;
using FluentValidationDemo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentValidationDemo.Validations
{
    public class OrderDetailValidator : AbstractValidator<OrderDetail>
    {
        public OrderDetailValidator()
        {
            RuleFor(d => d.OrderID).GreaterThan(0);
            RuleFor(d => d.ProductID).GreaterThan(0);
            RuleFor(d => d.Quantity).GreaterThan(0);
            RuleFor(d => d.UnitPrice).GreaterThan(0);
            RuleFor(d => d.Discount).LessThanOrEqualTo(15);
        }
    }
}
