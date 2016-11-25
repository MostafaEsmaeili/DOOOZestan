using System;
using Framework.Localizer.Interfaces;

namespace Framework.Localizer
{
    public class Translator : ITranslator
    {
        protected readonly string CollectionName;
        public Translator(string collectionname = "")
        {
            CollectionName = collectionname;
            if (string.IsNullOrWhiteSpace(collectionname))
            {
                CollectionName = Guid.NewGuid().ToString("d");
            }
        }
        public string Localize(string key, Culture culture = null, MessageType msgmode = MessageType.General)
        {
            return TranslatorExtension.Localize(key, CollectionName, culture, msgmode);
        }

        public bool HasKey(string key, Culture culture = null, MessageType msgmode = MessageType.General)
        {
            return TranslatorExtension.HasKey(key, CollectionName, culture, msgmode);
        }

        public void AddDictionaryFromResx(string filePath, Culture culture, MessageType msgmode = MessageType.General)
        {
            TranslatorExtension.AddDictionaryFromResx(filePath, culture, CollectionName, msgmode);
        }
    }
}