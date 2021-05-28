using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TCC_API.Infrastructure.Repository
{
    public class EstadosCidades
    {
        public ObjectId id { get; set; }
        public string UF { get; set; }
        public string MUNICIPIO { get; set; }
    }
}
