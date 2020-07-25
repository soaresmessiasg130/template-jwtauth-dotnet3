using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Models;
using Shop.Repositories;
using Shop.Services;

namespace Shop.Controllers
{
  [Route("v1/account")]
  public class HomeController : ControllerBase
  {
    [HttpPost]
    [Route("login")]
    [AllowAnonymous]
    public async Task<ActionResult<dynamic>> Authenticate([FromBody] User model) {
      var user = UserRepository.GetUser(model.Username, model.Password);

      if (user == null)
        return NotFound(new{ Message = "Usuário ou senha inválidos!"});

      var token = TokenService.GenerateToken(user);
      
      user.Password = "";

      return new {
        user = user,
        token = token
      };
    }

    [AllowAnonymous]
    public string Anonymous () => "Anonymous Route - No Authentication!!!";

    [HttpGet]
    [Authorize]
    [Route("authenticated")]
    public string Authenticated () => 
      string.Format("Autenticado: {0};", User.Identity.Name);

    [HttpGet("employee")]
    [Authorize(Roles = "employee,manager,dev")]
    public string Employee () =>
      string.Format("Funcionário Autenticado: {0};", User.Identity.Name);

    [HttpGet("manager")]
    [Authorize(Roles = "manager,dev")]
    public string Manager () =>
      string.Format("Gerente Autenticado: {0};", User.Identity.Name);

    [HttpGet("dev")]
    [Authorize(Roles = "dev")]
    public string Dev () =>
      string.Format("Suport Autenticado: {0};", User.Identity.Name);

  }
}