using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Demotest.Data
{
    public class Users
    {
        [Key]
        public int userid { get; set; }

        public string username { get; set; }

        public string password { get; set; }

        public DateTime createDate { get; set; }

        public string createBy { get; set; }

        public DateTime updateDate { get; set; }

        public string updateBy { get; set; }

    }
}
