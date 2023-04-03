using ApplicationCore.Utils;
using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceUsuario : IServiceUsuario
    {       
        public IEnumerable<Usuarios> GetUsuarios()
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            return repository.GetUsuarios();
        }
        public Usuarios GetUsuarioByID(int id)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            Usuarios usuario = new Usuarios();
            usuario = repository.GetUsuarioByID(id);
            usuario.contrasenna=Cryptography.DecrypthAES(usuario.contrasenna);
            return usuario;
        }

        public Usuarios GetUsuario(string email, string contrasenna)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            // Encriptar el password para poder compararlo

            string cryptPassword = Cryptography.EncrypthAES(contrasenna);

            return repository.GetUsuario(email, cryptPassword);
        }

        public Usuarios Guardar(Usuarios usuario)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            // Encriptar el password para guardarlo
            usuario.contrasenna = Cryptography.EncrypthAES(usuario.contrasenna);

            return repository.Guardar(usuario);
        }


    }
}
