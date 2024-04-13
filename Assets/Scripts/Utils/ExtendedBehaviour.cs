using System;
using System.Collections.Generic;
using System.Linq;
using DC.MessageService;
using UnityEngine;

namespace Utils
{
    public class ExtendedBehaviour : MonoBehaviour, IEventBase
    {
        private Dictionary<Type, TinyMessageSubscriptionToken> _tokens;

        protected virtual void Awake() => _tokens = new Dictionary<Type, TinyMessageSubscriptionToken>();

        public void Subscribe<T>(Action<T> action) where T : class, ITinyMessage
        {
            if (!BaseLocator.DoesServiceExist(typeof(ITinyMessengerHub)))
            {
                BaseLocator.Add<ITinyMessengerHub>(new TinyMessengerHub());
            }
            
            _tokens.Add(typeof(T), Locator.EventHub.Subscribe(action));
        }
        
        public void Unsubscribe<T>()
        {
            var type = typeof(T);
            
            foreach (var token in
                     _tokens.Where(token => token.Key == type))
            {
                Debug.Log($"{gameObject.name} has unsubscribed from message type {token.Key.Name}");
                Locator.EventHub.Unsubscribe(token.Value);
                _tokens.Remove(token.Key);

                break;
            }
        }

        protected virtual void OnDestroy()
        {
            if (_tokens.Count <= 0) return;
            
            Debug.Log($"{gameObject.name} is cleaning up it's messages.");
            
            foreach (var token in _tokens)
            {
                Locator.EventHub.Unsubscribe(token.Value);
            }
            _tokens.Clear();
        }
    }
}