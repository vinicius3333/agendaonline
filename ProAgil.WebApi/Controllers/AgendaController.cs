using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Domain;
using ProAgil.Repository;
using AutoMapper;
using ProAgil.WebApi.Dtos;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Linq;
using System;
using Microsoft.AspNetCore.Authorization;

namespace ProAgil.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendaController : ControllerBase
    {
        private readonly IAgendaRepository _repo;
        private readonly IMapper _mapper;

        public AgendaController(IAgendaRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet("Get")]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            try
            {
                var agendamentoDesatualizado = await _repo.teste();
                MotorRemocao(agendamentoDesatualizado);

                var agendamentoAtual = await _repo.teste();
                var results = _mapper.Map<AgendaDto[]>(agendamentoAtual);
                if (results != null)
                {
                    return Ok(results);
                }
                else
                {
                    return Ok(new AgendaDto());
                }

            }
            catch (System.Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [HttpGet("ListaAgendamentosPorUsuario/{usuarioId}")]
        public async Task<ActionResult> ListaAgendamentosPorUsuario(int usuarioId)
        {
            try
            {
                var agendamentoDesatualizado = await _repo.ObterTodosAgendamentosPorUsuarioAsync(usuarioId);
                MotorRemocao(agendamentoDesatualizado);
                //mandar a agenda atual para uma nova Lista

                var agendamentoAtual = await _repo.ObterTodosAgendamentosPorUsuarioAsync(usuarioId);
                var results = _mapper.Map<AgendaDto>(agendamentoAtual);

                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou");
            }
        }

        [HttpGet("ListaUsuariosPorAgenda/{agendaId}")]
        public async Task<ActionResult> ListaUsuariosPorAgenda(int agendaId)
        {
            try
            {
                //implementar deletar usuario

                var usuarios = await _repo.ObterTodosUsuariosAsync();
                var results = _mapper.Map<UserDto>(usuarios);

                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou");
            }
        }

        [HttpGet]
        public async Task<ActionResult> ListaDiasAgendados()
        {
            try
            {
                var results = await _repo.ObterDiasAgendadosAsync();

                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> AgendarCliente(AgendaDto model)
        {
            //Validações
            var agendamentoModel = _mapper.Map<Agenda>(model);

            var clientesAgendados = await _repo.ObterDataClientesAgendadosAsync(agendamentoModel);
            var horariosAtendimento = await _repo.ObterHorariosAtendimento();
            var horarioAgendado = model.DataHora.ToString("HH:mm");
            try
            {
                if (clientesAgendados != null)
                {
                    if (horariosAtendimento.Contains(horarioAgendado))
                    {
                        _repo.Add(agendamentoModel);
                        if (await _repo.SaveChangesAsync())
                        {
                            return Created($"/api/agenda/{model.Id}", _mapper.Map<AgendaDto>(agendamentoModel));
                        }
                    }
                    else
                    {
                        return this.BadRequest();
                    }
                }
                else
                {
                    return this.BadRequest();
                }
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Banco de dados Falhou {ex.Message}");
            }
            return BadRequest();
        }

        [AllowAnonymous]
        public async void MotorRemocao(Agenda[] agendamentos)
        {
            var horaAtual = DateTime.Now.ToString("HH:mm:ss");
            var idDataServicoFinalizado = _repo.ObterServicosFinalizadosAsync(agendamentos);
            var idDataServicosVencidos = _repo.ObterServicosVencidosAsync(agendamentos);

            if (idDataServicoFinalizado.Length > 0)
            {
                //Chamar Delete
                _repo.DeleteRange(idDataServicoFinalizado);
            }
            else if (idDataServicosVencidos.Length > 0)
            {
                //Chamar Delete
                _repo.DeleteRange(idDataServicosVencidos);
            }
            await _repo.SaveChangesAsync();
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName;
                    var fullPath = Path.Combine(pathToSave, fileName.Replace("\"", " ").Trim());

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }

                return Ok();
            }
            catch (System.Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, e);
            }

            return BadRequest("Erro ao tentar realizar Upload");
        }

        [HttpPut("{AgendaId}")]
        public async Task<IActionResult> Put(int AgendaId, AgendaDto model)
        {
            try
            {
                var agendamento = await _repo.ObterAgendamentoPorIdAsync(AgendaId);
                if (agendamento == null) return NotFound();

                _mapper.Map(model, agendamento);

                _repo.Update(agendamento);

                if (await _repo.SaveChangesAsync())
                {
                    return Created($"/api/agenda/{model.Id}", _mapper.Map<AgendaDto>(agendamento));
                }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou");
            }
            return BadRequest();
        }

        [HttpDelete("{AgendaId}")]
        public async Task<IActionResult> Delete(int AgendaId)
        {
            try
            {
                var agendamento = await _repo.ObterAgendamentoPorIdAsync(AgendaId);
                if (agendamento == null) return NotFound();

                _repo.Delete(agendamento);

                if (await _repo.SaveChangesAsync())
                {
                    return Ok();
                }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou");
            }
            return BadRequest();
        }
    }
}