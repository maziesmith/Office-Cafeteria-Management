using System;
using System.Collections.Generic;
using System.Linq;
using Cafeteria.CoreLibs.Abstractions;
using Cafeteria.CoreLibs.DomainModel;

namespace Cafeteria.CoreLibs.Services
{
    public class FoodRepository : IFoodRepository
    {
        private const string All = "All";
        private readonly List<FoodItem> _cachedItems = new List<FoodItem>();
        private static readonly Random Random = new Random(DateTime.Now.Millisecond);

        public IEnumerable<FoodItem> GetFoodItems()
        {
            if (_cachedItems.Count > 0) return _cachedItems;
            _cachedItems.AddRange(GetEverAvailableItems());
            _cachedItems.AddRange(GetBreakFastItems());
            _cachedItems.AddRange(GetLunchItems());
            _cachedItems.AddRange(GetSupperItems());
            return _cachedItems;
        }

        public FoodItem GetFoodInfo(int id)
        {
            var items = GetFoodItems();
            return items.FirstOrDefault(item => item.Id == id);
        }

        private static IEnumerable<FoodItem> GetEverAvailableItems()
        {
            var availability = new Availability
            {
                AvailabilityDays = new List<string> { All },
                StartTime = TimeSpan.FromHours(0),
                EndTime = TimeSpan.FromHours(24)
            };
            return new[]
            {
                GetVegSandwich(availability),
                GetEggBhurji(availability),
                GetDosa(availability),
                GetUtthapam(availability),
                GetOmelette(availability),
                GetChickenSandwich(availability),
                GetBoiledEgg(availability)
            };
        }

        private static IEnumerable<FoodItem> GetBreakFastItems()
        {
            var availability = new Availability
            {
                AvailabilityDays = new List<string> { All },
                StartTime = TimeSpan.FromHours(8),
                EndTime = TimeSpan.FromHours(11)
            };
            return new[]
            {
                GetBesanChila(availability),
                GetIdli(availability)
                ,
            };
        }


        private static IEnumerable<FoodItem> GetLunchItems()
        {
            var availability = new Availability
            {
                AvailabilityDays = new List<string> { All },
                StartTime = TimeSpan.FromHours(12),
                EndTime = TimeSpan.FromHours(16)
            };
            return new[]
            {
               GetNonVegMini(availability),
               GetVegHealth(availability),
               GetVegMini(availability),
               GetFullVeg(availability)
            };
        }


        private static IEnumerable<FoodItem> GetSupperItems()
        {
            var availability = new Availability
            {
                AvailabilityDays = new List<string> { All },
                StartTime = TimeSpan.FromHours(16),
                EndTime = TimeSpan.FromHours(20)
            };
            return new[]
            {
                GetBhelpuri(availability),
                GetVegMomos(availability),
                GetPanipuri(availability)
            };
        }

        private static FoodItem GetNonVegMini(Availability availability)
        {
            return new FoodItem
            {
                Id = 13,
                Availability = availability,
                Rating = GetRandomRating(),
                Details = new FoodDetails
                {
                    Name = "Non Veg Mini",
                    Description = "Non Veg mini meal with 2 sabzi including daal and rice.",
                    Price = 65,
                    Calories = 250,
                    Image = ImageHelper.GetNonVegMiniImage()
                }
            };
        }

        private static FoodItem GetVegHealth(Availability availability)
        {
            return new FoodItem
            {
                Id = 12,
                Availability = availability,
                Rating = GetRandomRating(),
                Details = new FoodDetails
                {
                    Name = "Veg Health",
                    Description = "You don't have to count the calories with this veg health meal.",
                    Price = 95,
                    Calories = 150,
                    Image = ImageHelper.GetVegHealthImage()
                }
            };
        }

        private static FoodItem GetFullVeg(Availability availability)
        {
            return new FoodItem
            {
                Id = 11,
                Availability = availability,
                Rating = GetRandomRating(),
                Details = new FoodDetails
                {
                    Name = "Full Veg",
                    Description = "Veg full meal with 4 sabzi including daal, rice and sweetdish.",
                    Price = 45,
                    Calories = 350,
                    Image = ImageHelper.GetFullVegImage()
                }
            };
        }
        private static FoodItem GetVegMini(Availability availability)
        {
            return new FoodItem
            {
                Id = 10,
                Availability = availability,
                Rating = GetRandomRating(),
                Details = new FoodDetails
                {
                    Name = "Veg Mini",
                    Description = "Veg mini meal with 2 sabzi including daal and rice.",
                    Price = 45,
                    Calories = 280,
                    Image = ImageHelper.GetMiniVegImage()
                }
            };
        }

        private static FoodItem GetBhelpuri(Availability availability)
        {
            return new FoodItem
            {
                Id = 9,
                Availability = availability,
                Rating = GetRandomRating(),
                Details = new FoodDetails
                {
                    Name = "Bhelpuri",
                    Description = "Try this Mumbai special bhelpuri to satisfy your taste buds.",
                    Price = 25,
                    Calories = 80,
                    Image = ImageHelper.GetBhelpuriImage()
                }
            };
        }


        private static FoodItem GetPanipuri(Availability availability)
        {
            return new FoodItem
            {
                Id = 14,
                Availability = availability,
                Rating = GetRandomRating(),
                Details = new FoodDetails
                {
                    Name = "Pani puri",
                    Description = "Try this Mumbai special panipuri to satisfy your tsate buds.",
                    Price = 25,
                    Calories = 70,
                    Image = ImageHelper.GetPanipuriImage()
                }
            };
        }

