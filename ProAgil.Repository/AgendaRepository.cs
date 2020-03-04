using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;
using ProAgil.Domain.Identity;

namespace ProAgil.Repository
{
    public class AgendaRepository : IAgendaRepository
    {

        private readonly AgendaContext _context;

        public AgendaRepository(AgendaContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        ///GERAIS
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public void DeleteRange<T>(T[] entityArray) where T : class
        {
            _context.RemoveRange(entityArray);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<Agenda[]> teste()
        {
            IQueryable<Agenda> query = _context.Agendas;
            query = query.AsNoTracking();

            return await query.ToArrayAsync();
        }

        public async Task<Agenda[]> ObterTodosAgendamentosPorUsuarioAsync(int UserId)
        {
            IQueryable<Agenda> query = _context.Agendas.Where(x => x.UserId == UserId);
            query = query.AsNoTracking();

            return await query.ToArrayAsync();
        }
        
        public async Task<User[]> ObterTodosUsuariosAsync()
        {
            IQueryable<User> query = _context.Users.OrderByDescending(x => x.Id);
            query = query.AsNoTracking();

            return await query.ToArrayAsync();
        }

        public async Task<Agenda[]> ObterClientesAgendadosMesmaDataAsync(Agenda agenda)
        {
            IQueryable<Agenda> query = _context.Agendas.Where(a => a.DataHora == agenda.DataHora);
            query = query.AsNoTracking();

            return await query.ToArrayAsync();
        }

        public async Task<Agenda[]> ObterDiasAgendadosAsync()
        {
            IQueryable<Agenda> query = _context.Agendas.OrderBy(x => x.DataHora);
            query = query.AsNoTracking();

            return await query.ToArrayAsync();
        }

        public async Task<List<TimeSpan>> ObterHorariosAtendimento(Agenda agenda)
        {
            var duracao = _context.Users.Where(x => x.Id == agenda.UserId).Select(x => x.Duracao).ToList().First();
            var abertura = _context.Users.Where(x => x.Id == agenda.UserId).Select(x => x.Abertura).First();
            var fechamento = _context.Users.Where(x => x.Id == agenda.UserId).Select(x => x.Fechamento).ToList().First();

            List<TimeSpan> horarios = new List<TimeSpan>();
            TimeSpan calc = new TimeSpan();
            calc = abertura;
            horarios.Add(calc);
            while (calc < fechamento)
            {
                calc = calc.Add(duracao);
                horarios.Add(calc);
            }

            return horarios;
        }

        public Agenda[] ObterServicosFinalizadosAsync(Agenda[] agendamentos)
        {
            Agenda[] query = agendamentos.Where(a => a.DataHora <= DateTime.Now && a.DataHora.AddMinutes(50) <= DateTime.Now).ToArray();

            return query;
        }

        public Agenda[] ObterServicosVencidosAsync(Agenda[] agendamentos)
        {
            Agenda[] query = agendamentos.Where(a => a.DataHora < DateTime.Now).ToArray();

            return query;
        }
        public async Task<Agenda> ObterAgendamentoPorIdAsync(int AgendaId)
        {
            IQueryable<Agenda> query = _context.Agendas;
            query = query.AsNoTracking().OrderByDescending(c => c.DataHora)
                         .Where(c => c.Id == AgendaId);

            return await query.FirstOrDefaultAsync();
        }
    }
}