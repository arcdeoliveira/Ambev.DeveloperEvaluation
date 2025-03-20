using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Common
{
    public class DomainNotification : INotification
    {
        public DomainNotification(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; }
        public string Value { get; }
    }
}
