using ActivityApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityApp
{
    public interface IActivity
    {
        DateTime Start { get; }
        DateTime Stop { get; }
    }

    public class Activity : IActivity
    {
        private readonly DateTime _Start;
        private readonly DateTime _Stop;
        protected Activity(DateTime start, DateTime stop)
        {
            _Start = start;
            _Stop = stop;
        }
        public DateTime Start => _Start;
        public DateTime Stop => _Stop;
    }


    public class Studying : Activity
    {
        public Studying(DateTime start, DateTime stop) : base(start, stop)
        {
        }
    }

    public class Sleeping : Activity
    {
        public Sleeping(DateTime start, DateTime stop) : base(start, stop)
        {
        }
    }

    public class Electronics : Activity
    {
        public Electronics(DateTime start, DateTime stop) : base(start, stop)
        {
        }
    }

    public class Socializing : Activity
    {
        public Socializing(DateTime start, DateTime stop) : base(start, stop)
        {
        }
    }

    public interface IActivityManager
    {
        void Average();
    }

    public class ActivityManager : IActivityManager
    {
        public List<IActivity> activityList = new List<IActivity>();
        public void Average()
        {
            activityList.ForEach(i => i.GetType());
        }
    }


    public static class ListEx
    {
        public static List<T> Get<T>(this System.Collections.Generic.List<IActivity> list)
        {
            return list.OfType<T>().ToList();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ActivityManager manager = new ActivityManager();
            manager.activityList.Add(new Studying(DateTime.Now, DateTime.Now.AddMinutes(20)));
            manager.activityList.Add(new Studying(DateTime.Now.AddMinutes(5), DateTime.Now.AddMinutes(20)));
            manager.activityList.Add(new Studying(DateTime.Now.AddMinutes(100), DateTime.Now.AddMinutes(100)));
            manager.activityList.Add(new Sleeping(DateTime.Now, DateTime.Now.AddMinutes(300)));
            manager.activityList.Add(new Sleeping(DateTime.Now.AddMinutes(5), DateTime.Now.AddMinutes(20)));
            manager.activityList.Add(new Sleeping(DateTime.Now.AddMinutes(100), DateTime.Now.AddMinutes(100)));
            manager.activityList.Add(new Socializing(DateTime.Now.AddMinutes(200), DateTime.Now.AddMinutes(220)));
            manager.activityList.Add(new Socializing(DateTime.Now.AddMinutes(55), DateTime.Now.AddMinutes(65)));
            manager.activityList.Add(new Socializing(DateTime.Now.AddMinutes(100), DateTime.Now.AddMinutes(300)));
            manager.activityList.Add(new Electronics(DateTime.Now.AddMinutes(200), DateTime.Now.AddMinutes(220)));
            manager.activityList.Add(new Electronics(DateTime.Now.AddMinutes(55), DateTime.Now.AddMinutes(65)));
            manager.activityList.Add(new Electronics(DateTime.Now.AddMinutes(100), DateTime.Now.AddMinutes(300)));

            var activites =
                manager
                .activityList
                .Select(i => new
                {
                    Type = i.GetType(),
                    Offset = i.Stop.Subtract(i.Start).TotalMinutes
                })
                .GroupBy(j => j.Type, k => k.Offset)
                .Select(m => new
                {
                    Type = m.Key,
                    Average = m.Average()
                });

            foreach (var activity in activites)
            {
                Console.WriteLine($" {activity.Type.Name} : Average : {activity.Average} minutes");
            }


            Console.ReadLine();
        }
    }
}
