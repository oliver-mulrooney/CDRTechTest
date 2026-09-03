using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDR.Model.Models.ValidationResults;
public class ValidationResult 
{
    public bool IsValid { get; set; }
    public string? Message { get; set; }
}
