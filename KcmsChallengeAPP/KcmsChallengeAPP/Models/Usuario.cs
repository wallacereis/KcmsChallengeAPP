using System;
using System.Collections.Generic;

namespace KcmsChallengeAPP.Models
{
    public class Usuario
    {
        public int UsuarioID { get; set; }
        public Guid UsuarioGuid { get; set; }
        public string CEP { get; set; }
        public string Numero { get; set; }
        public string PontoReferencia { get; set; }

        public string CPF { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string ConfirmarSenha { get; set; }
        public string TelefoneCelular { get; set; }

        public string Imagem { get; set; }
        public string ImagemURL { get; set; }
        public byte[] ImagemByte { get; set; }

        public DateTime DataNascimento { get; set; }
        public DateTime DataCadastro { get; set; }
        public string ContentType { get; set; }

        public bool Ativo { get; set; }
        public bool AceiteTermoUso { get; set; }

    }
}

