namespace Cafeteria.CoreLibs.DomainModel
{
    public class FoodDetails
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }

        public byte[] Image { get; set; }

        public int Calories { get; set; }
    }
}