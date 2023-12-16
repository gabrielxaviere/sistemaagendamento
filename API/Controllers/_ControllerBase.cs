using App._Core;
using App.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControllerBase : Controller
    {


        public ControllerBase()
        {
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controllerActionDescriptor = context.ActionDescriptor as Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor;
            AuthorizeUserAttribute atributo = null;

            if (controllerActionDescriptor != null)
            {
                var actionAttributes = controllerActionDescriptor.MethodInfo.GetCustomAttributes(inherit: true);

                foreach (var item in actionAttributes)
                {
                    if (item is AuthorizeUserAttribute)
                    {
                        atributo = item as AuthorizeUserAttribute;
                    }
                }
            }

            var jwtInstance = new JWT(); // Crie uma instância de JWT
            var retorno = jwtInstance.GetUserByToken(Request.Headers["token"].ToString());

            string accessLevel = "";

            if (atributo != null)
            {
                accessLevel = atributo.AccessLevel;
            }

            //var status = Utils.Utils.ValidateAccessToken(accessLevel, Request.Headers["token"].ToString(), ref this.idUser, ref this.idInstancia);

            //if (status > 0)
            //{
            //    context.Result = StatusCode(status, "Acesso Negado");
            //}

            base.OnActionExecuting(context);
        }
    }
}
