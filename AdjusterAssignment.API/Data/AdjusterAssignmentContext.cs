using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using AdjusterAssignment.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdjusterAssignment.API.Data
{
    public class AdjusterAssignmentContext
    {
        public AdjusterAssignmentContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["DatabaseSettings:ConnectionString"]);
            var database = client.GetDatabase(configuration["DatabaseSettings:DatabaseName"]);

            AdjusterAssignments = database.GetCollection<AdjAssignment>(configuration["DatabaseSettings:CollectionName"]);
            SeedData(AdjusterAssignments);
        }

        public IMongoCollection<Product> Products { get; }

        public IMongoCollection<AdjAssignment> AdjusterAssignments { get; set; }


        public async Task InsertOneAsync(AdjAssignment adjAssignment)
        {
             await AdjusterAssignments.InsertOneAsync(adjAssignment).ConfigureAwait(false);
        }

        public async Task<bool> UpdateAsync(string id, AdjAssignment adjAssignment)
        {
            var updateResult = await AdjusterAssignments.UpdateOneAsync(rec => rec.Id == id, Builders<AdjAssignment>.Update.
                     Set(rec => rec.AdjusterName, adjAssignment.AdjusterName)
                    .Set(rec => rec.AdjuterId, adjAssignment.AdjuterId)
                    .Set(rec => rec.ClaimGroupId, adjAssignment.ClaimGroupId)
                    .Set(rec => rec.ClaimId, adjAssignment.ClaimId)
            ).ConfigureAwait(false);

            return updateResult.IsAcknowledged &&
                   updateResult.ModifiedCount > 0;
        }
        public async Task DeleteAsync(string id)
        {
            var updateResult = await AdjusterAssignments.DeleteOneAsync(rec => rec.Id == id).ConfigureAwait(false);
        }

        private static void SeedData(IMongoCollection<Product> productCollection)
        {
            bool existProduct = productCollection.Find(p => true).Any();
            if (!existProduct)
            {
                productCollection.InsertManyAsync(GetPreconfiguredProducts());
            }
        }

        private static void SeedData(IMongoCollection<AdjAssignment> assignmentCollection)
        {
            bool existAssignment = assignmentCollection.Find(p => true).Any();
            if (!existAssignment)
            {
                assignmentCollection.InsertManyAsync(GetAssignments());
            }
        }

        private static IEnumerable<Product> GetPreconfiguredProducts()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Name = "IPhone X",
                    Description = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                    ImageFile = "product-1.png",
                    Price = 950.00M,
                    Category = "Smart Phone"
                },
                new Product()
                {
                    Name = "Samsung 10",
                    Description = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                    ImageFile = "product-2.png",
                    Price = 840.00M,
                    Category = "Smart Phone"
                },
                new Product()
                {
                    Name = "Huawei Plus",
                    Description = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                    ImageFile = "product-3.png",
                    Price = 650.00M,
                    Category = "White Appliances"
                },
                new Product()
                {
                    Name = "Xiaomi Mi 9",
                    Description = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                    ImageFile = "product-4.png",
                    Price = 470.00M,
                    Category = "White Appliances"
                },
                new Product()
                {
                    Name = "HTC U11+ Plus",
                    Description = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                    ImageFile = "product-5.png",
                    Price = 380.00M,
                    Category = "Smart Phone"
                },
                new Product()
                {
                    Name = "LG G7 ThinQ EndofCourse",
                    Description = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                    ImageFile = "product-6.png",
                    Price = 240.00M,
                    Category = "Home Kitchen"
                },
                new Product()
                {
                    Name = "VU Television",
                    Description = "This is VU Television.",
                    ImageFile = "product-9.png",
                    Price = 290.00M,
                    Category = "Home Kitchen"
                }
            };
        }

        private static IList<AdjAssignment> GetAssignments()
        {
            return new List<AdjAssignment>()
            {
                new AdjAssignment()
                {
                    AdjusterName = "Martin Flower",
                    AdjuterId = Guid.NewGuid().ToString(),
                    ClaimGroupId="Auto",
                    ClaimId = "ORDFF1231313"
                },
                new AdjAssignment()
                {
                    AdjusterName = "Brian Adams",
                    AdjuterId = Guid.NewGuid().ToString(),
                    ClaimGroupId="Property",
                    ClaimId = "ORDFF1231123313"
                },
                new AdjAssignment()
                {
                    AdjusterName = "Hari Nattuva",
                    AdjuterId = Guid.NewGuid().ToString(),
                    ClaimGroupId="Health",
                    ClaimId = "ORDFF12667878"
                },
                new AdjAssignment()
                {
                    AdjusterName = "Diana Adams",
                    AdjuterId = Guid.NewGuid().ToString(),
                    ClaimGroupId="Auto",
                    ClaimId = "ORDFF123451313"
                },
                new AdjAssignment()
                {
                    AdjusterName = "Eric Peter",
                    AdjuterId = Guid.NewGuid().ToString(),
                    ClaimGroupId="Property",
                    ClaimId = "ORDFF761313"
                },
                new AdjAssignment()
                {
                    AdjusterName = "Ian juliet",
                    AdjuterId = Guid.NewGuid().ToString(),
                    ClaimGroupId="Health",
                    ClaimId = "ORDFG1231313"
                }
            };
        }

    }
}
