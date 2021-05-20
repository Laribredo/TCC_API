using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TCC_API.Infrastructure.Repository
{
    public class Usuarios
    {
        public Guid? id { get; set; }
        public string nome { get; set; }
        public string cpf { get; set; }
        public string email { get; set; }
        public string senha { get; set; }
        public string apelido { get; set; }
        public DateTime dataNascimento { get; set; }
    }
}
