using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCC_API.DTO;
using TCC_API.Infrastructure.Context;
using TCC_API.Infrastructure.Enums;
using TCC_API.Infrastructure.Repository;

namespace TCC_API.Services
{
    public class ReceitasService
    {
        public List<ResponseReceitasRecentesDTO> GetAllReceitas()
        {
            ResponseReceitasRecentesDTO rs = new ResponseReceitasRecentesDTO();
            MongoDbContext context_ = new MongoDbContext();
            var receitas = context_.Receitas.AsQueryable();
            var usuario = context_.Usuarios.AsQueryable();
            var com = context_.ComentariosReceitas.AsQueryable();

            var sql = from rec in receitas
                      join usu in usuario on rec.IdUsuario equals (Guid)usu.id                      
                      join com_ in com on (Guid)rec.Id equals com_.idPublicacao into comentarios
                      select new ResponseReceitasRecentesDTO()
                      {
                          receita = rec.receita,
                          titulo = rec.Titulo,
                          apelido = usu.apelido,
                          conteudo = rec.Conteudo,
                          imagem = rec.Imagem,
                          dtCriacao = rec.dtCriacao,                          
                          qtdComentarios = comentarios.Count(),
                          id = (Guid)rec.Id
                      };


            var res = sql.OrderByDescending(s => s.dtCriacao).ToList();
            return res;
        }

        public List<ResponseReceitasRecentesDTO> GetAllReceitasDoce()
        {
            ResponseReceitasRecentesDTO rs = new ResponseReceitasRecentesDTO();
            MongoDbContext context_ = new MongoDbContext();
            var receitas = context_.Receitas.AsQueryable();
            var usuario = context_.Usuarios.AsQueryable();
            var com = context_.ComentariosReceitas.AsQueryable();

            var sql = from rec in receitas
                      join usu in usuario on rec.IdUsuario equals (Guid)usu.id
                      join com_ in com on (Guid)rec.Id equals com_.idPublicacao into comentarios
                      where rec.receita == CategoriasReceita.DOCE
                      select new ResponseReceitasRecentesDTO()
                      {
                          receita = rec.receita,
                          titulo = rec.Titulo,
                          apelido = usu.apelido,
                          conteudo = rec.Conteudo,
                          imagem = rec.Imagem,
                          dtCriacao = rec.dtCriacao,
                          qtdComentarios = comentarios.Count(),
                          id = (Guid)rec.Id
                      };


            var res = sql.ToList();
            return res;
        }

        public List<ResponseReceitasRecentesDTO> GetAllReceitasSalgados()
        {
            ResponseReceitasRecentesDTO rs = new ResponseReceitasRecentesDTO();
            MongoDbContext context_ = new MongoDbContext();
            var receitas = context_.Receitas.AsQueryable();
            var usuario = context_.Usuarios.AsQueryable();
            var com = context_.ComentariosReceitas.AsQueryable();

            var sql = from rec in receitas
                      join usu in usuario on rec.IdUsuario equals (Guid)usu.id
                      join com_ in com on (Guid)rec.Id equals com_.idPublicacao into comentarios
                      where rec.receita == CategoriasReceita.SALGADO    
                      select new ResponseReceitasRecentesDTO()
                      {
                          receita = rec.receita,
                          titulo = rec.Titulo,
                          apelido = usu.apelido,
                          conteudo = rec.Conteudo,
                          imagem = rec.Imagem,
                          dtCriacao = rec.dtCriacao,
                          qtdComentarios = comentarios.Count(),
                          id = (Guid)rec.Id
                      };


            var res = sql.ToList();
            return res;
        }

        public List<ResponseReceitasRecentesDTO> GetMinhasReceitas(Guid idUsuario)
        {
            ResponseReceitasRecentesDTO rs = new ResponseReceitasRecentesDTO();
            MongoDbContext context_ = new MongoDbContext();
            var receitas = context_.Receitas.AsQueryable();
            var usuario = context_.Usuarios.AsQueryable();
            var com = context_.ComentariosNoticias.AsQueryable();

            var sql = from rec in receitas
                      join usu in usuario on rec.IdUsuario equals (Guid)usu.id
                      join com_ in com on (Guid)rec.Id equals com_.idPublicacao into comentarios
                      where usu.id == idUsuario
                      select new ResponseReceitasRecentesDTO()
                      {
                          receita = rec.receita,
                          titulo = rec.Titulo,
                          apelido = usu.apelido,
                          conteudo = rec.Conteudo,
                          qtdComentarios = comentarios.Count(),
                          imagem = rec.Imagem,
                          dtCriacao = rec.dtCriacao,
                          id = (Guid)rec.Id
                      };


            var res = sql.OrderByDescending(s => s.dtCriacao).ToList();
            return res;
        }


        public Receitas GetReceitasByIdClass(Guid idReceita)
        {
            MongoDbContext context_ = new MongoDbContext();
            var receita = context_.Receitas.Find(s => (Guid)s.Id == idReceita).FirstOrDefault();
            return receita;
        }


        public ResponseLerReceitaDTO GetReceitaById(Guid idReceita)
        {
            ResponseReceitasRecentesDTO rs = new ResponseReceitasRecentesDTO();
            MongoDbContext context_ = new MongoDbContext();
            var receitas = context_.Receitas.AsQueryable();
            var usuario = context_.Usuarios.AsQueryable();
            var com = context_.ComentariosNoticias.AsQueryable();

            var sql = from rec in receitas
                      join usu in usuario on rec.IdUsuario equals (Guid)usu.id
                      join com_ in com on (Guid)rec.Id equals com_.idPublicacao into comentarios
                      where rec.Id == idReceita
                      select new ResponseLerReceitaDTO()
                      {
                          receita = rec.receita,
                          titulo = rec.Titulo,
                          apelido = usu.apelido,
                          conteudo = rec.Conteudo,
                          qtdComentarios = comentarios.Count(),
                          imagem = rec.Imagem,
                          dtCriacao = rec.dtCriacao,                          
                          id = (Guid)rec.Id
                      };


            var res = sql.FirstOrDefault();
            return res;
        }


        public ResponseCadastroDTO UpdateReceita(Receitas receitas)
        {
            ResponseCadastroDTO response = new ResponseCadastroDTO();
            try
            {
                MongoDbContext context_ = new MongoDbContext();                
                context_.Receitas.ReplaceOne(s => s.Id == receitas.Id, receitas);
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


        public ResponseCadastroDTO AddReceita(Receitas receitas)
        {
            ResponseCadastroDTO response = new ResponseCadastroDTO();
            try
            {
                MongoDbContext context_ = new MongoDbContext();
                receitas.Id = Guid.NewGuid();
                context_.Receitas.InsertOne(receitas);
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

        public bool DeleteReceitaById(Guid id)
        {
            try
            {
                MongoDbContext context_ = new MongoDbContext();
                var noticias = context_.Receitas.DeleteOne(s => s.Id == id);

                return true;

            }
            catch (Exception e)
            {
                return false;
            }

        }
    }
}
