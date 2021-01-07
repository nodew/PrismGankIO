using System;
using System.Collections.Generic;
using System.Text;

namespace PrismGankIO.Core.Models
{
    public class HttpResult<T>
    {
        public T Data { get; set; }

        public int Status { get; set; }
    }
}
