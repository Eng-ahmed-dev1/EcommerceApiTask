namespace Ecomerec.BLL.DTOs
{
    public class CartWriteDTO
    {
        public ICollection<CartItemWriteDTO> CartItems { get; set; } = new HashSet<CartItemWriteDTO>();
    }
}