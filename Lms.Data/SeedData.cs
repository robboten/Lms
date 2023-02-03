using AutoBogus;
using Bogus;
using Lms.Core.Models.Entities;
using Lms.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Data
{
    internal class SeedData
    {
        private static LmsApiContext _context;
        public static async Task InitAsync(LmsApiContext context) {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            var ran = new Random();

            var faker = new Faker<Tournament>()
                .StrictMode(false)
                .Rules((f, o) =>
                {
                    o.Title= f.Random.Word();
                    o.StartDate = f.Date.Between(DateTime.Now.AddDays(-30), DateTime.Now.AddDays(30));
                    o.Games = new Faker<Game>()
                    .StrictMode(false)
                    .Rules((f, o) =>
                    {
                        o.Title = f.Random.Word();
                        o.StartDate = f.Date.Future();
                    })
                    .Generate(ran.Next(3,7)).ToList();
                });

            var faked = faker.Generate(20);


            await _context.AddRangeAsync(faked);
            await _context.SaveChangesAsync();
        }

    }
}
