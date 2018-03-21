using System;

namespace Fisher.Bookstore.Api.Models
{

    public class Book
    {

        public int Id {get; set;}

        public String Title {get; set;}

        public String Author {get; set;}

        public String ISBN {get; set;}

        public DateTime PublishDate {get; set;}

        public String Publisher {get; set;}
    }
}