// <auto-generated>
// ReSharper disable RedundantUsingDirective
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable RedundantNameQualifier
// TargetFrameworkVersion = 4.5
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Threading;
using DatabaseGeneratedOption = System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption;

namespace Calvary.Data
{
    public partial class CalvaryContext : DbContext, ICalvaryContext
    {
        public DbSet<BaptizedReason> BaptizedReasons { get; set; } // BaptizedReason
        public DbSet<Configuration> Configurations { get; set; } // Configuration
        public DbSet<EducationGrade> EducationGrades { get; set; } // EducationGrade
        public DbSet<EducationMajor> EducationMajors { get; set; } // EducationMajor
        public DbSet<Ethnic> Ethnics { get; set; } // Ethnic
        public DbSet<Hobby> Hobbies { get; set; } // Hobby
        public DbSet<Income> Incomes { get; set; } // Income
        public DbSet<IncomeType> IncomeTypes { get; set; } // IncomeType
        public DbSet<Job> Jobs { get; set; } // Job
        public DbSet<KlasisMapping> KlasisMappings { get; set; } // KlasisMapping
        public DbSet<LookUp> LookUps { get; set; } // LookUp
        public DbSet<Meeting> Meetings { get; set; } // Meeting
        public DbSet<MeetingType> MeetingTypes { get; set; } // MeetingType
        public DbSet<Member> Members { get; set; } // Member
        public DbSet<MemberMarriage> MemberMarriages { get; set; } // MemberMarriage
        public DbSet<MemberNote> MemberNotes { get; set; } // MemberNote
        public DbSet<MemberState> MemberStates { get; set; } // MemberState
        public DbSet<MemberStateHistory> MemberStateHistories { get; set; } // MemberStateHistory
        public DbSet<MemberVisit> MemberVisits { get; set; } // MemberVisit
        public DbSet<Menu> Menus { get; set; } // Menu
        public DbSet<MutationType> MutationTypes { get; set; } // MutationType
        public DbSet<Region> Regions { get; set; } // Region
        public DbSet<ResignReason> ResignReasons { get; set; } // ResignReason
        public DbSet<Role> Roles { get; set; } // Role
        public DbSet<RoleMenu> RoleMenus { get; set; } // RoleMenu
        public DbSet<Schedule> Schedules { get; set; } // Schedule
        public DbSet<ScheduleGroup> ScheduleGroups { get; set; } // ScheduleGroup
        public DbSet<Shrine> Shrines { get; set; } // Shrine
        public DbSet<Skill> Skills { get; set; } // Skill
        public DbSet<Sysdiagram> Sysdiagrams { get; set; } // sysdiagrams
        public DbSet<UserLogin> UserLogins { get; set; } // UserLogin
        public DbSet<VisitResult> VisitResults { get; set; } // VisitResult
        public DbSet<Worship> Worships { get; set; } // Worship
        public DbSet<WorshipType> WorshipTypes { get; set; } // WorshipType
        
        static CalvaryContext()
        {
            System.Data.Entity.Database.SetInitializer<CalvaryContext>(null);
        }

        public CalvaryContext()
            : base("Name=CalvaryConnection")
        {
            InitializePartial();
        }

        public CalvaryContext(string connectionString) : base(connectionString)
        {
            InitializePartial();
        }