        private static FoodItem GetVegMomos(Availability availability)
        {
            return new FoodItem
            {
                Id = 15,
                Availability = availability,
                Rating = GetRandomRating(),
                Details = new FoodDetails
                {
                    Name = "Veg Momos",
                    Description = "These specialities from East India will leave you craving for more",
                    Price = 80,
                    Calories = 160,
                    Image = ImageHelper.GetMomosImage()
                }
            };
        }


        private static FoodItem GetChickenSandwich(Availability availability)
        {
            return new FoodItem
            {
                Id = 8,
                Availability = availability,
                Rating = GetRandomRating(),
                Details = new FoodDetails
                {
                    Name = "Chicken Sandwich",
                    Description = "Chicken sandwich with loads of chicken chunks.",
                    Price = 65,
                    Calories = 140,
                    Image = ImageHelper.GetSandwichImage()
                }
            };
        }

        private static FoodItem GetVegSandwich(Availability availability)
        {
            return new FoodItem
            {
                Id = 7,
                Availability = availability,
                Rating = GetRandomRating(),
                Details = new FoodDetails
                {
                    Name = "Veg Sandwich",
                    Description = "Veg sandwich with loads of vegetables.",
                    Price = 55,
                    Calories = 90,
                    Image = ImageHelper.GetSandwichImage()
                }
            };
        }


        private static FoodItem GetBoiledEgg(Availability availability)
        {
            return new FoodItem
            {
                Id = 6,
                Availability = availability,
                Rating = GetRandomRating(),
                Details = new FoodDetails
                {
                    Name = "Boiled Egg",
                    Description = "Monday ho ya Sunday, Roz khao Ande",
                    Price = 20,
                    Calories = 20,
                    Image = ImageHelper.GetBoiledEggImage()
                }
            };
        }


        private static FoodItem GetEggBhurji(Availability availability)
        {
            return new FoodItem
            {
                Id = 9,
                Availability = availability,
                Rating = GetRandomRating(),
                Details = new FoodDetails
                {
                    Name = "Egg Bhurji",
                    Description = "Our best selling dish, the bhurji paav",
                    Price = 35,
                    Calories = 120,
                    Image = ImageHelper.GetBhurjiImage()
                }
            };
        }


        private static FoodItem GetOmelette(Availability availability)
        {
            return new FoodItem
            {
                Id = 5,
                Availability = availability,
                Rating = GetRandomRating(),
                Details = new FoodDetails
                {
                    Name = "Bread Omelette",
                    Description = "Start your day with a tasty ommellete",
                    Price = 40,
                    Calories = 150,
                    Image = ImageHelper.GetOmeletteImage()
                }
            };
        }

        private static FoodItem GetUtthapam(Availability availability)
        {
            return new FoodItem
            {
                Id = 4,
                Availability = availability,
                Rating = GetRandomRating(),
                Details = new FoodDetails
                {
                    Name = "Utthapam",
                    Description = "Craving for pizza. Try this healthier option instead",
                    Price = 45,
                    Calories = 120,
                    Image = ImageHelper.GetUtthapamImage()
                }
            };
        }

        private static FoodItem GetDosa(Availability availability)
        {
            return new FoodItem
            {
                Id = 3,
                Availability = availability,
                Rating = GetRandomRating(),
                Details = new FoodDetails
                {
                    Name = "Dosa",
                    Description = "Taste the most famous dish of south India",
                    Price = 40,
                    Calories = 180,
                    Image = ImageHelper.GetDosaImage()
                }
            };
        }

        private static FoodItem GetBesanChila(Availability availability)
        {
            return new FoodItem
            {
                Id = 2,
                Availability = availability,
                Rating = GetRandomRating(),
                Details = new FoodDetails
                {
                    Name = "Besan Chila",
                    Description = "A dish which is craved by all",
                    Price = 50,
                    Calories = 150,
                    Image = ImageHelper.GetBesanChilaImage()
                }
            };
        }

        private static FoodItem GetIdli(Availability availability)
        {
            return new FoodItem
            {
                Id = 1,
                Availability = availability,
                Rating = GetRandomRating(),
                Details = new FoodDetails
                {
                    Name = "Idli",
                    Description = "South Indian speciality Idli.",
                    Price = 30,
                    Calories = 100,
                    Image = ImageHelper.GetIdliImage()
                }
            };
        }

        private static Rating GetRandomRating()
        {
            var randomRating = GetRandomStar();

            return new Rating
            {
                OverallRating = randomRating,
                Reviews = Enumerable.Range(1, 8).Select(i => new Review { Feedback = GetRandomFeedback(), User = GeRandomUser(), Rating = GetRandomStar() })
            };
        }

        private static double GetRandomStar()
        {
            var randomRating = Random.NextDouble() + Random.Next(1, 5);
            return randomRating;
        }

        private static string GeRandomUser()
        {
            var users = new List<string>
            {
                "Harsh",
                "Tilak",
                "Ankita",
                "Prinshul",
                "Arun",
                "Akanksha",
                "Sameer",
                "Saurabh",
                "Anuj",
                "Siddharth",
                "Shrutika",
                "Vibhor",
                "Ahmed",
                "Sherry",
                "Pooja",
                "Payal",
                "Dharam",
                "Vaibhav",
                "Ankit",
                "Neha"
            };
            var randomUser = Random.Next(0, users.Count);
            return users[randomUser];
        }

        private static string GetRandomFeedback()
        {
            var feedbacks = new List<string>
            {
                "Amazing food. Love it everytime",
                "Quality can be better. Although good as compared to the price",
                "Very econimical and healthy at the same time.",
                "All my colleagues order this item most of the time",
                "Somewhat expensive as compared to what we get outside",
                "I eat this item everyday",
                "Sometimes overcooked but fine overall",
                "Can't be better than this"
            };
            var randomFeedback = Random.Next(0, feedbacks.Count);
            return feedbacks[randomFeedback];
        }
    }
}
