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
            IQueryable<Agenda> query = _context.Agendas.Where(x => x.User.Id == UserId);
            query = query.AsNoTracking();

            return await query.ToArrayAsync();
        }
   
        public async Task<User[]> ObterTodosUsuariosAsync()
        {
            IQueryable<User> query = _context.Users;
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
            IQueryable<Agenda> query =  _context.Agendas.OrderBy(x => x.DataHora);
            query = query.AsNoTracking();    

            return await query.ToArrayAsync();
        }

        public async Task<List<string>> ObterHorariosAtendimento()
        {
            List<string> horarios = new List<string>();
            horarios.Add("09:30");    
            horarios.Add("10:20");    
            horarios.Add("11:10");
            horarios.Add("12:00");
            horarios.Add("12:50");
            horarios.Add("13:40");
            horarios.Add("14:30");
            horarios.Add("15:20");
            horarios.Add("16:10");
            horarios.Add("17:00");
            horarios.Add("17:50");
            horarios.Add("18:40");    

            return horarios;
        }

        public Agenda[] ObterServicosFinalizadosAsync(Agenda[] agendamentos)
        {
            Agenda[] query = agendamentos.Where(a => a.DataHora <= DateTime.Now.Date && a.DataHora.AddMinutes(50) <= DateTime.Now.Date).ToArray();    

            return query;
        }

        public Agenda[] ObterServicosVencidosAsync(Agenda[] agendamentos)
        {
            Agenda[] query = agendamentos.Where(a => a.DataHora < DateTime.Now.Date).ToArray();    

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