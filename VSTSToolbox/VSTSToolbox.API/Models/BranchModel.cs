using System;
using System.Collections.Generic;
using System.Text;

namespace VSTSToolbox.API.Models
{
    public class BranchModel
    {
        public string Name { get; set; }
        public string ObjectId { get; set; }
        public string Url { get; set; }
    }
    public class BranchResponseModel
    {
        public int count { get; set; }
        public List<BranchModel> value { get; set; }
    }
}
