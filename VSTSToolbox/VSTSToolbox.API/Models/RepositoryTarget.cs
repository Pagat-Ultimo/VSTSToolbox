using System;
using System.Collections.Generic;
using System.Text;

namespace VSTSToolbox.API.Models
{
    public class RepositoryTarget
    {
        public string Organization { get; set; }
        public string ProjectId { get; set; }
        public string RepositoryId { get; set; }
    }
}
