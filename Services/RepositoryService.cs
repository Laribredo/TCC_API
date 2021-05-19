using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TCC_API.DTO;
using TCC_API.Infrastructure.Enums;

namespace TCC_API.Services
{
    public class RepositoryService
    {
        private FormatsImages formats;

        public async Task<ResponseRepositoryDTO> UploadImagens(IFormFile file)
        {
            ResponseRepositoryDTO response = new ResponseRepositoryDTO(); 
            if (file == null || file.Length == 0)
            {
                response.isSaved = false;
            }           
            try
            {
                string formato = file.FileName.Substring(file.FileName.Length - 3);

                bool formatoAceito = Enum.TryParse(formato.ToUpper(), out formats); 

                if(!formatoAceito)
                {
                    response.isSaved = false;
                    return response;
                }

                string imagem = ObjectId.GenerateNewId().ToString() + "." + formato;

                string filePath = Path.Combine("C:\\TCC_API\\TCC_API\\Imagens", imagem);
                using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                response.isSaved = true;
                response.file = imagem;

                return response;
            }catch(Exception e )
            {
                response.isSaved = false;
                return response;
            }
            

        }
    }
}
