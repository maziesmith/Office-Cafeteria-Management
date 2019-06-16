namespace Cafeteria.CoreLibs.DomainModel
{
    public class Order
    {
        public int FoodId { get; set; }

        public int OrderId { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public Payment Payment { get; set; }
    }
}