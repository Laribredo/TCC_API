using MongoDB.Bson;
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
    public class UsuarioService
    {
        private MongoDbContext context_;

        public Usuarios Login(RequestLoginDTO request)
        {
            context_ = new MongoDbContext();
            Usuarios us = context_.Usuarios.Find(s => (s.email == request.login || s.apelido == request.login) && s.senha == request.senha).FirstOrDefault();
            return us;
        }
        
        public List<string> ValidaDados(Usuarios us) {

            context_ = new MongoDbContext();
            List<string> erros = new List<string>();
            if(GetUsuariosByCpf(us.cpf) != null)
            {
                erros.Add("Já existe esse CPF na nossa base de dados.");
            }
            if (GetUsuarioByEmail(us.email) != null)
            {
                erros.Add("Já existe esse email na nossa base de dados.");
            }
            if(GetUsuariosByApelido(us.apelido) != null)
            {
                erros.Add("Já existe esse apelido na nossa base de dados");
            }

            return erros; 

        }


        public Usuarios GetUsuariosByCpf(string cpf)
        {
            context_ = new MongoDbContext();
            Usuarios u =  context_.Usuarios.Find<Usuarios>(s => s.cpf == cpf).FirstOrDefault();
            return u;
        }

        public Usuarios GetUsuarioByEmail(string cpf)
        {
            context_ = new MongoDbContext();
            return context_.Usuarios.Find<Usuarios>(s => s.cpf == cpf).FirstOrDefault();
        }

        public Usuarios GetUsuariosByApelido(string apelido)
        {
            context_ = new MongoDbContext();
            return context_.Usuarios.Find<Usuarios>(s => s.apelido == apelido).FirstOrDefault();
        }

        public Usuarios GetUsuariosById(ObjectId id)
        {
            context_ = new MongoDbContext();
            return context_.Usuarios.Find<Usuarios>(s => s.id == id).FirstOrDefault();
        }

        public ResponseCadastroDTO AddUsuario(Usuarios us)
        {
            context_ = new MongoDbContext();
            ResponseCadastroDTO dt = new ResponseCadastroDTO();
            if(ValidaDados(us).Count == 0)
            {
                try
                {
                    us.id = ObjectId.GenerateNewId();
                    context_.Usuarios.InsertOne(us);
                    dt.cadastrado = true;
                }catch(Exception e)
                {
                    dt.cadastrado = false;
                    dt.erros.Add("Ocorreu um erro ao se comunicar com a base de dados, por favor teste novamente.");

                }
                
            }
            else
            {
                dt.cadastrado = false;
                dt.erros = ValidaDados(us);
            }
                       

            return dt;
        }
    }
}
