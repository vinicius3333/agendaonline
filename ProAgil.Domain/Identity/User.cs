using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ProAgil.Domain.Identity
{
    public class User : IdentityUser<int>
    {
        public override int Id { get; set; }
        
        [Column(TypeName = "nvarchar(150)")]
        public string FullName { get; set; }
        public string Company { get; set; }
        public string MarketSegment { get; set; }
        public string ImagemPerfil { get; set; }
        public DateTime Abertura { get; set; }
        public DateTime Fechamento { get; set; }
        public string Duracao { get; set; }
        public List<UserRole> UserRoles { get; set; }
        public int? AgendaId { get{ return AutoIncrementAgendaId(); } }

        public int AutoIncrementAgendaId()
        { 
            var number = this.Id; 
            return number;
        }

        public virtual Agenda Agenda { get;}
    }
}
