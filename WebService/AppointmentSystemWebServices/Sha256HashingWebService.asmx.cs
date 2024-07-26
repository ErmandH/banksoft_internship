using AppointmentLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace AppointmentSystemWebServices
{
    /// <summary>
    /// Summary description for Sha256HashingWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Sha256HashingWebService : System.Web.Services.WebService
    {

        [WebMethod]
        public string GenerateHash(string input)
        {
            return Sha256HashGenerator.Generate(input);
        }
    }
}
