using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Core.Xml;
using KMLib.Abstract;
using System.IO;

namespace KMLib
{
    public class KmlDocument
    {
        private KMLRoot m_KML;
        //[XmlArray(ElementName = "kml", Namespace="http://earth.google.com/kml/2.2")]        
        public KMLRoot KML {
            get {
                return m_KML;
            }
            set {
                m_KML = value;
            }
        }

        private static bool associationsInitialized = false;
        private static void InitAssociationsAN() {
            if (associationsInitialized) return;
            XmlTypeAssociator<AObject>.AddType(typeof(Placemark));
            XmlTypeAssociator<AObject>.AddType(typeof(Region));
            XmlTypeAssociator<AObject>.AddType(typeof(GroundOverlay));
            XmlTypeAssociator<AObject>.AddType(typeof(NetworkLink));
            associationsInitialized = true;
        }

        public void Save(string fpath) {
            InitAssociationsAN();
            XmlSerializer<KMLRoot>.SerializeObjectToFile(m_KML, fpath);
            //XmlSerializer ser = new XmlSerializer(typeof(KMLRoot), "http://earth.google.com/kml/2.1");
            //FileStream fs = new FileStream(fpath, FileMode.Create);
            //ser.Serialize(fs, m_KML);
            //fs.Close();
        }

        public static KmlDocument Load(string fpath) {
            InitAssociationsAN();
            KmlDocument ans = new KmlDocument();
            ans.KML = XmlSerializer<KMLRoot>.DeserializeObjectFromFile(fpath);
            return ans;
        }
    }

    //[XmlRoot(ElementName = "kml", Namespace = "http://earth.google.com/kml/2.1")]
    //--if you add the namespace, then all children get a blank namespace attrib...
    [XmlRoot(ElementName = "kml")]
    public class KMLRoot : XmlList<AObject>
    {
        private XmlList<AObject> m_Document;
        
        public KMLRoot()
        {
            m_Document = new XmlList<AObject>();
        }

        public XmlList<AObject> Document
        {
            get
            {
                return m_Document;
            }
            set
            {
                m_Document = value;
            }
        }

        
    }
}
