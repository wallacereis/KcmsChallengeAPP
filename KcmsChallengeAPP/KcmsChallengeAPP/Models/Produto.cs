using Realms;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KcmsChallengeAPP.Models
{
    public class Produto : RealmObject
    {
        [PrimaryKey]
        public string ProdutoID { get; set; } = Guid.NewGuid().ToString();
        public string Descricao { get; set; }
        public string Detalhes { get; set; }

        public decimal Preco { get; set; }
        public decimal PrecoPromocional { get; set; }

        public string Imagem { get; set; }
        public string ImagemURL { get; set; }
        public byte[] ImagemByte { get; set; }
        public string ContentType { get; set; }

        public DateTimeOffset DataCadastro { get; set; }
        public bool Ativo { get; set; }

        /* EF Relations - ManyToOne */
        public Categoria Categoria { get; set; }
        public string CategoriaID { get; set; }

        public string StrDescricao => string.Format("{0}", Descricao.Length > 15 ? Descricao.Substring(0, 15) + " ..." : Descricao);
        public string StrDetalhes => string.Format("{0}", Detalhes.Length > 65 ? Detalhes.Substring(0, 65) + " ..." : Detalhes);
    }
}
