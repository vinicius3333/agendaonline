using System.Collections.Generic;
using System.Threading.Tasks;
using ProAgil.Domain;
using ProAgil.Domain.Identity;

namespace ProAgil.Repository
{
    public interface IAgendaRepository
    {
         void Add<T>( T entity) where T : class;
         void Update<T>( T entity) where T : class;
         void Delete<T>( T entity) where T : class;
         void DeleteRange<T>( T[] entity) where T : class;

         Task<bool> SaveChangesAsync();
         ///EVENTOS
         Task<Agenda[]> teste();
         Task<Agenda[]> ObterTodosAgendamentosPorUsuarioAsync(int usuarioId);
         Task<Agenda> ObterAgendamentoPorIdAsync(int agendamentoId);
         Task<User[]> ObterTodosUsuariosAsync();
         Task<Agenda[]> ObterClientesAgendadosMesmaDataAsync(Agenda agenda);
         Task<Agenda[]> ObterDiasAgendadosAsync();
         Task<List<string>> ObterHorariosAtendimento();
         Agenda[] ObterServicosFinalizadosAsync(Agenda[] agendamentos);
         Agenda[] ObterServicosVencidosAsync(Agenda[] agendamentos);

    }
}