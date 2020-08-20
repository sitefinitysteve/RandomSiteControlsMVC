using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.Scheduling;
using Telerik.Sitefinity.Scheduling.Model;
using Telerik.Sitefinity;

namespace RandomSiteControlsMVC.MVC.Models.ScheduledTaskStatus
{
    public class ScheduledTaskStatusModel
    {
        public ScheduledTaskStatusModel()
        {
            SchedulingManager schedulingManager = SchedulingManager.GetManager();

            var now = DateTime.Now;
            this.AllTasks = schedulingManager.GetTaskData();
            this.FutureTasks = this.AllTasks.Where(x => x.ExecuteTime >= now);
            this.FailedTasks = this.AllTasks.Where(x => x.ExecuteTime < now);
            this.CronJobs = this.AllTasks.Where(x => x.TypeOfSchedule == "crontab");
        }

        public IQueryable<ScheduledTaskData> AllTasks { get; set; }
        public IQueryable<ScheduledTaskData> FutureTasks { get; set; }
        public IQueryable<ScheduledTaskData> FailedTasks { get; set; }
        public IQueryable<ScheduledTaskData> CronJobs { get; set; }

    }
}

public static class TimeSpanExtensionMethods
{
    public static string ToReadableString(this TimeSpan span)
    {
        string formatted = string.Format("{0}{1}{2}",
            (span.Days / 7) > 0 ? string.Format("{0:0} weeks, ", span.Days / 7) : string.Empty,
            span.Days % 7 > 0 ? string.Format("{0:0} days, ", span.Days % 7) : string.Empty,
            span.Hours > 0 ? string.Format("{0:0} hours, ", span.Hours) : string.Empty);

        if (formatted.EndsWith(", ")) formatted = formatted.Substring(0, formatted.Length - 2);

        return formatted;
    }
}