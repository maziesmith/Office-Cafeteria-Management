using System;

namespace Cafeteria.CoreLibs.DomainModel
{
    public class FoodItem
    {
        public int Id { get; set; }

        public FoodDetails Details { get; set; }

        public Rating Rating { get; set; }

        public Availability Availability { get; set; }

        public bool IsAvailableNow => Availability.StartTime < DateTime.Now.TimeOfDay &&
                                      Availability.EndTime > DateTime.Now.TimeOfDay;

        public override string ToString()
        {
            return Details.Name;
        }
    }
}
