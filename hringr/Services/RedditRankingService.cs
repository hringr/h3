using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hringr.Services
{
    public class RedditRankingService
    {
        // Returns the number of seconds from the epoch to date.
        private double EpochSeconds(DateTime date)
        {
            var epoch = new DateTime(1970, 1, 1);
            var td = date - epoch;
            Console.WriteLine(td.Milliseconds);
            return (td.Days * 86400 + td.Seconds + (td.Milliseconds / 1000));
        }

        private int Score(int ups, int downs)
        {
            return ups - downs;
        }

        // The hot formula. Should match the equivalent function in postgres.
        public double Hot(int ups, int downs, DateTime date)
        {
            int score = Score(ups, downs);
            double order = Math.Log(Math.Abs(score), 10);
            int sign = score > 0 ? 1 : score < 0 ? -1 : 0;
            double seconds = EpochSeconds(date) - 1134028003;
            return Math.Round(sign * order + seconds / 45000, 7);
        }
    }
}