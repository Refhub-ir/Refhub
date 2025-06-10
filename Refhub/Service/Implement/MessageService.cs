using System.Reflection;
using Microsoft.Extensions.Localization;
using Refhub.Resources;
using Refhub.Service.Interface;

namespace Refhub.Service.Implement;

public class MessageService : IMessageService
{
    private readonly IStringLocalizer _localizer;

    public MessageService(IStringLocalizerFactory factory)
    {
        var assemblyName = Assembly.GetExecutingAssembly().GetName().Name ?? "Refhub";
        _localizer = factory.Create(nameof(Messages), assemblyName);
    }

    public string Get(string key)
    {
        var value = _localizer[key];
        return value.ResourceNotFound ? $"[{key}]" : value.Value;
    }

    public string Get(string key, params object[] parameters)
    {
        var value = _localizer[key, parameters];
        return value.ResourceNotFound ? $"[{key}]" : value.Value;
    }
}
