using AIPoweredBlogPortfolio.API.dbContext;
using AIPoweredBlogPortfolio.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace AIPoweredBlogPortfolio.API.Services
{
    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext _context;

        public AdminService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Admin> Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var admin = await _context.Admins.SingleOrDefaultAsync(x => x.Username == username);

            if (admin == null)
                return null;

        
            if (!VerifyPasswordHash(password, admin.PasswordHash))
                return null;

            return admin;
        }

        public async Task<Admin> Create(Admin admin, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password is required");

            if (await _context.Admins.AnyAsync(x => x.Username == admin.Username))
                throw new ArgumentException("Username \"" + admin.Username + "\" is already taken");

            CreatePasswordHash(password, out string passwordHash);
            admin.PasswordHash = passwordHash;

            _context.Admins.Add(admin);
            await _context.SaveChangesAsync();

            return admin;
        }

        public async Task Delete(int id)
        {
            var admin = await _context.Admins.FindAsync(id);
            if (admin != null)
            {
                _context.Admins.Remove(admin);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Admin>> GetAll()
        {
            return await _context.Admins.ToListAsync();
        }

        public async Task<Admin> GetById(int id)
        {
            return await _context.Admins.FindAsync(id);
        }

        public async Task Update(Admin admin, string password = null)
        {
            var adminToUpdate = await _context.Admins.FindAsync(admin.AdminId);
            if (adminToUpdate == null)
                throw new ArgumentException("Admin not found");

            if (!string.IsNullOrWhiteSpace(admin.Username) && admin.Username != adminToUpdate.Username)
            {
                if (await _context.Admins.AnyAsync(x => x.Username == admin.Username))
                    throw new ArgumentException("Username " + admin.Username + " is already taken");

                adminToUpdate.Username = admin.Username;
            }

            if (!string.IsNullOrWhiteSpace(password))
            {
                CreatePasswordHash(password, out string passwordHash);
                adminToUpdate.PasswordHash = passwordHash;
            }

            if (!string.IsNullOrWhiteSpace(admin.Email))
                adminToUpdate.Email = admin.Email;

            await _context.SaveChangesAsync();
        }

        private static void CreatePasswordHash(string password, out string passwordHash)
        {
            using (var hmac = new HMACSHA512())
            {
                var salt = hmac.Key;
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                passwordHash = Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hash);
            }
        }

        private static bool VerifyPasswordHash(string password, string storedHash)
        {
            var parts = storedHash.Split(':');
            if (parts.Length != 2)
                return false;

            var salt = Convert.FromBase64String(parts[0]);
            var storedHashBytes = Convert.FromBase64String(parts[1]);

            using (var hmac = new HMACSHA512(salt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(storedHashBytes);
            }
        }
    }
}
