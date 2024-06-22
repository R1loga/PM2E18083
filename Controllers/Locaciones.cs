using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using System.Threading.Tasks;
using PM2E18083.Models;

namespace PM2E18083.Controllers
{
    public class Locaciones
    {
        SQLiteAsyncConnection _connection;

        //Constructor vacio
        public Locaciones() { }

        //Conexion a la base de datos
        public async Task Init()
        {
            try
            {
                if (_connection is null)
                {
                    SQLite.SQLiteOpenFlags extensiones = SQLite.SQLiteOpenFlags.ReadWrite | SQLite.SQLiteOpenFlags.Create | SQLite.SQLiteOpenFlags.SharedCache;

                    if (string.IsNullOrEmpty(FileSystem.AppDataDirectory))
                    {
                        return;
                    }

                    _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, "DBPersonas"), extensiones);

                    var creacion = await _connection.CreateTableAsync<Models.Locaciones_>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Init(): {ex.Message}");
            }
        }

        //Crear metodos crud para la clase personas
        //Create
        public async Task<int> storeAutor(Locaciones_ autor)
        {
            await Init();
            if (autor.Id == 0)
            {
                return await _connection.InsertAsync(autor);
            }
            else
            {
                return await _connection.UpdateAsync(autor);
            }
        }

        //Update
        public async Task<int> updateAutor(Locaciones_ autor)
        {
            await Init();
            return await _connection.UpdateAsync(autor);
        }

        //Read
        public async Task<List<Models.Locaciones_>> getListAutor()
        {
            await Init();
            return await _connection.Table<Locaciones_>().ToListAsync();
        }

        //Read Element
        public async Task<Models.Locaciones_> getAutors(int pid)
        {
            await Init();
            return await _connection.Table<Locaciones_>().Where(i => i.Id == pid).FirstOrDefaultAsync();
        }

        //Delete
        public async Task<int> deleteAutor(int autorID)
        {
            await Init();
            var autorToDelete = await getAutors(autorID);

            if (autorToDelete != null)
            {
                return await _connection.DeleteAsync(autorToDelete);
            }

            return 0;
        }
    }
}
