using System;
using System.Collections.Generic;

#nullable disable

namespace Senai_SP_Medical_Group_WebAPI.Domains
{
    public partial class ImagemUsuario
    {
        public short Id { get; set; }
        public short? IdUsuario { get; set; }
        public byte[] Binario { get; set; }
        public string MymeType { get; set; }
        public string NomeArquivo { get; set; }
        public DateTime DataInclusao { get; set; }

        public virtual Usuario IdUsuarioNavigation { get; set; }
    }
}
