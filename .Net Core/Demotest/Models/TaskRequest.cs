using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demotest.Models
{
    public class TaskRequest
    {
        public int taskId { get; set; }

        public string taskName { get; set; }

        public string status { get; set; }

        public int userId { get; set; }
    }
}
