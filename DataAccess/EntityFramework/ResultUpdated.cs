using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
namespace DataAccess.EntityFramework
{
    public class ResultUpdated
    {
        public ResultUpdated() { }
       
        [XmlElement]
        public int ErrorCode { get; set; }
        [XmlElement]
        public string ErrorDesc { get; set; }

        [XmlElement]
        public SeverityUpdated Severity { get; set; }
    }
}
