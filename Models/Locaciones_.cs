using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SQLite;

namespace PM2E18083.Models
{
    [Table("Locaciones")]
    public class Locaciones_
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string latitud { get; set; }
        public string longitud { get; set; }
        [MaxLength(200)]
        public string descrip { get; set; } = string.Empty;
        public string Foto { get; set; } = string.Empty;
       
    }
}
