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
        public DbSet<Project> Projects { get; set; }
        public DbSet<JoinRequest> JoinRequests { get; set; }
        public DbSet<InvitationRequest> InvitationRequests { get; set; }
        public DbSet<ProjectMembership> ProjectMemberships { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           

            //         < --Project-- >

            //   Project -> JoinRequest
            modelBuilder.Entity<JoinRequest>()
                .HasOne(r => r.Project)
                .WithMany(p => p.JoinRequests)
                .HasForeignKey(r => r.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);

            //   Project -> ProjectMembership
            modelBuilder.Entity<ProjectMembership>()
                .HasOne(r => r.Project)
                .WithMany(p => p.ProjectMemberships)
                .HasForeignKey(r => r.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);


            //   Project -> invitationRequest
            modelBuilder.Entity<InvitationRequest>()
                .HasOne(r => r.TargetProject)
                .WithMany(p => p.SentInvitations)
                .HasForeignKey(r => r.TargetProjectId)
                .OnDelete(DeleteBehavior.Restrict);


            //         < --Researcher-- >

            //  ProjectMember -> JoinRequest
            modelBuilder.Entity<JoinRequest>()
                .HasOne(j => j.Requester)
                .WithMany(r => r.SentJoinRequests)
                .HasForeignKey(j => j.RequesterId)
                .OnDelete(DeleteBehavior.Restrict);

            //  ProjectMember -> InvitationRequest
            modelBuilder.Entity<InvitationRequest>()
                .HasOne(j => j.Receiver)
                .WithMany(r => r.ReceivedInvitations)
                .HasForeignKey(j => j.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            //  ProjectMember -> ProjectMembership
            modelBuilder.Entity<ProjectMembership>()
                 .HasOne(pm => pm.Researcher)
                .WithMany(m => m.ProjectMemberships)
                .HasForeignKey(pm => pm.ResearcherId);

            //  ApplicationUser 1 <-> 1 Researcher
            modelBuilder.Entity<ApplicationUser>()
                .HasOne(a => a.Researcher)
                .WithOne(r => r.User)
                .HasForeignKey<Researcher>(r => r.Id)
                .OnDelete(DeleteBehavior.Cascade);

            //         < --Notification-- >
            modelBuilder.Entity<Notification>()
                 .HasOne(n => n.User)
                 .WithMany(u => u.Notifications)
                 .HasForeignKey(n => n.UserId)
                 .OnDelete(DeleteBehavior.Cascade);
                  base.OnModelCreating(modelBuilder);



            //         < --Admin-- >

            //  Admin -> Researcher

            modelBuilder.Entity<Researcher>()
                .HasOne(r => r.ApprovedByAdmin)
                .WithMany(p => p.ApprovedResearchers)
                .HasForeignKey(r => r.ApprovedByAdminId)
                .OnDelete(DeleteBehavior.Restrict);

            //  Admin -> Project

            modelBuilder.Entity<Project>()
                .HasOne(p => p.ApprovedByAdmin)
                .WithMany(a => a.ApprovedProjects)
                .HasForeignKey(p => p.ApprovedByAdminId)
                .OnDelete(DeleteBehavior.Restrict);

            //  ApplicationUser 1 <-> 1 Admin
            modelBuilder.Entity<ApplicationUser>()
                .HasOne(a => a.Admin)
                .WithOne(r => r.User)
                .HasForeignKey<Admin>(r => r.Id)
                .OnDelete(DeleteBehavior.Cascade);
           

            //----------------
            // Key ProjectMembership
            modelBuilder.Entity<ProjectMembership>()
                .HasKey(pm => new { pm.ResearcherId, pm.ProjectId });

        }

    }
}
