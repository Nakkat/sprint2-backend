﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Peoples.WebApi.Domains;
using Senai.Peoples.WebApi.Interfaces;
using Senai.Peoples.WebApi.Repositories;

namespace Senai.Peoples.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Authorize]

    public class TiposUsuarioController : ControllerBase
    {
        private ITipoUsuarioRepository _tipoTipoUsuarioRepository { get; set; }

        public TiposUsuarioController()
        {
            _tipoTipoUsuarioRepository = new TipoUsuarioRepository();
        }

        [Authorize(Roles = "1")]
        [HttpGet]
        public IEnumerable<TipoUsuarioDomain> Get()
        {

            return _tipoTipoUsuarioRepository.ListarTipoUsuario();
        }

        [Authorize(Roles = "1")]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            TipoUsuarioDomain tipoUsuarioBuscado = _tipoTipoUsuarioRepository.BuscarPorIdTipoUsuario(id);

            if (tipoUsuarioBuscado == null)
            {

                return NotFound("Nenhum tipo de usuário encontrado");
            }

            return Ok(tipoUsuarioBuscado);
        }

        [Authorize(Roles = "1")]
        [HttpPost]
        public IActionResult Post(TipoUsuarioDomain novoUsuario)
        {
            // Faz a chamada para o método .Cadastrar();
            _tipoTipoUsuarioRepository.CadastrarTipoUsuario(novoUsuario);

            // Retorna um status code 201 - Created
            return StatusCode(201);
        }

        [Authorize(Roles = "1")]
        [HttpPut("{id}")]
        public IActionResult AlterarUSuario(int id, TipoUsuarioDomain usuarioAtualizado)
        {
            TipoUsuarioDomain tipoUsuarioBuscado = _tipoTipoUsuarioRepository.BuscarPorIdTipoUsuario(id);

            if (tipoUsuarioBuscado == null)
            {
                return NotFound
                    (
                        new
                        {
                            mensagem = "Tipo de usuário não encontrado",
                            erro = true
                        }
                    );
            }

            try
            {
                _tipoTipoUsuarioRepository.AlterarTipoUsuario(id, usuarioAtualizado);

                return NoContent();
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }

        [Authorize(Roles = "1")]
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            _tipoTipoUsuarioRepository.DeletarTipoUsuario(id);

            return Ok("Tipo de usuário deletado");
        }
    }
}

