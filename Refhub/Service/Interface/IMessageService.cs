namespace Refhub.Service.Interface
{
    public interface IMessageService
    {
        string Get(string key);
        string Get(string key, params object[] parameters);
    }
}
