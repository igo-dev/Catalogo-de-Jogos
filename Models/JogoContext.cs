using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalogo_de_jogos.Models
{
    public class JogoContext : DbContext
    {
        public JogoContext(DbContextOptions<JogoContext> opt):base(opt)
        {

        }
        public DbSet<JogoModel> Jogos { get; set; }
    }
}
