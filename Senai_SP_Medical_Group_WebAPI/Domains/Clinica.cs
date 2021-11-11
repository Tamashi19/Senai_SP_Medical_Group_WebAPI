using System;
using System.Collections.Generic;

#nullable disable

namespace Senai_SP_Medical_Group_WebAPI.Domains
{
    public partial class Clinica
    {
        public Clinica()
        {
            Medicos = new HashSet<Medico>();
        }

        public short IdClinica { get; set; }
        public string NomeClinica { get; set; }
        public string Endereço { get; set; }
        public string Cnpj { get; set; }
        public string RazaoSocial { get; set; }
        public TimeSpan HorarioAbertura { get; set; }
        public TimeSpan HorarioFechamento { get; set; }

        public virtual ICollection<Medico> Medicos { get; set; }
    }
}
