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
         Task<Agenda[]> ObterTodosAgendamentosPorUsuarioAsync(int usuarioId);
         Task<Agenda> ObterAgendamentoPorIdAsync(int agendamentoId);
         Task<User[]> ObterTodosUsuariosPorAgendamentoAsync(int agendaId);
         Task<Agenda[]> ObterDataClientesAgendadosAsync(Agenda agenda);
         Task<Agenda[]> ObterDiasAgendadosAsync();
         Task<List<string>> ObterHorariosAtendimento();
         Task<Agenda[]> ObterIdsServicosFinalizadosAsync(Agenda[] agendamentos);
         Task<Agenda[]> ObterIdsServicosVencidosAsync(Agenda[] agendamentos);

    }
}