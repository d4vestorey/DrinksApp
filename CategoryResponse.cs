using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinksApp
{
    public class CategoryResponse
    {
        public List<Repository> drinks { get; set; } = new();
    }
}