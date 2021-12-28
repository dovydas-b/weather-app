using System;

namespace MetaApp.Infrastructure
{
    public static class Utils
    {
        public static void CreateRecurringAction(
            Action action,
            double interval,
            bool immediate = true)
        {
            if (immediate) action();

            var timer = new System.Timers.Timer
            {
                AutoReset = true,
                Interval = interval,
            };

            timer.Elapsed += (_, _) => action();
            timer.Start();
        }
    }
}