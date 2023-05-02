using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using RazorPagesGeneral.Models;

using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace RazorPagesGeneral
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }


    [Serializable]
    public class Testimonial
    {
        public int Id { get; set; }
        public string? CommentLabel { get; set; }
        public string? Comment { get; set; }
        public string? Name { get; set; }
        public string? JobTitle { get; set; }
        public string? ImageUrl { get; set; }
    }
    public interface ITestimonialService
    {
        IEnumerable<Testimonial> getAll();
    }

    public class TestimonialService : ITestimonialService
    {
        public IEnumerable<Testimonial> getAll()
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseSqlite(config.GetConnectionString("Default"))
                .Options;

            var testimonials = new List<Testimonial>();
            using (var context = new AppDBContext(options))
            {
                context.Database.EnsureCreated();
                context.Database.Migrate();

                foreach (var testimonial in context.Testimonials)
                {
                    testimonials.Add(testimonial);
                }
            }
            return testimonials;
        }
    }
    /// <summary>
    /// /////////////////////////////////////////////////////////////////////////////
    /// </summary>

    public class Feature
    {
        public string? Delay { get; set; }
        public string? Class { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
    public interface IFeatureService
    {
        IEnumerable<Feature> getAll();
    }

    public class FeatureService : IFeatureService
    {
        public IEnumerable<Feature> getAll()
        {
            var streamReader = new StreamReader("features.json");

            string json = streamReader.ReadToEnd();
            return JsonSerializer.Deserialize<Feature[]>(json) ?? new Feature[] { };
        }
    }

    public class Contact
    {
        public int Id { get; set; }
        public String first_name { get; set; }
        public String last_name { get; set; }
        public String email { get; set; }
        public String phone { get; set; }
        public String select_service { get; set; }
        public String select_price { get; set; }
        public String comments { get; set; }

        public Contact(string first_name, string last_name, string email, string phone, string select_service, string select_price, string comments)
        {
            this.first_name = first_name;
            this.last_name = last_name;
            this.email = email;
            this.phone = phone;
            this.select_service = select_service;
            this.select_price = select_price;
            this.comments = comments;
        }
    }

    public interface IContactsService
    {
        void writeToDBContacts(Contact contact);
    }

    public class ContactsService : IContactsService
    {
        void IContactsService.writeToDBContacts(Contact contact)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseSqlite(config.GetConnectionString("Default"))
                .Options;

            using (var context = new AppDBContext(options))
            {
                context.Database.EnsureCreated();
                context.Database.Migrate();

                context.Contacts.Add(contact);
                context.SaveChanges();
            }
        }
    }
}