        public CalvaryContext(string connectionString, System.Data.Entity.Infrastructure.DbCompiledModel model) : base(connectionString, model)
        {
            InitializePartial();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new BaptizedReasonMapping());
            modelBuilder.Configurations.Add(new ConfigurationMapping());
            modelBuilder.Configurations.Add(new EducationGradeMapping());
            modelBuilder.Configurations.Add(new EducationMajorMapping());
            modelBuilder.Configurations.Add(new EthnicMapping());
            modelBuilder.Configurations.Add(new HobbyMapping());
            modelBuilder.Configurations.Add(new IncomeMapping());
            modelBuilder.Configurations.Add(new IncomeTypeMapping());
            modelBuilder.Configurations.Add(new JobMapping());
            modelBuilder.Configurations.Add(new KlasisMappingMapping());
            modelBuilder.Configurations.Add(new LookUpMapping());
            modelBuilder.Configurations.Add(new MeetingMapping());
            modelBuilder.Configurations.Add(new MeetingTypeMapping());
            modelBuilder.Configurations.Add(new MemberMapping());
            modelBuilder.Configurations.Add(new MemberMarriageMapping());
            modelBuilder.Configurations.Add(new MemberNoteMapping());
            modelBuilder.Configurations.Add(new MemberStateMapping());
            modelBuilder.Configurations.Add(new MemberStateHistoryMapping());
            modelBuilder.Configurations.Add(new MemberVisitMapping());
            modelBuilder.Configurations.Add(new MenuMapping());
            modelBuilder.Configurations.Add(new MutationTypeMapping());
            modelBuilder.Configurations.Add(new RegionMapping());
            modelBuilder.Configurations.Add(new ResignReasonMapping());
            modelBuilder.Configurations.Add(new RoleMapping());
            modelBuilder.Configurations.Add(new RoleMenuMapping());
            modelBuilder.Configurations.Add(new ScheduleMapping());
            modelBuilder.Configurations.Add(new ScheduleGroupMapping());
            modelBuilder.Configurations.Add(new ShrineMapping());
            modelBuilder.Configurations.Add(new SkillMapping());
            modelBuilder.Configurations.Add(new SysdiagramMapping());
            modelBuilder.Configurations.Add(new UserLoginMapping());
            modelBuilder.Configurations.Add(new VisitResultMapping());
            modelBuilder.Configurations.Add(new WorshipMapping());
            modelBuilder.Configurations.Add(new WorshipTypeMapping());

            OnModelCreatingPartial(modelBuilder);
        }

        public static DbModelBuilder CreateModel(DbModelBuilder modelBuilder, string schema)
        {
            modelBuilder.Configurations.Add(new BaptizedReasonMapping(schema));
            modelBuilder.Configurations.Add(new ConfigurationMapping(schema));
            modelBuilder.Configurations.Add(new EducationGradeMapping(schema));
            modelBuilder.Configurations.Add(new EducationMajorMapping(schema));
            modelBuilder.Configurations.Add(new EthnicMapping(schema));
            modelBuilder.Configurations.Add(new HobbyMapping(schema));
            modelBuilder.Configurations.Add(new IncomeMapping(schema));
            modelBuilder.Configurations.Add(new IncomeTypeMapping(schema));
            modelBuilder.Configurations.Add(new JobMapping(schema));
            modelBuilder.Configurations.Add(new KlasisMappingMapping(schema));
            modelBuilder.Configurations.Add(new LookUpMapping(schema));
            modelBuilder.Configurations.Add(new MeetingMapping(schema));
            modelBuilder.Configurations.Add(new MeetingTypeMapping(schema));
            modelBuilder.Configurations.Add(new MemberMapping(schema));
            modelBuilder.Configurations.Add(new MemberMarriageMapping(schema));
            modelBuilder.Configurations.Add(new MemberNoteMapping(schema));
            modelBuilder.Configurations.Add(new MemberStateMapping(schema));
            modelBuilder.Configurations.Add(new MemberStateHistoryMapping(schema));
            modelBuilder.Configurations.Add(new MemberVisitMapping(schema));
            modelBuilder.Configurations.Add(new MenuMapping(schema));
            modelBuilder.Configurations.Add(new MutationTypeMapping(schema));
            modelBuilder.Configurations.Add(new RegionMapping(schema));
            modelBuilder.Configurations.Add(new ResignReasonMapping(schema));
            modelBuilder.Configurations.Add(new RoleMapping(schema));
            modelBuilder.Configurations.Add(new RoleMenuMapping(schema));
            modelBuilder.Configurations.Add(new ScheduleMapping(schema));
            modelBuilder.Configurations.Add(new ScheduleGroupMapping(schema));
            modelBuilder.Configurations.Add(new ShrineMapping(schema));
            modelBuilder.Configurations.Add(new SkillMapping(schema));
            modelBuilder.Configurations.Add(new SysdiagramMapping(schema));
            modelBuilder.Configurations.Add(new UserLoginMapping(schema));
            modelBuilder.Configurations.Add(new VisitResultMapping(schema));
            modelBuilder.Configurations.Add(new WorshipMapping(schema));
            modelBuilder.Configurations.Add(new WorshipTypeMapping(schema));
            return modelBuilder;
        }

        partial void InitializePartial();
        partial void OnModelCreatingPartial(DbModelBuilder modelBuilder);
    }
}
// </auto-generated>
