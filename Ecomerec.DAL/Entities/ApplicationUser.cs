using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Ecomerec.DAL
{
    public class ApplicationUser : IdentityUser<int>
    {
        public Cart Cart { get; set; }

    }
}