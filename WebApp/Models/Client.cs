using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Client
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Cédula")]
        public int DocumentId { get; set; }
        [Required]
        [DisplayName("Nombre")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Apellido")]
        public string LastName { get; set; }
        [Required]
        [DisplayName("Teléfono")]
        public int Telephone { get; set; }
    }
}
