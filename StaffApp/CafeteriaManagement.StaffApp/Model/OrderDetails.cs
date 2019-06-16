using Cafeteria.CoreLibs.DomainModel;

namespace CafeteriaManagement.StaffApp.Model
{
    public class OrderDetails
    {
        public string OrderId { get; set; }

        public FoodItem FoodItem { get; set; }
    }
}
