using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ProAgil.Domain.Identity;

namespace ProAgil.WebApi.Dtos
{
    public class AgendaDto
    {

        public int Id { get; set; }

        [Required (ErrorMessage="Campo Nome é obrigatório")]
        [StringLength (100, MinimumLength=10, ErrorMessage="Preencha seu nome completo")]
        public string Nome { get; set; }
        
        [EmailAddress]
        [Required (ErrorMessage="Campo Email é obrigatório")]
        public string Email { get; set; }

        [Required (ErrorMessage="Campo Data é obrigatório")]
        public DateTime DataHora { get; set; }

        [Phone]
        [Required (ErrorMessage="Campo Celular é obrigatório")]
        public string Celular { get; set; }
        
        public List<UserDto> Usuarios { get; set; }
        public int? UserId { get; set; }
        public virtual User User { get; set; }
    }
}