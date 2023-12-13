using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace CustomerApp.Infrastructure.Miscellaneous.Login;

internal sealed class PasswordHasher : IPasswordHasher
{
    private const int _byteSize = 128 / 8;
    private const int _iterationCount = 100000;
    private const int _requestedBytes = 256 / 8;
    public string Hash([DataType(DataType.Password)] string Password)
    {
        byte[] _salt = RandomNumberGenerator.GetBytes(_byteSize);
        byte[] _hashedPassword = KeyDerivation.Pbkdf2(
            password: Password,
            salt: _salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: _iterationCount,
            numBytesRequested: _requestedBytes
        );

        byte[] _result = new byte[_hashedPassword.Length + _byteSize];

        Array.Copy(_salt, _result, _byteSize);
        Array.Copy(_hashedPassword, 0, _result, 16, _hashedPassword.Length);

        return Convert.ToBase64String(_result);
    }

    public bool Verify([DataType(DataType.Password)] string Password,
        [DataType(DataType.Password)] string HashedPassword)
    {
        byte[] _all = Convert.FromBase64String(HashedPassword);
        byte[] _salt = _all[.._byteSize];
        byte[] _hashedPassword = _all[(_byteSize)..];

        byte[] _currentPassword = KeyDerivation.Pbkdf2(
            password: Password,
            salt: _salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: _iterationCount,
            numBytesRequested: _requestedBytes
        );

        return CryptographicOperations.FixedTimeEquals(_currentPassword, _hashedPassword);
    }
}
