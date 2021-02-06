using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using Calvary.Data;
using Calvary.ViewModels.Master.Skill;

namespace Calvary.Providers
{
	public class SkillProvider : BaseProvider
	{
		public SkillProvider(CalvaryContext context, IPrincipal principal)
            : base(context, principal)
        {			
        }
		
		public void AddSkill(Skill skill)
        {
            context.Skills.Add(skill);
            SetAuditFields(skill);

            context.SaveChanges();
        }		
		
		public void UpdateSkill(Skill skill)
        {
            SetAuditFields(skill);
            context.SaveChanges();
        }	

		public void DeleteSkill(int skillId)
        {
            var selectedSkill = context.Skills.SingleOrDefault(skill => skill.Id == skillId);
            if(selectedSkill != null)
            {
                context.Skills.Remove(selectedSkill);
                context.SaveChanges();
            }
        }

		public IQueryable<ListSkillViewModel> List()
        {
            var query = from skill in context.Skills
                        select new ListSkillViewModel
                        {
                            Id = skill.Id,
							Name = skill.Name
                        };
            return query;
        }

        public IEnumerable<Skill> GetSkills()
        {
            context.Configuration.ProxyCreationEnabled = false;
            var query = context.Skills;
            return query.ToList();
        }

        public Skill GetSkill(int skillId)
        {
            var selectedSkill = context.Skills.SingleOrDefault(skill => skill.Id == skillId);
            return selectedSkill;
        }
	}
}
			