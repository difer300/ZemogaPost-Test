using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zemoga.Models
{
    public class BoolResponse
    {
        public bool Value { get; set; }

        public BoolResponse(bool value)
        {
            this.Value = value;
        }

        public BoolResponse()
        {
            Value = false;
        }
        
    }
}
