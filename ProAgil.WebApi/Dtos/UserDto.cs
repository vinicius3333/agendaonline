using System;
using System.Collections.Generic;

namespace ProAgil.WebApi.Dtos
{
    public class UserDto
    {
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
        public int? AgendaId { get; set; }
    }
}