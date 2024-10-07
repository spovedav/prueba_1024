using PRUEBA.CORE.Entity;
using PRUEBA.CORE.Interfaces.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRUEBA.CORE.Repositorio
{
    public class UsuarioRepositorio : Repository<Usuario>, IUsuarioRepositorio
    {
        public UsuarioRepositorio()
        {

        }
    }
}
