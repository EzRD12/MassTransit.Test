using Quartz;
using System;
using System.Threading.Tasks;
using Core.Models;
using Sender.Workers;

namespace Sender.Jobs
{
    internal class SendMessageJob: IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Worker._bus.Publish(new Message { Text = $"Greetings from Dominican Republic at {DateTimeOffset.Now}" });
        }
    }
}
