using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StaffMgtApi.Models;

namespace StaffMgtApi.Data
{
    public class StaffMgtApiContext : DbContext
    {
        public StaffMgtApiContext (DbContextOptions<StaffMgtApiContext> options)
            : base(options)
        {
        }

        public DbSet<StaffModel> StaffModel { get; set; } = default!;
    }
}
