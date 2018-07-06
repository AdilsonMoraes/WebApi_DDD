using Cadastro.Dominio.CadastroUsuario;
using Cadastro.Dominio.CadastroUsuario.Servico;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Cadastro.Web.Controllers
{
    public class UsuarioController : ApiController
    {
        private readonly IUsuarioServico _servico;

        public UsuarioController(IUsuarioServico servico)
        {
            _servico = servico;
        }

        // GET: api/Usuario
        [AllowAnonymous]
        [Route("api/Usuario/ListarUsuario")]
        [HttpGet]
        public IEnumerable<Usuario> ListarUsuario()
        {
            return _servico.ListarUsuario();
        }

        // GET: api/Usuario/5
        [AllowAnonymous]
        [Route("api/Usuario/ListarUsuarioPeloId/{id}")]
        [HttpGet]
        public Usuario ListarUsuarioPeloId(int id)
        {
            Usuario item = _servico.ListarUsuarioPeloId(id);

            if (item == null)
            {
                var message = string.Format("Usuário não encontrado id = {0}", id);
                throw new HttpResponseException
                    (Request.CreateErrorResponse(HttpStatusCode.NotFound, message));
            }
            else
            {
                return item;
            }
        }

        // POST: api/Usuario
        [AllowAnonymous]
        [Route("api/Usuario/InserirUsuario")]
        [HttpPost]
        public IHttpActionResult InserirUsuario([FromBody]Usuario usuario)
        {
            try
            {
                _servico.InserirUsuario(usuario);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT: api/Usuario/5
        [AllowAnonymous]
        [Route("api/Usuario/AlterarUsuario")]
        [HttpPost]
        public IHttpActionResult AlterarUsuario([FromBody]Usuario usuario)
        {
            try
            {
                _servico.AlterarUsuario(usuario);
                return Ok(new Usuario { usuarioid = usuario.usuarioid });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }


        }

        // DELETE: api/Usuario/5
        [AllowAnonymous]
        [Route("api/Usuario/DeletarUsuario/{id}")]
        [HttpPost]
        public IHttpActionResult DeletarUsuario(int id)
        {
            try
            {
                _servico.DeletarUsuario(id);
                return Ok(new Usuario { usuarioid = id });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    }
}
