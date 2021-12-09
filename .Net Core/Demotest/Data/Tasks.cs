using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Demotest.Data
{
    public class Tasks
    {
        [Key]
        public int taskId { get; set; }

        public string taskName { get; set; }

        public string status { get; set; }

        public int userId { get; set; }

        public DateTime createDate { get; set; }

        public string createBy { get; set; }

        public DateTime updateDate  { get; set; }

        public string updateBy { get; set; }
    }

}
