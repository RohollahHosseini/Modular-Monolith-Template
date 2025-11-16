using Blog.Modules.Content.Infrastrocture.Configuration.Proccessing.Outbox;
using Quartz;
using Quartz.Impl;
using System.Collections.Specialized;

namespace Blog.Modules.Content.Infrastrocture.Configuration.Proccessing.Quartz
{
    internal class QuartzStartup
    {
        internal static void Initialize(long? internalProcessingPoolingInterval = null)
        {
            var schedulerConfiguration = new NameValueCollection();
            schedulerConfiguration.Add("quartz.scheduler.instanceName", "Blog-Content");

            ISchedulerFactory schedulerFactory = new StdSchedulerFactory(schedulerConfiguration);
            IScheduler scheduler = schedulerFactory.GetScheduler().GetAwaiter().GetResult();

            scheduler.Start().GetAwaiter().GetResult();

            var processOutboxJob = JobBuilder.Create<ProcessOutboxJob>().Build();
            ITrigger trigger;
            if (internalProcessingPoolingInterval.HasValue)
            {
                trigger =
                    TriggerBuilder
                        .Create()
                        .StartNow()
                        .WithSimpleSchedule(x =>
                            x.WithInterval(TimeSpan.FromSeconds(internalProcessingPoolingInterval.Value))
                                .RepeatForever())
                        .Build();
            }
            else
            {
                trigger =
                    TriggerBuilder
                        .Create()
                        .StartNow()
                        .WithCronSchedule("0/2 * * ? * *")
                        .Build();
            }

            scheduler
                .ScheduleJob(processOutboxJob, trigger)
                .GetAwaiter().GetResult();

            //var processInboxJob = JobBuilder.Create<ProcessInboxJob>().Build();

            //ITrigger processInboxTrigger;
            //if (internalProcessingPoolingInterval.HasValue)
            //{
            //    processInboxTrigger =
            //        TriggerBuilder
            //            .Create()
            //            .StartNow()
            //            .WithSimpleSchedule(x =>
            //                x.WithInterval(TimeSpan.FromMilliseconds(internalProcessingPoolingInterval.Value))
            //                    .RepeatForever())
            //            .Build();
            //}
            //else
            //{
            //    processInboxTrigger =
            //        TriggerBuilder
            //            .Create()
            //            .StartNow()
            //            .WithCronSchedule("0/2 * * ? * *")
            //            .Build();
            //}

            //scheduler
            //    .ScheduleJob(processInboxJob, processInboxTrigger)
            //    .GetAwaiter().GetResult();

            //var processInternalCommandsJob = JobBuilder.Create<ProcessInternalCommandsJob>().Build();

            //ITrigger processInternalCommandsTrigger;
            //if (internalProcessingPoolingInterval.HasValue)
            //{
            //    processInternalCommandsTrigger =
            //        TriggerBuilder
            //            .Create()
            //            .StartNow()
            //            .WithSimpleSchedule(x =>
            //                x.WithInterval(TimeSpan.FromMilliseconds(internalProcessingPoolingInterval.Value))
            //                    .RepeatForever())
            //            .Build();
            //}
            //else
            //{
            //    processInternalCommandsTrigger =
            //        TriggerBuilder
            //            .Create()
            //            .StartNow()
            //            .WithCronSchedule("0/2 * * ? * *")
            //    .Build();
            //}

            //scheduler.ScheduleJob(processInternalCommandsJob, processInternalCommandsTrigger).GetAwaiter().GetResult();

        }
    }
}
