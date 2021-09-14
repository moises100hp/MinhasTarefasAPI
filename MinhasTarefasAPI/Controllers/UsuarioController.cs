using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MinhasTarefasAPI.Models;
using MinhasTarefasAPI.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhasTarefasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly SignInManager<AplicationUser> _signInManager;
        private readonly UserManager<AplicationUser> _userManager;

        public UsuarioController(IUsuarioRepository usuarioRepository, SignInManager<AplicationUser> signInManager, UserManager<AplicationUser> userManager)
        {
            _usuarioRepository = usuarioRepository;
            _signInManager = signInManager;
            _userManager = userManager;
        }


        public ActionResult Login([FromBody] UsuarioDTO usuarioDTO)
        {
            ModelState.Remove("Nome");
            ModelState.Remove("ConfirmacaoSenha");

            if (ModelState.IsValid)
            {
                AplicationUser usuario = _usuarioRepository.Obter(usuarioDTO.Email, usuarioDTO.Senha);

                if (usuario != null)
                {
                    //Login no Identity
                    //Será adaptado para JWT

                    _signInManager.SignInAsync(usuario, false);

                    //retorna Token (JWT)
                    return Ok();
                }
                else
                    return NotFound("Usuário não localizado");
            }
            else
                return UnprocessableEntity(ModelState);
        }

        public ActionResult Cadastrar([FromBody] UsuarioDTO usuarioDTO)
        {
            if (ModelState.IsValid)
            {
                AplicationUser usuario = new AplicationUser();

                usuario.FullName = usuarioDTO.Nome;
                usuario.Email = usuarioDTO.Email;

                var resultado = _userManager.CreateAsync(usuario, usuarioDTO.Senha).Result;

                if (!resultado.Succeeded)
                {
                    List<string> erros = new List<string>();

                    foreach (var erro in resultado.Errors)
                    {
                        erros.Add(erro.Description);
                    }
                    return UnprocessableEntity(erros);
                }
                else
                    return Ok(usuario);
            }
            else
                return UnprocessableEntity(ModelState);
        }
    }
}
