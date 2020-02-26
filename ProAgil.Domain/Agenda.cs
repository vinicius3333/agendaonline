using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProAgil.Domain.Identity;

namespace ProAgil.Domain
{
   public class Agenda
   {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public DateTime DataHora { get; set; }
        
        public string Celular { get; set; }

        public int? UserId { get; set; }

        public virtual User User { get;}
    }
}