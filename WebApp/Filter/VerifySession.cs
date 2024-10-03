using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Web;
using Domain.Models;
using WebApp.Controllers;
namespace WebApp.Filter
{
    public class VerifySession : IAsyncActionFilter
    {
        public  async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var user = context.HttpContext.User;
            if(!user.Identity!.IsAuthenticated){
                if(context.Controller is AuthenticationController == false){
                    context.HttpContext.Response.Redirect("/Authentication/Login/");
                }
            }else{
                if(context.Controller is AuthenticationController == true){
                    context.HttpContext.Response.Redirect("/Home/Index");
                }
            }
            await next();
        }
    }
}