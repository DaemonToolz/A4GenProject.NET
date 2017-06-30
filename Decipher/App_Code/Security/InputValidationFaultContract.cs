using Data;
using Data.Interop;
using System;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;
using TokenManager;

/// <summary>
/// Summary description for InputValidator
/// </summary>
namespace Security.Validation
{
    public class InputValidationFaultContract{
        public String Error { get; set; }

    }
}