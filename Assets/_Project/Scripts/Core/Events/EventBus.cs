using System;

namespace VRTraining.Core.Events
{
    // своя шина событий, без сторонних библиотек
    // на каждый тип события свой список подписчиков
    // системы друг про друга не знают, общаются только через нее
    public static class EventBus<T> where T : struct, IGameEvent
    {
        private static Action<T> listeners;

        public static void Subscribe(Action<T> listener)
        {
            listeners += listener;
        }

        public static void Unsubscribe(Action<T> listener)
        {
            listeners -= listener;
        }

        public static void Raise(T gameEvent)
        {
            listeners?.Invoke(gameEvent);
        }

        // на случай если кто то забыл отписаться
        public static void Clear()
        {
            listeners = null;
        }
    }
}
