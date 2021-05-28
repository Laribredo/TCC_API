using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCC_API.DTO;
using TCC_API.Infrastructure.Context;
using TCC_API.Infrastructure.Repository;

namespace TCC_API.Services
{
    public class RestaurantesService
    {

        public List<string> GetUfs()
        {
            MongoDbContext context_ = new MongoDbContext();
            return context_.EstadosCidades.AsQueryable().Select(s => s.UF).Distinct().OrderBy(s => s).ToList();

        }
        public List<string> GetMunicipios(string uf)
        {
            MongoDbContext context_ = new MongoDbContext();
            var r = context_.EstadosCidades.AsQueryable().Where(s => uf == s.UF).Select(s => s.MUNICIPIO).OrderBy(s => s).ToList();
            return r;

        }

        public ResponseCadastroDTO cadastrarRestaurante(Restaurantes restaurantes)
        {

            ResponseCadastroDTO response = new ResponseCadastroDTO();
            try
            {
                MongoDbContext context_ = new MongoDbContext();
                restaurantes.id = Guid.NewGuid();
                context_.Restaurantes.InsertOne(restaurantes);
                response.cadastrado = true;
            }
            catch (Exception e)
            {
                response.cadastrado = false;
                response.erros = new List<string>();
                response.erros.Add("Ocorreu um erro ao se conectar com a base de dados.");
                throw;
            }

            return response;
        }

        public ResponseCadastroDTO updateRestaurante(Restaurantes restaurantes)
        {

            ResponseCadastroDTO response = new ResponseCadastroDTO();
            try
            {
                MongoDbContext context_ = new MongoDbContext();
                context_.Restaurantes.ReplaceOne(s => s.id == restaurantes.id,restaurantes);
                response.cadastrado = true;
            }
            catch (Exception e)
            {
                response.cadastrado = false;
                response.erros = new List<string>();
                response.erros.Add("Ocorreu um erro ao se conectar com a base de dados.");
                throw;
            }

            return response;
        }

        public List<Restaurantes> GetRestaurantesByIdUsuario(Guid idUsuario)
        {
            MongoDbContext context_ = new MongoDbContext();
            var rest = context_.Restaurantes.AsQueryable();

            return context_.Restaurantes.Find(s => s.idUsuario == idUsuario).ToList();
        }

        public Restaurantes GetRestaurantesById(Guid id)
        {
            MongoDbContext context_ = new MongoDbContext();
            var rest = context_.Restaurantes.AsQueryable();

            return context_.Restaurantes.Find(s => s.id == id).FirstOrDefault();
        }


        public bool DeleteRestauranteById(Guid id)
        {
            try
            {
                MongoDbContext context_ = new MongoDbContext();
                var noticias = context_.Restaurantes.DeleteOne(s => s.id == id);

                return true;

            }
            catch (Exception e)
            {
                return false;
            }

        }


        public List<ResponseRestaurantesRecentesDTO> GetRestaurantesByCidade(string cidade, Guid idUsuario)
        {
            MongoDbContext context_ = new MongoDbContext();
            var rest = context_.Restaurantes.AsQueryable();
            var us = context_.Usuarios.AsQueryable();
            var av = context_.AvaliacoesRestaurantes.AsQueryable();

            var sql = from restaurante in rest
                      join usuario in us on restaurante.idUsuario equals (Guid)usuario.id
                      join ava in av on (Guid)restaurante.id equals ava.idRestaurante into avaliacoes 
                      from avaliacao in avaliacoes.DefaultIfEmpty()                      
                      select new ResponseRestaurantesRecentesDTO()
                      {
                          DtCadastro = restaurante.dtCadastro,
                          Endereco = restaurante.Endereco,
                          id = (Guid)restaurante.id,
                          imagem = restaurante.imagem,
                          Municipio = restaurante.Municipio,
                          NomeRestaurante = restaurante.NomeRestaurante,
                          Numero = restaurante.Numero,
                          Uf = restaurante.UF,
                          idUsuario = (Guid)usuario.id,
                          apelido = usuario.apelido,
                          voto = avaliacao.nota
                      };
            var t = sql.Where(s => s.Municipio == cidade).ToList();
            for (int i = 0; i < t.Count(); i++)
            {
                var avaliacao = context_.AvaliacoesRestaurantes.Find(s => s.idRestaurante == t[i].id).ToList();
                var possuiVoto = avaliacao.Where(s => s.idUsuario == idUsuario).ToList();
                t[i].voto = possuiVoto.Count > 0 ? t[i].voto : null;
                if (avaliacao.Count() > 0)
                {                    
                   
                    var sum = avaliacao.Sum(s => s.nota) / avaliacao.Count();
                    t[i].media = sum;                   
                }
                else
                {
                    t[i].media = null;
                }


            }

            return t;
        }
    }
}
