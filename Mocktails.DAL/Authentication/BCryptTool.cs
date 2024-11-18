using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mocktails.DAL.Authentication;
public static class BCryptTool
{
    // Utility class for hashing passwords and validating hashes using BCrypt.
    private static string GetRandomSalt() => BCrypt.Net.BCrypt.GenerateSalt(12);

    // Hashes the provided password using BCrypt. It automatically generates a salt.
    // The salt is internally included in the hash result.
    public static string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);

    // Verifies if the provided password matches the stored hash.
    // It hashes the password and compares it with the stored hash.
    public static bool ValidatePassword(string password, string correctHash) => BCrypt.Net.BCrypt.Verify(password, correctHash);
}
