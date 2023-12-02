using IdentityModel;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace IdentityServer;

public class SeedData
{
    public static void EnsureDbPopulatedWithUsers(IServiceProvider sp)
    {
        using var dbContext = sp.GetRequiredService<ApplicationDbContext>();
        if (dbContext.Database.GetPendingMigrations().Any())
        {
            dbContext.Database.Migrate();
        }
        if (!dbContext.Users.Any())
        {
            var userMgr = sp.GetRequiredService<UserManager<ApplicationUser>>();
            var admin = new ApplicationUser
            {
                UserName = "admin"
            };
            var result = userMgr.CreateAsync(admin, password: "admin").Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
            result = userMgr.AddClaimAsync(admin, new Claim(JwtClaimTypes.Role, "Admin")
            ).Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            var bob = new ApplicationUser
            {
                UserName = "bob",
                Email = "BobSmith@email.com",
            };

            result = userMgr.CreateAsync(bob, password: "bob").Result;

            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            result = userMgr.AddClaimsAsync(bob, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Bob Smith"),
                            new Claim(JwtClaimTypes.GivenName, "Bob"),
                            new Claim(JwtClaimTypes.FamilyName, "Smith"),
                            new Claim(JwtClaimTypes.WebSite, "http://bob.com"),// TODO how can I  get other claim types? prob IProfileService
                            new Claim("location", "somewhere"),
                            new Claim(JwtClaimTypes.Role, "User")
                        }).Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
        }
    }
}
