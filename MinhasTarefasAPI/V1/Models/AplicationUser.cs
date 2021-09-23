using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MinhasTarefasAPI.V1.Models
{
    public class AplicationUser : IdentityUser
    {
        public string FullNamed { get; set; }
        [ForeignKey("UsuarioId")]
        public ICollection<Tarefa> Tarefas { get; set; }
        [ForeignKey("UsuarioId")]
        public ICollection<Token> Tokens { get; set; }
    }
}
