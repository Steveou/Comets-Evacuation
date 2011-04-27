using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CometsEvacuation
{
    // was just an idea (based on Nick Gravelyn's code)
    // but I guess I won't use it

    public class Timers
    {
        private class Timer
        {
            string name;
            float ticks;
            bool repeat;
            Action function;

            public Timer(string name, float ticks, bool repeat, Action function)
            {

            }
        }

        private List<Timer> timers;

        public Timers()
        {
            timers = new List<Timer>();
        }

        public void Create(string name, float ticks, bool repeat, Action function)
        {

        }

        public void Remove(string name)
        {

        }

        public void Update(double elapsedSeconds)
        {

        }
    }
}
