using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using Calvary.Data;
using Calvary.ViewModels.Master.Job;

namespace Calvary.Providers
{
	public class JobProvider : BaseProvider
	{
		public JobProvider(CalvaryContext context, IPrincipal principal)
            : base(context, principal)
        {			
        }
		
		public void AddJob(Job job)
        {
            context.Jobs.Add(job);
            SetAuditFields(job);

            context.SaveChanges();
        }		
		
		public void UpdateJob(Job job)
        {
            SetAuditFields(job);
            context.SaveChanges();
        }	

		public void DeleteJob(int jobId)
        {
            var selectedJob = context.Jobs.SingleOrDefault(job => job.Id == jobId);
            if(selectedJob != null)
            {
                context.Jobs.Remove(selectedJob);
                context.SaveChanges();
            }
        }

		public IQueryable<ListJobViewModel> List()
        {
            var query = from job in context.Jobs
                        select new ListJobViewModel
                        {
                            Id = job.Id,
							Name = job.Name,
                        };
            return query;
        }

        public IEnumerable<Job> GetJobs()
        {
            context.Configuration.ProxyCreationEnabled = false;
            var query = context.Jobs.OrderBy(job => job.Name);
            return query.ToList();
        }

        public Job GetJob(int jobId)
        {
            var selectedJob = context.Jobs.SingleOrDefault(job => job.Id == jobId);
            return selectedJob;
        }
	}
}
			