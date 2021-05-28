using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TCC_API.Infrastructure.Repository
{
    public class Restaurantes
    {
        public Guid? id { get; set; }
        [Required(ErrorMessage = "É necessário possuir um UF")]
        public string UF { get; set; }
        public Guid idUsuario { get; set; }
        [Required(ErrorMessage = "É necessário possuir uma cidade")]
        public string Municipio{ get; set; }
        [Required(ErrorMessage = "É necessário possuir um endereço")]
        public string Endereco { get; set; }
        [Required(ErrorMessage = "É necessário possuir um Numero")]
        public string Numero  { get; set; }
        [Required(ErrorMessage = "É necessário possuir um CEP")]
        public string CEP { get; set; }
        public string imagem { get; set; }
        public string NomeRestaurante { get; set; }
        public DateTime dtCadastro { get; set; }
    }
}
