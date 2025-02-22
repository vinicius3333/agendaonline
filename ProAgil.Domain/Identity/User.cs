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
        public TimeSpan Abertura { get; set; }
        public TimeSpan Fechamento { get; set; }
        public TimeSpan Duracao { get; set; }
        public List<UserRole> UserRoles { get; set; }
        public virtual Agenda Agenda { get;}
    }
}