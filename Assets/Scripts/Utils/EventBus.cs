using System;

namespace Utils
{
    public static class EventBus
    {
        public static Action PutDeuterium;
        public static Action PutTriterium;

        public static Action<float> MovingWheel;
    }
}
