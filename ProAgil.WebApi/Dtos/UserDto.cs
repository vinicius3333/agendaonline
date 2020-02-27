using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProAgil.WebApi.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Company { get; set; }
        public string ImagemPerfil { get; set; }
        public string Email { get; set; }
        public string MarketSegment { get; set; }
        public DateTime Abertura { get; set; }
        public DateTime Fechamento { get; set; }
        public string Duracao { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public List<AgendaDto> Agendas { get; set; }
        public int? AgendaId { get{ return AutoIncrementAgendaId(); } }

        public int AutoIncrementAgendaId()
        { 
            var number = this.Id; 
            return number;
        }

    }
}