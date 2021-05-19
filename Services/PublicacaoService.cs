using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TCC_API.DTO;
using TCC_API.Infrastructure.Context;
using TCC_API.Infrastructure.Repository;
using MongoDB.Bson;
using MongoDB.Driver;

namespace TCC_API.Services
{
    public class PublicacaoService
    {

        public List<string> ValidaNoticias(Noticias noticias)
        {
            List<string> erros = new List<string>();
            if (String.IsNullOrEmpty(noticias.conteudo))
            {
                erros.Add("É necessário preencher um conteudo para a publicação");
            }
            if (String.IsNullOrEmpty(noticias.titulo))
            {
                erros.Add("É necessário preencher um titulo para a publicação");
            }

            return erros;
        }

        public List<Noticias> GetAllNoticias()
        {
            MongoDbContext context_ = new MongoDbContext();
            return context_.Publicaoes.Find(s => true).ToList();
        }

        public ResponseCadastroDTO UpdatePublicacao(Noticias noticias)
        {
            ResponseCadastroDTO response = new ResponseCadastroDTO();

            try
            {
                List<string> erros = ValidaNoticias(noticias);
                if(erros.Count == 0)
                {
                    MongoDbContext context_ = new MongoDbContext();
                    context_.Publicaoes.ReplaceOne(s => s.id == noticias.id, noticias);
                    response.cadastrado = true;
                }
                else
                {
                    response.cadastrado = false;
                    response.erros = erros;
                }
                
            }
            catch(Exception e )
            {
                response.cadastrado = false;
                response.erros = new List<string>();
                response.erros.Add("Ocorreu um erro na base de dados");
            }

            return response;
        }

        public Noticias GetPublicaoById(string id)
        {
            MongoDbContext context_ = new MongoDbContext();
            return context_.Publicaoes.Find(s => s.id == new Guid(id)).FirstOrDefault();
        }

        public ResponseCadastroDTO AddPublicacao(Noticias noticia)
        {
            ResponseCadastroDTO response = new ResponseCadastroDTO();
            MongoDbContext context_ = new MongoDbContext();
            List<string> erros = ValidaNoticias(noticia);

            if (erros.Count == 0)
            {
                try
                {
                    noticia.id = Guid.NewGuid();
                    context_.Publicaoes.InsertOne(noticia);
                    response.cadastrado = true;
                    response.erros = null;

                }catch(Exception e)
                {
                    
                    response.cadastrado = false;
                    response.erros = new List<string>();
                    erros.Add("Ocorreu um erro ao inserir a publicação.");
                }
                
            }
            else
            {
                response.cadastrado = false;
                response.erros = erros;
            }

            return response;

        }

        public List<Noticias> GetPublicacoesRecentesByUsuario(string id)
        {
            MongoDbContext context_ = new MongoDbContext();
            var ultimas = context_.Publicaoes.Find(s => s.usuario == id).ToList()
                .OrderByDescending(s => s.dtCadastro).Take(5).ToList();

            return ultimas;
        }


        public List<Noticias> GetAllPublicacoesByUser(string id)
        {
            MongoDbContext context_ = new MongoDbContext();
            var noticias = context_.Publicaoes.Find(s => true).ToList()
                .OrderByDescending(s => s.dtCadastro).ToList();

            return noticias;
        }

        public bool DeletePublicacaoById(string id)
        {
            try
            {
                MongoDbContext context_ = new MongoDbContext();
                var noticias = context_.Publicaoes.DeleteOne(s => s.id == new Guid(id));

                return true;

            }catch(Exception e)
            {
                return false;
            }
                   
        }
    }
}
