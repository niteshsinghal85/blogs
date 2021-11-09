using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenericRepositoryPattern.Entities
{
    public class Customer :  BaseEntity
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
    }
}
