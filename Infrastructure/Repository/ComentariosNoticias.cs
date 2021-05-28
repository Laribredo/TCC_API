using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TCC_API.Infrastructure.Repository
{
    public class ComentariosNoticias
    {
        public Guid? id { get; set; }
        public Guid idPublicacao { get; set; }
        public Guid idUsuario { get; set; }        
        [Required(ErrorMessage = "O campo de Descrição não é válido.")]
        public string  descricao { get; set; }
        public DateTime dtComentario { get; set; }
    }
}
