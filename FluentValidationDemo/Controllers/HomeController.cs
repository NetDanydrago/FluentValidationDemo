using FluentValidationDemo.Entities;
using FluentValidationDemo.Validations;
using FluentValidation.Results;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace FluentValidationDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {

        [HttpPost("Create")]
        public IActionResult CreateOrder([FromBody]Order order)
        {
            ActionResult Result = Ok();

            OrderValidator Validator = new OrderValidator();

            //Nota ValidationResult se encuentra en el espacio de nombres FluentValidation.Results
            ValidationResult ValidationResult = Validator.Validate(order);

            if (ValidationResult.IsValid)
            {
                //Guardar la Order
            }
            else
            {
                //Es posible combinar todos los errores en un solo string ValidationResult.ToString()
                //De manera predeterminada los mensajes son separados por nuevas lineas, pero si lo deseas
                //puedes cambiar el caracter de separacion con el metodo ToString("~")
                Result = Problem(ValidationResult.ToString("-"));
            }
            return Result;
        }

        [HttpPost("CreateException")]
        public IActionResult CreateOrderExceptions([FromBody] Order order)
        {
            ActionResult Result = Ok();

            OrderValidator Validator = new OrderValidator();

            //Otra alternativa en lugar de devolver un ValidationResult
            //se le puede decirle a FluentValidation que lance una excepción si la validación falla.
            //ValidateAndThrow se encuentra en el espacio de nombre FluentValidation
            try
            {
                Validator.ValidateAndThrow(order);
                //Guardar la Orden
            }
            catch (ValidationException e)
            {
                //ValiationException expone una propieda error con los mensajes de error correspondientes.
                StringBuilder Builder = new StringBuilder();
                foreach (var Failure in e.Errors)
                {
                    Builder.AppendFormat($"Property {Failure.PropertyName}," +
                        $" Failed Validation. Erros was {Failure.ErrorMessage}", Environment.NewLine);
                }
                Result = Problem(Builder.ToString());
            }
            return Result;
        }
    }
}
