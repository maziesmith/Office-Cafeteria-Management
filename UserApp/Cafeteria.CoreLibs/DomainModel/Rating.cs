using System.Collections.Generic;

namespace Cafeteria.CoreLibs.DomainModel
{
    public class Rating
    {
        public double OverallRating { get; set; }

        public IEnumerable<Review> Reviews { get; set; }
        
    }
}