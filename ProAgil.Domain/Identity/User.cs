using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ProAgil.Domain.Identity
{
    public class User : IdentityUser<int>
    {
        [Column(TypeName = "nvarchar(150)")]
        public string FullName { get; set; }
        public string Company { get; set; }
        public string MarketSegment { get; set; }
        public string ImagemPerfil { get; set; }
        public List<UserRole> UserRoles { get; set; }
        public int? AgendaId { get; set; }
        public Agenda Agenda { get;}
    }
}
