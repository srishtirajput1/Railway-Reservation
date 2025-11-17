using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace RailwayReservation.Services
{
    public class PasswordHasher
    {
        //check if a password is Valid or not
        public bool IsPasswordValid(string password)
        {
            // Regular expression for validating password
            string pattern = @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[\W_]).{8,}$";
            return Regex.IsMatch(password, pattern);
        }
        // Hash password with a salt and return the hash
        public string HashPassword(string password, out string salt)
        {
            // Generate a random salt (16 bytes)
            byte[] saltBytes = new byte[16];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes); // Fill the salt array with random values
            }

            // Generate the hash using PBKDF2 (Rfc2898DeriveBytes)
            var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 10000);  // 10000 iterations
            byte[] hashBytes = pbkdf2.GetBytes(32);  // Hash length (32 bytes)

            // Combine salt and hash into one byte array
            byte[] hashWithSalt = new byte[48];  // 16 bytes salt + 32 bytes hash
            Array.Copy(saltBytes, 0, hashWithSalt, 0, 16);
            Array.Copy(hashBytes, 0, hashWithSalt, 16, 32);

            // Convert to Base64 for storing in DB
            salt = Convert.ToBase64String(saltBytes);
            return Convert.ToBase64String(hashWithSalt);
        }

        // Verify entered password by comparing with the stored hash
        public bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        {
            byte[] hashBytes = Convert.FromBase64String(storedHash);
            byte[] saltBytes = Convert.FromBase64String(storedSalt);

            // Extract the stored password hash (which is the 32 bytes after the salt)
            byte[] storedPasswordHash = new byte[32];
            Array.Copy(hashBytes, 16, storedPasswordHash, 0, 32);  // Extract stored hash (after salt)

            // Rehash the entered password using the stored salt
            var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, saltBytes, 10000);  // Use the same salt and iterations
            byte[] enteredPasswordHash = pbkdf2.GetBytes(32);  // 32 bytes hash length

            // Compare the hashes byte-by-byte
            for (int i = 0; i < 32; i++)
            {
                if (storedPasswordHash[i] != enteredPasswordHash[i])
                {
                    return false; // Passwords don't match
                }
            }

            return true; // Passwords match
        }
    }
}
