using Ledger.CrossCutting.Identity.Abstractions;
using Microsoft.AspNetCore.Identity;
using System;

namespace Ledger.CrossCutting.Identity.Models.Users
{
    public class LedgerIdentityUser : IdentityUser<Guid>, IUser
    {
    }
}
