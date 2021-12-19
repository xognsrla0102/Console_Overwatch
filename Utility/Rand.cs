using System;

namespace Overwatch
{
    public static class Rand
    {
        public static Random rand;
        static Rand()
        {
            rand = new Random();
        }
    }
}
