using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
namespace DataAccess.EntityFramework
{
    public class PmtConstUpdated
    {
        [XmlElement("AllowPart")]
        public bool? AllowPart { get; set; }

        [XmlElement("Lower")]
        public string Lower { get; set; }
        [XmlElement("Upper")]
        public string Upper { get; set; }
    }
}
