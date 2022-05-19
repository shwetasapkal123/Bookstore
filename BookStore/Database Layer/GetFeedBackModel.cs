using System;
using System.Collections.Generic;
using System.Text;

namespace Database_Layer
{
    public class GetFeedBackModel
    {
        public int FeedbackId { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public int BookId { get; set; }
    }
}
