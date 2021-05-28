using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TCC_API.Infrastructure.Enums;

namespace TCC_API.Infrastructure.Repository
{
    public class Receitas
    {
        public Guid? Id { get; set; }
        public Guid IdUsuario { get; set; }
        [Required(ErrorMessage ="É necessário possuir um titulo")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "É necessário possuir um conteudo")]
        public string Conteudo { get; set; }
        public string Imagem { get; set; }
        public CategoriasReceita receita { get; set; }
        public DateTime dtCriacao { get; set; }
    }
}
