using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ProjetAtrst.Models;

namespace ProjetAtrst.Date
{
    public class ApplicationDbContext :IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Researcher> Researchers { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<Associate> Associates { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectRequest> ProjectRequests { get; set; }
        public DbSet<ProjectMembership> ProjectMemberships { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }
        public DbSet<ProjectTaskAssignment> ProjectTaskAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            //         < --Project-- >
            //   Project -> ProjectRequests
            modelBuilder.Entity<ProjectRequest>()
                .HasOne(r => r.Project)
                .WithMany(p => p.ProjectRequests)
                .HasForeignKey(r => r.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);

            //   Project -> ProjectMembership
            modelBuilder.Entity<ProjectMembership>()
                .HasOne(r => r.Project)
                .WithMany(p => p.ProjectMemberships)
                .HasForeignKey(r => r.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);

            //   Project -> ProjectTask
            modelBuilder.Entity<ProjectTask>()
                .HasOne(r => r.Project)
                .WithMany(p => p.Tasks)
                .HasForeignKey(r => r.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);



            //         < --User-- >
            // User -> SentRequests
            modelBuilder.Entity<ProjectRequest>()
                .HasOne(r => r.Sender)
                .WithMany(u => u.SentRequests)
                .HasForeignKey(r => r.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            // User -> ReceivedRequests
            modelBuilder.Entity<ProjectRequest>()
                .HasOne(r => r.Receiver)
                .WithMany(u => u.ReceivedRequests)
                .HasForeignKey(r => r.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            //  User -> ProjectMembership
            modelBuilder.Entity<ProjectMembership>()
                .HasOne(pm => pm.User)
                .WithMany(pm => pm.ProjectMemberships) 
                .HasForeignKey(pm => pm.UserId);

            //  User -> ProjectTaskAssignment
            modelBuilder.Entity<ProjectTaskAssignment>()
                .HasOne(pm => pm.AssignedUser)
                .WithMany(pm => pm.TaskAssignments) 
                .HasForeignKey(pm => pm.AssignedUserId);


            //         < --Relationships-- >
            //  ApplicationUser 1 <-> 1 Researcher
            modelBuilder.Entity<ApplicationUser>()
                .HasOne(a => a.Researcher)
                .WithOne(r => r.User)
                .HasForeignKey<Researcher>(r => r.Id)
                .OnDelete(DeleteBehavior.Cascade);

            //  ApplicationUser 1 <-> 1 Partenaire
            modelBuilder.Entity<ApplicationUser>()
                .HasOne(a => a.Partner)
                .WithOne(p => p.User)
                .HasForeignKey<Partner>(p => p.Id)
                .OnDelete(DeleteBehavior.Cascade);

            //  ApplicationUser 1 <-> 1 Associate
            modelBuilder.Entity<ApplicationUser>()
                .HasOne(a => a.Associate)
                .WithOne(p => p.User)
                .HasForeignKey<Associate>(p => p.Id)
                .OnDelete(DeleteBehavior.Cascade);

            ////  ApplicationUser 1 <-> 1 admin
            //modelBuilder.Entity<ApplicationUser>()
            //    .HasOne(a => a.Admin)
            //    .WithOne(p => p.User)
            //    .HasForeignKey<Admin>(p => p.Id)
            //    .OnDelete(DeleteBehavior.Cascade);


            //         < --Notification-- >
            modelBuilder.Entity<Notification>()
                 .HasOne(n => n.User)
                 .WithMany(u => u.Notifications)
                 .HasForeignKey(n => n.UserId)
                 .OnDelete(DeleteBehavior.Cascade);
                  base.OnModelCreating(modelBuilder);


            //         < --Admin-- >
            ////  Admin -> User
            //modelBuilder.Entity<ApplicationUser>()
            //    .HasOne(r => r.ApprovedByAdmin)
            //    .WithMany(p => p.ApprovedUsers)
            //    .HasForeignKey(r => r.ApprovedByAdminId)
            //    .OnDelete(DeleteBehavior.Restrict)
            //    .IsRequired(false);

            ////  Admin -> Project
            //modelBuilder.Entity<Project>()
            //    .HasOne(p => p.ApprovedByAdmin)
            //    .WithMany(a => a.ApprovedProjects)
            //    .HasForeignKey(p => p.ApprovedByAdminId)
            //    .OnDelete(DeleteBehavior.Restrict);

           

            // Key ProjectMembership
            modelBuilder.Entity<ProjectMembership>()
                .HasKey(pm => new { pm.UserId, pm.ProjectId });


            //  Admin -> Project
            modelBuilder.Entity<ProjectTaskAssignment>()
                .HasOne(p => p.Task)
                .WithMany(a => a.Assignments)
                .HasForeignKey(p => p.TaskId)
                .OnDelete(DeleteBehavior.Restrict);

        }

    }
}
