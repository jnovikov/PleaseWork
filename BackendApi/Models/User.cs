using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BackendApi.Models
{
    public class User
    {
        public int Id { get; set; }
        
        [MaxLength(150)]
        public string Email { get; set; }
        
        [MaxLength(150)]
        public string Password { get; set; }
        
        [MaxLength(150)]
        public string Name { get; set; }
        
        public virtual ICollection<Deadline> Deadlines { get; set; }
       
    }
}