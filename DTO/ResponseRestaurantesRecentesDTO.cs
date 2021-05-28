using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCC_API.Infrastructure.Repository;

namespace TCC_API.DTO
{
    public class ResponseRestaurantesRecentesDTO
    {
        public Guid id { get; set; }
        public string Endereco { get; set; }
        public string Municipio { get; set; }
        public string Uf { get; set; }
        public string NomeRestaurante { get; set; }
        public string imagem { get; set; }
        public string Numero { get; set; }
        public DateTime DtCadastro { get; set; }
        public Guid idUsuario { get; set; }
        public string apelido { get; set; }
        public int? voto { get; set; }
        public IEnumerable<AvaliacoesRestaurantes> avaliacoes { get; set; }
        public double? media { get; set; }
    }
}
