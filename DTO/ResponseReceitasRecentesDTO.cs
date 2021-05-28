using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCC_API.Infrastructure.Enums;

namespace TCC_API.DTO
{
    public class ResponseReceitasRecentesDTO
    {
        public Guid id { get; set; }
        public string titulo { get; set; }
        public string apelido { get; set; }
        public string conteudo { get; set; }
        public string imagem { get; set; }
        public CategoriasReceita receita { get; set; }
        public int qtdComentarios { get; set; }
        public DateTime dtCriacao { get; set; }

    }
}
