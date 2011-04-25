using System;

namespace CometsEvacuation
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (CometsGame game = new CometsGame())
            {
                game.Run();
            }
        }
    }
#endif
}

