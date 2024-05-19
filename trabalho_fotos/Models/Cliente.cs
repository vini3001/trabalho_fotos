using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace trabalho_fotos.Models
{
    public class Cliente
    {
        [Key]
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Cpf { get; set; }
        public byte[]? Photo { get; set;}
        public string? Street { get; set; }
        public string? StreetNumber { get; set; }
        public string? Cep { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }

    }
}
