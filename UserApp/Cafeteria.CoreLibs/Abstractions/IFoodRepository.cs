using System.Collections.Generic;
using Cafeteria.CoreLibs.DomainModel;

namespace Cafeteria.CoreLibs.Abstractions
{
    public interface IFoodRepository
    {
        IEnumerable<FoodItem> GetFoodItems();

        FoodItem GetFoodInfo(int id);
    }
}
