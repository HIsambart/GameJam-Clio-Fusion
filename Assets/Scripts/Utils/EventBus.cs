using System;

namespace Utils
{
    public static class EventBus
    {
        public static Action PutDeuterium;
        public static Action PutTriterium;

        public static Action<float> MovingWheel;
        
        // Mini-Game
        public static Action OnTemperatureWin;
        public static Action OnPressureWin;
        public static Action OnCoockingWin;
        public static Action OnWheelWin;
    }
}
