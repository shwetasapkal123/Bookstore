using System;
using System.Collections.Generic;
using System.Text;

namespace Database_Layer
{
    public class AddressModel
    {
        public string FullAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int TypeId { get; set; }
        public int UserId { get; set; }
    }
}
