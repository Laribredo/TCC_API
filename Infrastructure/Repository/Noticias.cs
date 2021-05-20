using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TCC_API.Infrastructure.Repository
{
    public class Noticias
    {
        public Guid? id { get; set; }
        public string titulo { get; set; }
        public string imagem { get; set; }
        public string  conteudo { get; set; }
        public Guid usuario { get; set; }
        public DateTime? dtCadastro { get; set; }
    }
}
