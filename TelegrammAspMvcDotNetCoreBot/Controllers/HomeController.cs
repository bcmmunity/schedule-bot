using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TelegrammAppMvcDotNetCore___Buisness_Logic.Models;
using TelegrammAspMvcDotNetCoreBot.Models;

namespace TelegrammAspMvcDotNetCoreBot.Controllers
{
    public class HomeController : Controller
    {
        public string Index()
        {
            return "This is Telegram schedule bot";
        }
    }
}
