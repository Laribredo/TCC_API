using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TCC_API.DTO
{
    public class ResponseNoticiasRecentesDTO
    {
        public Guid id { get; set; }
        public string titulo { get; set; }
        public string conteudo { get; set; }
        public DateTime dtCriacao { get; set; }
        public string apelido { get; set; }
        public string imagem { get; set; }
    }
}
