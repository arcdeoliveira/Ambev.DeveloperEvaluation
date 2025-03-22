using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Notifications
{
    public class DomainNotification : INotification
    {
        public DomainNotification() { } 
        public DomainNotification(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; } = string.Empty;
        public string Value { get; } = string.Empty;


        public static INotification Create(string key, string value) => new DomainNotification(key, value); 
    }
}
