using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BillerIntegration
{
    [ServiceContract]
    public interface IBillerPrepaid
    {
        [OperationContract]
        XElement PrepaidValidation(string guid, XElement billerPrepaidValidationRequest, string username = null, string password = null);
    }
}
