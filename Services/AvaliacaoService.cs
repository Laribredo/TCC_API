using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCC_API.Infrastructure.Context;
using TCC_API.Infrastructure.Repository;

namespace TCC_API.Services
{
    public class AvaliacaoService
    {
        public bool AvaliarRestaurante(AvaliacoesRestaurantes avaliacoes)
        {
            try
            {
                MongoDbContext context_ = new MongoDbContext();
                avaliacoes.id = Guid.NewGuid();
                context_.AvaliacoesRestaurantes.InsertOne(avaliacoes);

                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
