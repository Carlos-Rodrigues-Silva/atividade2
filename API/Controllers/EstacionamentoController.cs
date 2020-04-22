using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using API.Data;
using API.Dto;
using API.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstacionamentoController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public EstacionamentoController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet("estacionamentos")]
        public async Task<IReadOnlyList<Estacionamento>> GetEstacionamentos()
        {
            return await _dataContext.Estacionamentos.Include(e => e.Endereco).ToListAsync();

        }

        [HttpGet("estacionamento/{id}")]
        public async Task<IActionResult> GetEstacionamento(int id)
        {
            var estacionamento = await _dataContext.Estacionamentos.Include(e => e.Endereco).FirstOrDefaultAsync(x => x.Id == id);

            if (estacionamento == null)
            {
                return BadRequest(400);
            }

            return Ok(estacionamento);
        }

        [HttpPost("cadastrar")]
        public async Task<IActionResult> CadastrarEstacionamento(Estacionamento estacionamento)
        {
            var estacionamentoExiste = await _dataContext.Estacionamentos.FirstOrDefaultAsync(x => x.NomeEstacionamento == estacionamento.NomeEstacionamento && x.Endereco.Cep == estacionamento.Endereco.Cep);

            if (estacionamentoExiste != null)
            {
                return BadRequest("Estacionamento já cadastrado");
            }


            var cadastrarEstacionamento = new Estacionamento()
            {
                NomeEstacionamento = estacionamento.NomeEstacionamento,
                Descricao = estacionamento.Descricao,
                PrecoHora = estacionamento.PrecoHora,
                Avaliacao = estacionamento.Avaliacao,
                NumeroVagas = estacionamento.NumeroVagas,
                NumeroVagasDisponiveis = estacionamento.NumeroVagasDisponiveis,
                Endereco = estacionamento.Endereco
            };

            await _dataContext.Estacionamentos.AddAsync(cadastrarEstacionamento);
            await _dataContext.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("deletar/{id}")]
        public async Task<IActionResult> DeletarEstacionamento(int id)
        {
            var estacionamento = await _dataContext.Estacionamentos.FirstOrDefaultAsync(x => x.Id == id);

            if (estacionamento == null)
            {
                return BadRequest("Estacionamento não cadastrado");
            }

            _dataContext.Estacionamentos.Remove(estacionamento);
            await _dataContext.SaveChangesAsync();

            return Ok();

        }

        [HttpPut("atualizar/{id}")]
        public async Task<IActionResult> AtualizarEstacionamento(AtualizarEstacionamento atualizarEstacionamento, int id)
        {
            Estacionamento estacionamentoExiste = await _dataContext.Estacionamentos.Include(x => x.Endereco).FirstOrDefaultAsync(x => x.Id == id);

            if (estacionamentoExiste == null)
            {
                return BadRequest("Estacionamento não cadastrado");
            }
            else
            {
                estacionamentoExiste.NomeEstacionamento = atualizarEstacionamento.NomeEstacionamento;
                estacionamentoExiste.Descricao = atualizarEstacionamento.Descricao;
                estacionamentoExiste.PrecoHora = atualizarEstacionamento.PrecoHora;
                estacionamentoExiste.Avaliacao = atualizarEstacionamento.Avaliacao;
                estacionamentoExiste.NumeroVagas = atualizarEstacionamento.NumeroVagas;
                estacionamentoExiste.NumeroVagasDisponiveis = atualizarEstacionamento.NumeroVagasDisponiveis;

                estacionamentoExiste.Endereco.NomeLogradouro = atualizarEstacionamento.NomeLogradouro;
                estacionamentoExiste.Endereco.Numero = atualizarEstacionamento.Numero;
                estacionamentoExiste.Endereco.Cep = atualizarEstacionamento.Cep;
                estacionamentoExiste.Endereco.Bairro = atualizarEstacionamento.Bairro;
                estacionamentoExiste.Endereco.Cidade = atualizarEstacionamento.Cidade;
                estacionamentoExiste.Endereco.Estado = atualizarEstacionamento.Estado;
                estacionamentoExiste.Endereco.TipoLogradouro = atualizarEstacionamento.TipoLogradouro;

                await _dataContext.SaveChangesAsync();

                return Ok();
            };
        }
    }
}