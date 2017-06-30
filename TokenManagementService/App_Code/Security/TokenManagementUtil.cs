using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TokenManagementUtil
/// </summary>
/// 
namespace TokenManagementService.Security{
    public enum TokenManagementUtil{
        _LOW_ID_SIZE = 8,
        _MEDIUM_ID_SIZE = 16,
        _LARGE_ID_SIZE = 32,
        _HUGE_ID_SIZE = 64
    }
}