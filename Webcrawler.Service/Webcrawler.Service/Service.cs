using System.ComponentModel;

namespace Webcrawler.Service
{
    public class Service
    {
        private int schedule;
        private BackgroundWorker worker;

        public Service(int scheduletime)
        {
            schedule = scheduletime;
        }
    }
}