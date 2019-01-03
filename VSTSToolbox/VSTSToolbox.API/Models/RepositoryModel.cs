using System;
using System.Collections.Generic;
using System.Text;

namespace VSTSToolbox.API.Models
{
    public class RepositoryModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class RepositoryResponseModel
    {
        public int count { get; set; }
        public List<RepositoryModel> value { get; set; }
    }
}
