﻿using PRUEBA.CORE.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRUEBA.CORE.Interfaces.Repositorio
{
    public interface IUnitOfWork
    {
        //IUsuarioRepositorio<Usuario> Personas { get; }
        void Save();
    }
}
