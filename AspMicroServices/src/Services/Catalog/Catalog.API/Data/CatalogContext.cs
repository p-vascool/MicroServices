using Catalog.API.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        private readonly MongoClient mongoClient;
        private readonly IMongoDatabase database;
        public CatalogContext(IConfiguration configuration)
        {
            this.mongoClient = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            this.database = this.mongoClient.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));
            Products = this.database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));

            CatalogContextSeed.SeedData(Products);
        }
        public IMongoCollection<Product> Products { get; }
    }
}
