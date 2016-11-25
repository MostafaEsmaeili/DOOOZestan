namespace Framework.Localizer.Interfaces
{
    public interface ITranslatorResolver
    {
        string Localize(string key,
            Culture culture = null,
            MessageType msgmode = MessageType.General);

        bool HasKey(string key,
            Culture culture = null,
            MessageType msgmode = MessageType.General);
    }

    public interface ITranslatorRegistrar
    {
        void AddDictionaryFromResx(string filePath,
            Culture culture,
            MessageType msgmode = MessageType.General);

    }
    public interface ITranslator : ITranslatorRegistrar, ITranslatorResolver
    {

    }
}
