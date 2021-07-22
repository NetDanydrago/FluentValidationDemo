using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidationDemo.Entities;

namespace FluentValidationDemo.Validations
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(o => o.CustomerID).GreaterThan(0);
            RuleFor(o => o.CustomerID).GreaterThan(0);
            RuleFor(o => o.ShipAddress).NotNull();
            RuleFor(o => o.ShipCity).NotNull().NotEmpty();
            //Tambien es posible encadenar varias validaciones en una propieda
            RuleFor(o => o.ShipName).NotNull().NotEqual("foo");
            RuleFor(o => o.ShipPostalCode).NotNull();
            RuleFor(o => o.OrderDetails).NotNull();
            //Tambien es posible crear una validacion personalizada con su mensaje correspondiente
            RuleFor(o => o.OrderDate).Must(date => date.CompareTo(DateTime.Now) == -1)
                .WithMessage("the date cannot be greater than the current one");
            //Para aplicar reglas a un objecto que representa una coleccion podemos usar
            //RuleForEach con SetValidator
            RuleForEach(o => o.OrderDetails).SetValidator(new OrderDetailValidator());


        }
    }
}
