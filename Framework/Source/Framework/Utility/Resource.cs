using System;
using System.Collections;
using System.IO;
using System.Xml;
using Framework.Globals;

namespace Framework.Utility
{
    public class Resource
    {
        private string _culture = "fa";

        public static string Culture { get; set; }
        #region Enumeration Members

        private enum ResourceManagerType
        {
            String,
            CustomMessage,
            Help
        }

        #endregion

        #region Static Members

        #region Method Members

        #region Public Methods

        #region GetHelpText

        //public static Message GetHelpText(string name)
        //{
        //    return GetHelpText(name, "fa");
        //}

        public static Message GetHelpText(string name)
        {
            name = name.ToLower();
            Hashtable resources = GetResource(ResourceManagerType.Help,Culture);

            Message text = resources[name] as Message;

            if (text == null)
            {
                // EventLogs.Warn(name + " In Lan " + language + " Not Found", "Resource Manager", -200); TODO LOG

            }
            return text;
        }

        #endregion

        #region GetString

        //public static string GetString(string name)
        //{
        //    return GetString(name, "fa");
        //}

        public static string GetString(string name)
        {

            name = name.Trim().ToLower();
            Hashtable resources;
            try
            {
                resources = GetResource(ResourceManagerType.String, Culture);
            }
            catch (Exception e)
            {
                return name;
            }

            string text = resources[name] as string;

            if (text == null && name.StartsWith("no_"))
            {
                return name.Replace("no_", string.Empty);
            }
            if (text == null)
            {
                //EventLogs.Warn(name + " In Lan " + language + " Not Found", "Resource Manager", -200); //TODO LOG
                return string.Format("<strong><FONT color=#ff0000 size=5>Missing Resource: {0}</FONT></strong>", name);
            }
            return text;
        }
      

        #endregion

        public static Message GetMessage(string msgTitle)
        {
            Hashtable resources = GetResource(ResourceManagerType.CustomMessage, Culture);

            if (resources[msgTitle] == null)
            {

                //EventLogs.Warn(MsgTitle + " Not Found", "Resource Manager", -210);TODO LOG
                return null;
            }

            return (Message)resources[msgTitle];
        }

      

        #endregion

        #region Private Methods

        //private static Hashtable GetResource(ResourceManagerType resourceType)
        //{
        //    return GetResource(resourceType, "fa");
        //}

        private static Hashtable GetResource(ResourceManagerType resourceType,string culture)
        {
            var resources = new Hashtable();

            resources = LoadResource(resourceType, resources,culture);
            return resources;
        }

        private static Hashtable LoadResource(ResourceManagerType resourceType, Hashtable target,string culture)
        {

            string filePath = AppPath.CorePath("Resource") + "{0}/{1}";
            switch (resourceType)
            {
                case ResourceManagerType.CustomMessage:
                    filePath = string.Format(filePath, culture, "Messages.xml");
                    break;
                case ResourceManagerType.String:
                    filePath = string.Format(filePath, culture, "Resources.xml");
                    break;
                case ResourceManagerType.Help:
                    filePath = string.Format(filePath, culture, "Help.xml");
                    break;
                default:
                    filePath = string.Format(filePath, culture, "Resources.xml");
                    break;
            }
            if (!System.IO.File.Exists(filePath))
            {
                throw new Exception(string.Format("File Doesn't Exists: {0}", filePath));
            }
            FileInfo fileInfo = new FileInfo(filePath);
            long fileSize = fileInfo.Length;
            long prevSize = CustomCache.Get(string.Format("Core.Resource.Size.{0}", resourceType)).SafeLong(0);
            Hashtable resource = (Hashtable)CustomCache.Get(string.Format("Core.Resource.{0}", resourceType));


            //resource.Count 
            if (resource == null || fileSize != prevSize)
            {

                XmlDocument d = new XmlDocument();
                try
                {
                    d.Load(filePath);
                }
                catch
                {
                    return target;
                }
                var selectSingleNode = d.SelectSingleNode("root");
                if (selectSingleNode != null)
                    foreach (XmlNode n in selectSingleNode.ChildNodes)
                    {
                        if (n.NodeType != XmlNodeType.Comment)
                        {
                            Message m;
                            switch (resourceType)
                            {
                                case ResourceManagerType.CustomMessage:
                                    m = new Message(n);
                                    target[m.Name] = m;
                                    break;

                                case ResourceManagerType.Help:
                                    m = new Message(n);
                                    target[m.Name] = m;
                                    break;

                                case ResourceManagerType.String:
                                    if (target[n.Attributes["name"].Value.ToLower()] == null)
                                        target.Add(n.Attributes["name"].Value.ToLower(), n.InnerText.Replace("&gt;", ">").Replace("&lt;", "<"));
                                    else
                                        target[n.Attributes["name"].Value.ToLower()] = n.InnerText.Replace("&gt;", ">").Replace("&lt;", "<");
                                    break;
                                    //m = new Message(n);
                                    //target[m.Name] = m;

                                    //break;
                            }
                        }
                    }

                CustomCache.Insert(string.Format("Core.Resource.{0}", resourceType.ToString()), target);
                CustomCache.Insert(string.Format("Core.Resource.Size.{0}", resourceType.ToString()), fileSize);

                return target;
            }
            return resource;


        }

        #endregion

        #endregion

        #endregion

    }
}
