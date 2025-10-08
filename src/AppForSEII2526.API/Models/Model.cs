namespace AppForSEII2526.API.Models
{
    public class Model
    {
        public Model() { }
        
        public Model(int id, string name, IList<Car> cars)
        {
            Id = id;
            Name = name;
            Cars = cars;
        }

        [Key]
        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.", MinimumLength = 1)]
        public string Name { get; set; }

        public IList<Car> Cars { get; set; } = new List<Car>();
    }
}
