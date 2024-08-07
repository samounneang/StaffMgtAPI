using Microsoft.EntityFrameworkCore;
using StaffMgtApi.Models;

namespace StaffMgtApi.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new StaffMgtApiContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<StaffMgtApiContext>>()))
            {
                // Look for any movies.
                if (context.StaffModel.Any())
                {
                    return;   // DB has been seeded
                }
                context.StaffModel.AddRange(
                    new StaffModel
                    {
                        StaffId = "DCC001",
                        FullName = "Long Heng",
                        Birthday = DateTime.Now.AddYears(-18),
                        Gender = StaffGender.Male
                    },
                     new StaffModel
                     {  
                         StaffId = "DCC002",
                         FullName = "Sok Chan",
                         Birthday = DateTime.Now.AddYears(-18),
                         Gender = StaffGender.Male
                     },
                     new StaffModel
                     {
                         StaffId = "DCC003",
                         FullName = "Meas Tola",
                         Birthday = DateTime.Now.AddYears(-18),
                         Gender = StaffGender.Male
                     },
                     new StaffModel
                     {
                         StaffId = "DCC004",
                         FullName = "Pov Pisey",
                         Birthday = DateTime.Now.AddYears(-18),
                         Gender = StaffGender.Male
                     }
                );
                context.SaveChanges();
            }
        }
    }
}
