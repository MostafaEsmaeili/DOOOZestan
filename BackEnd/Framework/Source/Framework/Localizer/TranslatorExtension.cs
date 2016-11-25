#region usings

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using Framework.ErrorHandler.Exceptions;

#endregion

namespace Framework.Localizer
{
    internal static class TranslatorExtension
    {

        public static void AddDictionary(Dictionary<string, string> dic,
            Culture culture,
            MessageType msgmode = MessageType.General)
        {
            AddDictionary(dic, "", culture, msgmode);
        }

        public static void AddDictionary(Dictionary<string, string> dic,
            string collectionName,
            Culture culture,
            MessageType msgmode = MessageType.General)
        {
            LocalizerCore.AddDictionary(true, dic, collectionName, culture, msgmode);
        }

        public static void AddDictionaryFromResx(Stream stream,
            string collectionName,
            Culture culture,
            MessageType msgmode = MessageType.General)
        {
            LocalizerCore.AddDictionaryFromResX(stream, collectionName, culture, msgmode);
        }

        public static void AddDictionaryFromResx(string filePath, Culture culture, string collectionName = "", MessageType msgmode = MessageType.General)
        {

            FileStream stream = null;

            try
            {
                stream = LocalizerCore.SafeOpenAndLockForRead(IO.PathHelper.Helper.MapPath(filePath));
                AddDictionaryFromResx(stream, collectionName, culture, msgmode);
            }
            finally
            {
                if (stream != null) stream.Close();
            }

        }

        public static void AddDictionaryFromResx(Stream stream,
            Culture culture,
            MessageType msgmode = MessageType.General)
        {
            AddDictionaryFromResx(stream, "", culture, msgmode);
        }

        public static void AddMessages(List<Message> msgs, Culture culture)
        {
            AddMessages(msgs, "", culture);
        }

        public static void AddMessages(List<Message> msgs, string collectionname, Culture culture)
        {
            AddMessages(true, msgs, collectionname, culture);
        }

        public static void AddMessages(bool cacheit, IEnumerable<Message> msgs, string collectionname, Culture culture)
        {
            SortedList<MessageType, SortedList<string, string>> lst =
                new SortedList<MessageType, SortedList<string, string>>();
            foreach (var message in msgs)
            {
                if (!lst.ContainsKey(message.MessageType))
                {
                    lst.Add(message.MessageType, new SortedList<string, string>());
                }
                lst[message.MessageType].Add(message.Key, message.Title + ";" + message.Body);
            }
            foreach (var kp in lst)
            {
                LocalizerCore.AddDictionary(cacheit, kp.Value, collectionname, culture, kp.Key);
            }
        }

        public static string GetBodyByKey(string key,
            Culture culture,
            MessageType msgmode = MessageType.General)
        {
            return GetBodyByKey(key, "", culture, msgmode);
        }

        public static string GetBodyByKey(string key,
            string collectionName = "",
            Culture culture = null,
            MessageType msgmode = MessageType.General)
        {
            return LocalizerCore.GetByKey(key, collectionName, culture, msgmode).Body;
        }

        public static Message GetByKey(
            string key,
            Culture culture = null, string collectionName = "",
            MessageType msgmode = MessageType.General)
        {
            return LocalizerCore.GetByKey(key, collectionName, culture, msgmode);
        }


        public static ConcurrentDictionary<string, Message> GetDictionary(Culture culture,
            MessageType msgmode = MessageType.General)
        {
            return LocalizerCore.GetDictionary("", culture, msgmode);
        }

        public static Message GetMessageByKey(string key,
            Culture culture = null,
            MessageType msgmode = MessageType.General)
        {
            return GetByKey("", culture, "", msgmode);
        }

        public static Message GetMessageByKey(string key,
            string collectionName = "",
            Culture culture = null,
            MessageType msgmode = MessageType.General)
        {
            return LocalizerCore.GetByKey(key, collectionName, culture, msgmode);
        }

        public static Message GetMessageInLanguage(string collectionname, Message msg, Culture destination)
        {
            return GetMessageByKey(msg.Key, collectionname, destination, msg.MessageType);
        }

        public static string Localize(this string key,
            MessageType msgmode,
            string collectionName = "",
            Culture culture = null)
        {
            return Localize(key, collectionName, culture, msgmode);
        }


        public static string Localize(this string key,
            string collectionName = "",
            Culture culture = null,
            MessageType msgmode = MessageType.General)
        {
            return GetBodyByKey(key, collectionName, culture, msgmode);
        }

        public static string Localize(this AppException exception, string collectionName = "", Culture culture = null)
        {
            return GetBodyByKey(exception.ErrorCode.ToString(), collectionName, culture, MessageType.Error);
        }

        public static string GetStringFormat(this string key, params object[] parameters)
        {
            return string.Format(GetBodyByKey(key), parameters);
        }

        public static string GetStringFormat(this string key, string collectionName, Culture culture, params object[] parameters)
        {
            return string.Format(GetBodyByKey(key, collectionName, culture), parameters);
        }

        public static string GetTitleByKey(string key,
            Culture culture = null,
            MessageType msgmode = MessageType.General)
        {
            return GetTitleByKey("", "", culture, msgmode);
        }

        public static string GetTitleByKey(string key,
            string collectionName = "",
            Culture culture = null,
            MessageType msgmode = MessageType.General)
        {
            return LocalizerCore.GetByKey(key, collectionName, culture, msgmode).Title;
        }


        public static bool HasKey(this string key, string collectionname = "",
            Culture culture = null,
            MessageType msgmode = MessageType.General)
        {
            return LocalizerCore.HasKey(key, collectionname, culture, msgmode);
        }
    }
}