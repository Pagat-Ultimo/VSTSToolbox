using System;
using System.Collections.Generic;
using System.Text;

namespace VSTSToolbox.API.Models
{
    public class ProjectModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string state { get; set; }
    }
    public class ProjectResponseModel
    {
        public int count { get; set; }
        public List<ProjectModel> value { get; set; }
    }
}
