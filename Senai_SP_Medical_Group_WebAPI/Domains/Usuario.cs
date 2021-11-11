using System;
using System.Collections.Generic;

#nullable disable

namespace Senai_SP_Medical_Group_WebAPI.Domains
{
    public partial class Usuario
    {
        public Usuario()
        {
            ImagemUsuarios = new HashSet<ImagemUsuario>();
            Medicos = new HashSet<Medico>();
            Pacientes = new HashSet<Paciente>();
        }

        public short IdUsuario { get; set; }
        public byte? IdTipoUsuario { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        public virtual TipoUsuario IdTipoUsuarioNavigation { get; set; }
        public virtual ICollection<ImagemUsuario> ImagemUsuarios { get; set; }
        public virtual ICollection<Medico> Medicos { get; set; }
        public virtual ICollection<Paciente> Pacientes { get; set; }
    }
}
