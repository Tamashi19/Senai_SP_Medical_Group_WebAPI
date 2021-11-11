using System;
using System.Collections.Generic;

#nullable disable

namespace Senai_SP_Medical_Group_WebAPI.Domains
{
    public partial class Situacao
    {
        public Situacao()
        {
            Consulta = new HashSet<Consultum>();
        }

        public byte IdSituacao { get; set; }
        public string Situacao1 { get; set; }

        public virtual ICollection<Consultum> Consulta { get; set; }
    }
}
