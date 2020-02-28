using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ProAgil.Domain;

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
        public DateTime Duracao { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public List<AgendaDto> Agendas { get; set; }
        public Agenda Agenda { get; set; }        
    }
}