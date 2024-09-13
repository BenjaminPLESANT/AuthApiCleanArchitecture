using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace AuthApi.Infrastructure.Identity;

public class ApplicationUser : IdentityUser<Guid>
{

}