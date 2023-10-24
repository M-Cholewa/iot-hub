using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Infrastructure.Security
{
    //https://raw.githubusercontent.com/dotnet/aspnetcore/main/src/Identity/Extensions.Core/src/IPasswordHasher.cs
    public interface IPasswordHasher
    {
        string HashPassword( string password);
        bool VerifyHashedPassword(string hashedPassword, string providedPassword);
    }
}
