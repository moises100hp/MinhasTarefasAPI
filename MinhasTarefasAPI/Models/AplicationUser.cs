using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MinhasTarefasAPI.Models
{
    public class AplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        [ForeignKey("UsuarioId")]
        public ICollection<Tarefa> Tarefas { get; set; }
    }
}
