namespace BDAProject.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using BDAProject.Common;
    using Microsoft.AspNetCore.Identity;

    public class UsersSeeder : ISeeder
    {
        public UsersSeeder()
        {
        }

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Users.Any())
            {
                return;
            }

            var roleId = dbContext.Roles.FirstOrDefault(x => x.Name == GlobalConstants.AdministratorRoleName).Id;

           dbContext.Users.FirstOrDefault(x => x.Id == "f1f76718-25dd-4c3f-863e-ab9c1c248c1d").Roles.Add(new IdentityUserRole<string>()
            {
                RoleId = roleId,
                UserId = "f1f76718-25dd-4c3f-863e-ab9c1c248c1d",
            });

           await dbContext.SaveChangesAsync();
        }
    }
}
