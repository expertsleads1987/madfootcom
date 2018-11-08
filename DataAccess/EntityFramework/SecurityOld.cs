using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
namespace DataAccess.EntityFramework
{
    public class SecurityOld
    {
        [XmlElement("Signature")]
        public string Signature { get; set; }
        [XmlIgnore]
        public bool SignatureSpecified { get; }
        [XmlElement("ChkSum")]
        public string CheckSum { get; set; }
        [XmlIgnore]
        public bool CheckSumSpecified { get; }
    }
}