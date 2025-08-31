using DataAccess.Data;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Utility.DBInitializer;

namespace Utility.DBInitializer
{
    public class DBInitializer : IDBInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public DBInitializer(
            ApplicationDbContext context,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public void Initialize()
        {
            // 1️⃣ Apply pending migrations
            if (_context.Database.GetPendingMigrations().Any())
            {
                _context.Database.Migrate();
            }

            // 2️⃣ Create roles if not exist
            if (!_roleManager.Roles.Any())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.SuperAdmin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Assistant)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Instructor)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Doctor)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.ExternalStudent)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.UniversityStudent)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Employee)).GetAwaiter().GetResult();

                // 3️⃣ Create SuperAdmin
                _userManager.CreateAsync(new ApplicationUser
                {
                    FirstName= "Super",
                    LastName="Admin",
                    FullName = "Super Admin",
                    UserName = "SuperAdmin",
                    Email = "Superadmin@eraasoft.com",
                    EmailConfirmed = true
                }, "Admin123+").GetAwaiter().GetResult();

                var user = _userManager.FindByNameAsync("SuperAdmin").GetAwaiter().GetResult();
                if (user != null)
                {
                    _userManager.AddToRoleAsync(user, SD.SuperAdmin).GetAwaiter().GetResult();
                }

                SeedDoctors();
                SeedAssistants();
            }
        }

        private void SeedDoctors()
        {
            var doctors = new List<(string FirstName, string LastName, string Email)>
            {
                ("Ahmed", "Ali", "ahmed.ali@uni.com"),
                ("Sara", "Hassan", "sara.hassan@uni.com"),
                ("Mohamed", "Khaled", "mohamed.khaled@uni.com"),
                ("Nour", "Samir", "nour.samir@uni.com"),
                ("Omar", "Ibrahim", "omar.ibrahim@uni.com"),
                ("Mina", "Romany", "mina.romany@uni.com"),
                ("Youssef", "Mahmoud", "youssef.mahmoud@uni.com"),
                ("Huda", "Fathy", "huda.fathy@uni.com"),
                ("Adel", "Lotfy", "adel.lotfy@uni.com"),
                ("Hany", "Saleh", "hany.saleh@uni.com"),
                ("Mostafa", "Gad", "mostafa.gad@uni.com"),
                ("Rana", "Nabil", "rana.nabil@uni.com"),
                ("Eman", "Kamal", "eman.kamal@uni.com"),
                ("Farah", "Zaki", "farah.zaki@uni.com"),
                ("Tamer", "Amin", "tamer.amin@uni.com")
            };

            foreach (var (first, last, email) in doctors)
            {
                if (_userManager.FindByEmailAsync(email).GetAwaiter().GetResult() == null)
                {
                    var appUser = new ApplicationUser
                    {
                        FirstName = first,
                        LastName = last,
                        FullName = $"{first} {last}",
                        Email = email,
                        UserName = email,
                        EmailConfirmed = true
                    };

                    _userManager.CreateAsync(appUser, "Doctor123+").GetAwaiter().GetResult();
                    _userManager.AddToRoleAsync(appUser, SD.Doctor).GetAwaiter().GetResult();

                    var doctor = new Doctor
                    {
                        ApplicationUserId = appUser.Id,
                    };
                    _context.Doctors.Add(doctor);
                }
            }

            _context.SaveChanges();
        }
        private void SeedAssistants()
        {
            var assistants = new List<(string FirstName, string LastName, string Email)>
        {
        ("Mariam", "Adel", "mariam.adel@uni.com"),
        ("Khaled", "Mostafa", "khaled.mostafa@uni.com"),
        ("Salma", "Youssef", "salma.youssef@uni.com"),
        ("Ola", "Ibrahim", "ola.ibrahim@uni.com"),
        ("Hassan", "Fouad", "hassan.fouad@uni.com"),
        ("Nada", "Tarek", "nada.tarek@uni.com"),
        ("Yara", "Maged", "yara.maged@uni.com"),
        ("Karim", "Fathy", "karim.fathy@uni.com"),
        ("Laila", "Hany", "laila.hany@uni.com"),
        ("Sherif", "Mahmoud", "sherif.mahmoud@uni.com"),
        ("Doaa", "Nabil", "doaa.nabil@uni.com"),
        ("Rami", "Samir", "rami.samir@uni.com"),
        ("Aya", "Kamal", "aya.kamal@uni.com"),
        ("Heba", "Lotfy", "heba.lotfy@uni.com"),
        ("Mostafa", "Amin", "mostafa.amin@uni.com")
        };
            foreach (var (first, last, email) in assistants)
            {
                if (_userManager.FindByEmailAsync(email).GetAwaiter().GetResult() == null)
                {
                    var user = new ApplicationUser
                    {
                        UserName = email,
                        Email = email,
                        FullName = $"{first} {last}",
                        FirstName = first,
                        LastName = last,
                        EmailConfirmed = true
                    };

                    _userManager.CreateAsync(user, "Assistant123+").GetAwaiter().GetResult();
                    _userManager.AddToRoleAsync(user, SD.Assistant).GetAwaiter().GetResult();

                    var assistantEntity = new Assistant
                    {
                        ApplicationUserId = user.Id
                    };

                    _context.Assistants.Add(assistantEntity);
                }
            }
            _context.SaveChanges();

        }

    }
}
