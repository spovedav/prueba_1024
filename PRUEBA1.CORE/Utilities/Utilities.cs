using PRUEBA.CORE.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRUEBA1.CORE.Utilities
{
    public static class Utilities
    {
        public static Dictionary<string, ValoresEntity> GetPropertiesWithTypes<T>(T obj)
        {
            var properties = typeof(T).GetProperties();
            var dictionary = new Dictionary<string, ValoresEntity>();

            foreach (var property in properties)
            {
                var propName = property.Name;            // Nombre de la propiedad (e.g., "Id", "Nombre", "Edad")
                var propValue = property.GetValue(obj);  // Valor actual de la propiedad en el objeto
                var propType = property.PropertyType.Name;  // Tipo de la propiedad (e.g., "Int32", "String")

                dictionary[propName] = new ValoresEntity
                {
                    Valor = propValue,
                    Tipo = propType
                };
            }

            return dictionary;
        }
    }
}
