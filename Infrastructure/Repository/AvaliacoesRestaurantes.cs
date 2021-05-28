using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TCC_API.Infrastructure.Repository
{
    public class AvaliacoesRestaurantes
    {
        public Guid? id { get; set; }
        public Guid idRestaurante { get; set; }
        public Guid idUsuario { get; set; }
        public DateTime DtAvaliacao { get; set; }
        public int nota { get; set; }
    }
}
