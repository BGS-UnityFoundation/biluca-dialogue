namespace BilucaDialogue.Helpers
{
    public class EventTest
    {
        public static EventTest Create(object sender, string eventName)
        {
            return new EventTest(sender, eventName);
        }

        public bool WasTriggered { get; private set; }
        public int TriggerCount { get; private set; }

        public EventTest(object sender, string eventName)
        {
            WasTriggered = false;
            TriggerCount = 0;
            SubscribeCallback(sender, eventName);
        }

        protected virtual void SubscribeCallback(object sender, string eventName)
        {
            SubscribeToEvent.Subscribe(sender, eventName, this, nameof(HandleTrigger));
        }

        protected void HandleTrigger()
        {
            WasTriggered = true;
            TriggerCount++;
        }
    }

    public class EventTest<T> : EventTest
    {
        public EventTest(object sender, string eventName) : base(sender, eventName)
        {
        }

        public T Parameter { get; private set; }
        public new static EventTest<T> Create(object sender, string eventName)
        {
            return new EventTest<T>(sender, eventName);
        }

        protected override void SubscribeCallback(object sender, string eventName)
        {
            SubscribeToEvent.Subscribe(sender, eventName, this, nameof(HandleTrigger));
        }

        protected void HandleTrigger(T obj)
        {
            Parameter = obj;
            HandleTrigger();
        }
    }

    public static class SubscribeToEvent
    {
        public static void Subscribe(
            object sender, string eventName, object target, string callback
        )
        {
            var eventInfo = sender.GetType().GetEvent(eventName);
            var handler = Delegate.CreateDelegate(eventInfo.EventHandlerType, target, callback);
            eventInfo.AddEventHandler(sender, handler);
        }
    }

}