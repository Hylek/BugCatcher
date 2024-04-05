using System;
using System.Collections.Generic;
using System.Linq;
using DC.MessageService;

namespace Utils
{
    public interface IEventBase
    {
        public void Subscribe<T>(Action<T> action) where T : class, ITinyMessage;
        public void Unsubscribe<T>();
    }
    
    public class EventBase : IEventBase, IDisposable
    {
        private readonly ITinyMessengerHub _hub = Locator.EventHub;
        private readonly Dictionary<Type, TinyMessageSubscriptionToken> _tokens = new();

        public void Subscribe<T>(Action<T> action) where T : class, ITinyMessage
        {
            _tokens.Add(typeof(T), _hub.Subscribe(action));
        }

        public void Unsubscribe<T>()
        {
            var type = typeof(T);
            
            foreach (var token in
                     _tokens.Where(token => token.Key == type))
            {
                _hub.Unsubscribe(token.Value);
                _tokens.Remove(token.Key);

                break;
            }
        }

        public void Dispose()
        {
            if (_tokens.Count <= 0) return;
            
            foreach (var token in _tokens)
            {
                _hub.Unsubscribe(token.Value);
            }
            _tokens.Clear();
        }
    }
}