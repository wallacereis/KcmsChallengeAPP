using Realms;
using System;
using System.Collections.Generic;

namespace KcmsChallengeAPP.Models
{
    public class Categoria : RealmObject
    {
        [PrimaryKey]
        public string CategoriaID { get; set; } = Guid.NewGuid().ToString();
        public string Descricao { get; set; }
        public DateTimeOffset DataCadastro { get; set; }
        public bool Ativo { get; set; }

        /* EF Relations - OneToMany */
        //public IEnumerable<Produto> Produtos { get; set; }
    }
}
