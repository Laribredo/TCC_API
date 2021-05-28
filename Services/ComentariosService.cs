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
    public class ComentariosService
    {
        public List<ResponseComentariosDTO> GetComentarios(Guid idPublicacao)
        {
            MongoDbContext context_ = new MongoDbContext();
            var comentarios = context_.ComentariosNoticias;
            var usuarios = context_.Usuarios;

            var sql = from comentario in comentarios.AsQueryable()
                      join usuario in usuarios.AsQueryable() on comentario.idUsuario equals (Guid)usuario.id
                      where comentario.idPublicacao.Equals(idPublicacao)
                      select new ResponseComentariosDTO()
                      {
                          apelido = usuario.apelido,
                          descricao = comentario.descricao,
                          dtComentario = comentario.dtComentario
                      };


            var teste = sql.ToList();

            return sql.ToList();


        }

        public bool ComentarNoticias(ComentariosNoticias comentarios)
        {
            try
            {
                MongoDbContext context_ = new MongoDbContext();
                comentarios.id = Guid.NewGuid();
                context_.ComentariosNoticias.InsertOne(comentarios);

                return true;

            }catch(Exception)
            {
                return false;
            }
        }

        public bool ComentarReceitas(ComentariosReceitas comentarios)
        {
            try
            {
                MongoDbContext context_ = new MongoDbContext();
                comentarios.id = Guid.NewGuid();
                context_.ComentariosReceitas.InsertOne(comentarios);

                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<ResponseComentariosDTO> GetComentariosReceitas(Guid idPublicacao)
        {
            MongoDbContext context_ = new MongoDbContext();
            var comentarios = context_.ComentariosReceitas;
            var usuarios = context_.Usuarios;

            var sql = from comentario in comentarios.AsQueryable()
                      join usuario in usuarios.AsQueryable() on comentario.idUsuario equals (Guid)usuario.id
                      where comentario.idPublicacao.Equals(idPublicacao)
                      select new ResponseComentariosDTO()
                      {
                          apelido = usuario.apelido,
                          descricao = comentario.descricao,
                          dtComentario = comentario.dtComentario
                      };


            var teste = sql.ToList();

            return sql.ToList();


        }
    }
}
