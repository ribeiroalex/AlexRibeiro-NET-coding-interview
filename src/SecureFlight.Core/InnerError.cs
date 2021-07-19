using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SecureFlight.Core
{
    public abstract class InnerError
    {
        public string Code { get; set; }

        [Required]
        public abstract string ErrorType { get; }
    }
}
