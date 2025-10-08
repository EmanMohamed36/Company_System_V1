using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Models.Shared
{
    public class BaseEntity
    {

        public int Id { get; set; }
        public int CreatedBy { get; set; } //user Id
        public DateTime CreatedOn { get; set; }
        public int LastModifiedBy { get; set; } //user ID 
        public DateTime LastModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
