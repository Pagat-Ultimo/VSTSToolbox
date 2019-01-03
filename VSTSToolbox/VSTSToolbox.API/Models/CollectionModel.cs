using System;
using System.Collections.Generic;
using System.Text;

namespace VSTSToolbox.API.Models
{
    public class CollectionModel
    {
        public string name { get; set; }
        public string uri { get; set; }
    }

    public class CollectionResponseModel
    {
        public int Count { get; set; }
        public List<CollectionModel> __wrappedArray { get; set; }
    }
}
