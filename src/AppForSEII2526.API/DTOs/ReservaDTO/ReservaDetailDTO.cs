namespace AppForSEII2526.API.DTOs.ReservaDTO
{
    public class ReservaDetailDTO : ReservaForCreateDTO
    {
        public int Id { get; set; }
        
        public ReservaDetailDTO(int id, string applicationUser, string clientAddress, PaymentMethod paymentMethod, DateTime date, IList<ReservaItemDTO> reservaItems)
        : base(applicationUser, clientAddress, paymentMethod, date, reservaItems)
        {
            Id = id;
        }
        public override bool Equals(object? obj)
        {
            return obj is ReservaDetailDTO dTO &&
                Id == dTO.Id &&
                base.Equals(obj) &&
                TotalPrice == dTO.TotalPrice;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Id);
        }
    }
}
