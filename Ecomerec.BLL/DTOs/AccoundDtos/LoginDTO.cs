using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;
using System.Security.Permissions;

namespace Ecomerec.BLL.DTOs.AccoundDtos
{
    public class LoginDTO
    {
        [Required] public string UserName {  get; set; }    
        [Required][MinLength(8)][DataType(DataType.Password)] public string Password { get; set; }

    }
}