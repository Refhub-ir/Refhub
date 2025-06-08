using Microsoft.Extensions.Localization;
using Refhub.Resources;
using Refhub.Service.Interface;
using System.Reflection;

namespace Refhub.Service.Implement
{
    public class MessageService:IMessageService
    {
        private readonly IStringLocalizer _localizer;

        public MessageService(IStringLocalizerFactory factory)
        {
            _localizer = factory.Create(nameof(Messages), Assembly.GetExecutingAssembly().GetName().Name);
        }

        public string Get(string key)
        {
            var value = _localizer[key];
            return value.ResourceNotFound ? $"[{key}]" : value.Value;
        }
    }
}
