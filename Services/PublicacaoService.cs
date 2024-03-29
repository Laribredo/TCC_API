﻿using Microsoft.AspNetCore.Http;
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
using MongoDB.Bson.Serialization;

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

        public List<ResponseNoticiasRecentesDTO> GetAllNoticias()
        {
            try
            {
                MongoDbContext context_ = new MongoDbContext();
                var obj = context_.Publicaoes;
                var user = context_.Usuarios;
                var comentario = context_.ComentariosNoticias;

                var query = from not in obj.AsQueryable()
                            join us in user.AsQueryable() on not.usuario equals (Guid)us.id
                            join coment in comentario.AsQueryable() on (Guid)not.id equals coment.idPublicacao into comentarios
                            select new ResponseNoticiasRecentesDTO()
                            {
                                apelido = us.apelido,
                                conteudo = not.conteudo,
                                dtCriacao = (DateTime)not.dtCadastro,
                                id = (Guid)not.id,
                                titulo = not.titulo,
                                imagem = not.imagem,
                                qtdComentarios = comentarios.Count()
                            };

             
                var t = query.OrderByDescending(s => s.dtCriacao).ToList();

                return t;
            }catch(Exception)
            {
                return new List<ResponseNoticiasRecentesDTO>();
            }
         
        }

        public ResponseCadastroDTO UpdatePublicacao(Noticias noticias)
        {
            ResponseCadastroDTO response = new ResponseCadastroDTO();

            try
            {
                List<string> erros = ValidaNoticias(noticias);
                if (erros.Count == 0)
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
            catch (Exception e)
            {
                response.cadastrado = false;
                response.erros = new List<string>();
                response.erros.Add("Ocorreu um erro na base de dados");
            }

            return response;
        }

        public ResponseLerNoticiaDTO GetPublicaoById(string id)
        {
            MongoDbContext context_ = new MongoDbContext();
            var noticia = context_.Publicaoes.AsQueryable();
            var usuario = context_.Usuarios.AsQueryable();
            var comentario = context_.ComentariosNoticias.AsQueryable();

            var sql = from not in noticia
                      join user in usuario on not.usuario equals (Guid)user.id
                      join coment in comentario on (Guid)not.id equals coment.idPublicacao into comentarios
                      where not.id.Equals(id)
                      select new ResponseLerNoticiaDTO()
                      {
                          id = (Guid)not.id,
                          apelido = user.apelido,
                          qtdComentarios = comentarios.Count(),
                          conteudo = not.conteudo,
                          dtCriacao = (DateTime)not.dtCadastro,
                          titulo = not.titulo,
                          imagem = not.imagem
                      };
            var obj = sql.FirstOrDefault();
            return obj;
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

                }
                catch (Exception e)
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

        public List<Noticias> GetPublicacoesRecentesByUsuario(Guid id)
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

            }
            catch (Exception e)
            {
                return false;
            }

        }
    }
}
