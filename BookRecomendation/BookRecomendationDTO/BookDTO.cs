using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRecomendationDTO
{
    public class BookDTO
    {
        public int BookIsbn { get; set; }
        public int BookRating { get; set; }
        public string BookReview { get; set; }
    }
}