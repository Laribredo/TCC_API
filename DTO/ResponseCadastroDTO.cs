using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TCC_API.DTO
{
    public class ResponseCadastroDTO
    {
        public bool cadastrado { get; set; }
        public List<string> erros;
    }
}
