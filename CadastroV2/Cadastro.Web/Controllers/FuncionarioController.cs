using Cadastro.Dominio.CadastroFuncionario;
using Cadastro.Dominio.CadastroFuncionario.Servico;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Cadastro.Web.Controllers
{
    public class FuncionarioController : ApiController
    {
        private readonly IFuncionarioServico _servico;

        public FuncionarioController(IFuncionarioServico servico) 
        {
            _servico = servico;
        }

        // GET: api/Funcionario
        [AllowAnonymous]
        [Route("api/Funcionario/ListarFuncionario")]
        [HttpGet]
        public IEnumerable<Funcionario> ListarFuncionario()
        {
            return _servico.ListarFuncionario();
        }

        // GET: api/Funcionario/5
        [AllowAnonymous]
        [Route("api/Funcionario/ListarFuncionarioPeloId/{id}")]
        [HttpGet]
        public Funcionario ListarFuncionarioPeloId(int id)
        {
            Funcionario funcionario = _servico.ListarFuncionarioPeloId(id);

            if (funcionario == null)
            {
                var message = string.Format("Funcionário não encontrado id = {0}", id);
                throw new HttpResponseException
                    (Request.CreateErrorResponse(HttpStatusCode.NotFound, message));
            }

            return funcionario;
        }

        // POST: api/Funcionario
        [AllowAnonymous]
        [Route("api/Funcionario/InserirFuncionario")]
        [HttpPost]
        public IHttpActionResult InserirFuncionario([FromBody]Funcionario funcionario)
        {
            try
            {
                _servico.InserirFuncionario(funcionario);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT: api/Funcionario/5
        [AllowAnonymous]
        [Route("api/Funcionario/AlterarFuncionario")]
        [HttpPost]
        public IHttpActionResult AlterarFuncionario([FromBody]Funcionario funcionario)
        {
            try
            {
                _servico.AlterarFuncionario(funcionario);
                return Ok(new Funcionario { funcionarioid = funcionario.funcionarioid });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE: api/Funcionario/5
        [AllowAnonymous]
        [Route("api/Funcionario/DeletarFuncionario/{id}")]
        [HttpPost]
        public IHttpActionResult DeletarFuncionario(int id)
        {
            try
            {
                _servico.DeletarFuncionario(id);
                return Ok(new Funcionario { funcionarioid = id });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
