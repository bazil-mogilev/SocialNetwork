using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.UI;

namespace SocialNetwork.Components
{
    public class SerializationHelper
    {
        public static string Serialize(object obj)
        {
            StringWriter writer = new StringWriter();
            (new LosFormatter()).Serialize(writer, obj);
            return writer.ToString();
        }

        public static object Deserialize(string data)
        {
            if (String.IsNullOrEmpty(data))
                return null;

            return (new LosFormatter()).Deserialize(data);
        }
    }
}