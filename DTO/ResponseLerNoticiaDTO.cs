using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TCC_API.DTO
{
    public class ResponseLerNoticiaDTO
    {
        public Guid id { get; set; }
        public string apelido { get; set; }
        public int qtdComentarios { get; set; }
        public string titulo { get; set; }
        public string conteudo  { get; set; }
        public DateTime dtCriacao { get; set; }
        public string imagem { get; set; }
    }
}
