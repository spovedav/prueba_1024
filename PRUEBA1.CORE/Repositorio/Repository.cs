using PRUEBA.CORE.Interfaces.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using PRUEBA1.CORE.Utilities;

namespace PRUEBA.CORE.Repositorio
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly string connectionString;
        private readonly string tableName;
        public Repository()
        {
            this.connectionString = ConfigInicial.CadenaConexion;
            tableName = typeof(T).Name; // Utiliza el nombre del tipo como nombre de tabla
        }

        public IEnumerable<T> Find(object parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters), "Los parámetros no pueden ser nulos.");
            }

            string tableName = typeof(T).Name; // Obtener el nombre de la tabla basado en la entidad T
            if (!IsValidTableName(tableName))
            {
                throw new InvalidOperationException("El nombre de la tabla no es válido.");
            }

            // Construir la cláusula WHERE de manera dinámica
            var whereClause = new List<string>();
            var props = parameters.GetType().GetProperties();

            foreach (var prop in props)
            {
                whereClause.Add($"{prop.Name} = @{prop.Name}"); // e.g., "Nombre = @Nombre"
            }

            // Si no hay parámetros, la cláusula WHERE será vacía (traer todos los registros)
            string whereSql = whereClause.Any() ? $"WHERE {string.Join(" AND ", whereClause)}" : string.Empty;

            // Crear la consulta SQL
            var sql = $"SELECT * FROM {tableName} {whereSql}";

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Ejecutar la consulta y retornar el resultado
                    return connection.Query<T>(sql, parameters).ToList();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Ocurrió un error al ejecutar la consulta en la base de datos.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error inesperado.", ex);
            }
        }

        public IEnumerable<T> GetAll()
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                return connection.Query<T>($"SELECT * FROM {tableName}").ToList();
            }
        }

        public T GetById(object parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters), "Los parámetros no pueden ser nulos.");
            }

            string tableName = typeof(T).Name; // Obtener el nombre de la tabla basado en la entidad T
            if (!IsValidTableName(tableName))
            {
                throw new InvalidOperationException("El nombre de la tabla no es válido.");
            }

            // Construir la cláusula WHERE de manera dinámica
            var whereClause = new List<string>();
            var props = parameters.GetType().GetProperties();

            foreach (var prop in props)
            {
                whereClause.Add($"{prop.Name} = @{prop.Name}"); // e.g., "IdUsuario = @IdUsuario"
            }

            // Crear el comando SQL
            var sql = $"SELECT * FROM {tableName} WHERE {string.Join(" AND ", whereClause)}";

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Ejecutar la consulta utilizando los parámetros dinámicos
                    var result = connection.QuerySingleOrDefault<T>(sql, parameters);

                    if (result == null)
                    {
                        throw new KeyNotFoundException("No se encontró un registro con los parámetros proporcionados.");
                    }

                    return result;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Ocurrió un error al ejecutar la consulta en la base de datos.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error inesperado.", ex);
            }
        }

        public void Add(T entity)
        {
            var propertiesWithValues = Utilities.GetPropertiesWithTypes(entity);

            var columns = string.Join(", ", propertiesWithValues.Keys);
            var parameters = string.Join(", ", propertiesWithValues.Keys);
            var sql = $"INSERT INTO {tableName} ({columns}) VALUES (@{parameters})";

            var valuesDict = new DynamicParameters();
            foreach (var prop in propertiesWithValues)
            {
                valuesDict.Add($"@{prop.Key}", prop.Value.Valor);
            }

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(sql, valuesDict);
            }
        }

        public void Update(T entity)
        {
            var propertiesWithValues = Utilities.GetPropertiesWithTypes(entity);

            var primaryKey = "Id";

            var setClause = new List<string>();
            var parameters = new DynamicParameters();

            foreach (var prop in propertiesWithValues)
            {
                if (prop.Key != primaryKey)
                {
                    setClause.Add($"{prop.Key} = @{prop.Key}");
                    parameters.Add($"@{prop.Key}", prop.Value.Valor);
                }
            }

            parameters.Add($"@{primaryKey}", propertiesWithValues[primaryKey].Valor);

            var sql = $"UPDATE {tableName} SET {string.Join(", ", setClause)} WHERE {primaryKey} = @{primaryKey}";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(sql, parameters);  // Ejecutar la consulta usando Dapper
            }
        }

        public void Delete(object parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters), "Los parámetros no pueden ser nulos.");
            }

            string tableName = typeof(T).Name; // Obtener el nombre de la tabla basado en la entidad T
            if (!IsValidTableName(tableName))
            {
                throw new InvalidOperationException("El nombre de la tabla no es válido.");
            }

            // Construir la cláusula WHERE de manera dinámica
            var whereClause = new List<string>();
            var props = parameters.GetType().GetProperties();

            foreach (var prop in props)
            {
                whereClause.Add($"{prop.Name} = @{prop.Name}"); // e.g., "IdUsuario = @IdUsuario"
            }

            // Crear el comando SQL DELETE
            var sql = $"DELETE FROM {tableName} WHERE {string.Join(" AND ", whereClause)}";

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    connection.Execute(sql, parameters);
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Ocurrió un error al ejecutar la consulta en la base de datos.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error inesperado.", ex);
            }
        }

        private bool IsValidTableName(string tableName)
        {
            var validTables = new HashSet<string> { "Usuario", "Agente" };
            return validTables.Contains(tableName);
        }
    }
}
