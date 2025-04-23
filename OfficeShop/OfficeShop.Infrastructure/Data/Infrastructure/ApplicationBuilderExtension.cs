using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using OfficeShop.Infrastructure.Data.Domain;
using System;
using System.Threading.Tasks;
using OfficeShop.Infrastructure.Data;
using OfficeShop.Data;

public static class ApplicationBuilderExtension
{
    public static async Task<IApplicationBuilder> PrepareDatabase(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();

        var services = serviceScope.ServiceProvider;
        var data = services.GetRequiredService<ApplicationDbContext>();

        SeedCategories(data);
        SeedBrands(data);

        await RoleSeeder(services);
        await SeedAdministrator(services);

        return app;
    }
    private static void SeedCategories(ApplicationDbContext dataCategory)
    {
        if (dataCategory.Categories.Any())
        {
            return;
        }

        dataCategory.Categories.AddRange(new[]
        {
        new Category { CategoryName = "Крака и плотове за маси" },
        new Category { CategoryName = "Поставка за лаптоп" },
        new Category { CategoryName = "Контейнер" },
        new Category { CategoryName = "Шкаф" },
        new Category { CategoryName = "Маса" },
        new Category { CategoryName = "Стол" },
        new Category { CategoryName = "Бюро" },
    });

        dataCategory.SaveChanges();
    }
    private static void SeedBrands(ApplicationDbContext dataBrand)
    {
        if (dataBrand.Brands.Any())
        {
            return;
        }

        dataBrand.Brands.AddRange(new[]
        {
        new Brand { BrandName = "LAGKAPTEN" },
        new Brand { BrandName = "GLADHOJDEN" },
        new Brand { BrandName = "VEBJORN" },
        new Brand { BrandName = "MITTCIRKEL" },
        new Brand { BrandName = "LOMMARP" },
        new Brand { BrandName = "KALLAX" },
        new Brand { BrandName = "MICKE" },
    });

        dataBrand.SaveChanges();
    }


    private static async Task RoleSeeder(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        string[] roleNames = { "Administrator", "Client" };

        IdentityResult roleResult;

        foreach (var role in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(role);

            if (!roleExist)
            {
                roleResult = await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }

    private static async Task SeedAdministrator(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        if (await userManager.FindByNameAsync("admin") == null)
        {
            ApplicationUser user = new ApplicationUser()
            {
                FirstName = "admin",
                LastName = "admin",
                UserName = "admin",
                Email = "admin@admin.com",
                Address = "admin address",
                PhoneNumber = "0888888888"
            };

            var result = await userManager.CreateAsync(user, "Admin123456!");

            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(user, "Administrator").Wait();
            }
        }
    }
}
