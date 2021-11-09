using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SecureFlight.Api.Models;

namespace SecureFlight.Api.Utils
{
    public class ErrorResponseActionResult : ActionResult
    {
        public ErrorResponse Result { get; set; }
    }
}