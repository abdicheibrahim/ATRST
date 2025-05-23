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
        public DbSet<Expert> Experts { get; set; }
        public DbSet<ProjectLeader> ProjectLeaders { get; set; }
        public DbSet<ProjectMember> ProjectMembers { get; set; }
        public DbSet<Researcher> Researchers { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<JoinRequest> JoinRequests { get; set; }
        public DbSet<ProjectFile> ProjectFiles { get; set; }
        public DbSet<ProjectEvaluation> ProjectEvaluations { get; set; }
        public DbSet<InvitationRequest> InvitationRequests { get; set; }
       


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // TPT Configuration
            modelBuilder.Entity<Admin>().ToTable("Admins");
            modelBuilder.Entity<Expert>().ToTable("Experts");
            modelBuilder.Entity<ProjectLeader>().ToTable("ProjectLeaders");
            modelBuilder.Entity<ProjectMember>().ToTable("ProjectMembers");
            modelBuilder.Entity<Researcher>().ToTable("Researchers");

            //         <--ProjectLeader-->

            //  ProjectLeader => JoinRequest 
            modelBuilder.Entity<JoinRequest>()
                .HasOne(j => j.ApprovedBy)
                .WithMany(n => n.JoinRequests)
                .HasForeignKey(j => j.ApprovedById)
                .OnDelete(DeleteBehavior.Restrict);

            //  ProjectLeader => InvitationRequest
            modelBuilder.Entity<InvitationRequest>()
                 .HasOne(p => p.Sender)
                 .WithMany(a => a.SentInvitations)
                 .HasForeignKey(p => p.SenderId)
                 .OnDelete(DeleteBehavior.Restrict);

            //  ProjectLeader => Project
            modelBuilder.Entity<Project>()
                .HasOne(p => p.Leader)
                .WithOne(jr => jr.CreatedProject)
                .HasForeignKey<Project>(p => p.LeaderId)
                .OnDelete(DeleteBehavior.Restrict);

            //  ProjectLeader => ProjectFile
            modelBuilder.Entity<ProjectFile>()
                .HasOne(p => p.UploadedBy)
                .WithMany(jr => jr.UploadedFiles)
                .HasForeignKey(p => p.UploadedById)
                .OnDelete(DeleteBehavior.Restrict);

            //         < --Project-- >

            //   Project -> JoinRequest
            modelBuilder.Entity<JoinRequest>()
                .HasOne(r => r.Project)
                .WithMany(p => p.JoinRequests)
                .HasForeignKey(r => r.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);

            //   Project -> ProjectMember
            modelBuilder.Entity<ProjectMember>()
                .HasOne(r => r.JoinedProject)
                .WithMany(p => p.Members)
                .HasForeignKey(r => r.JoinedProjectId)
                .OnDelete(DeleteBehavior.Restrict);

            //   Project -> ProjectFile
            modelBuilder.Entity<ProjectFile>()
                .HasOne(r => r.Project)
                .WithMany(p => p.Files)
                .HasForeignKey(r => r.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);

            //   Project -> invitationRequest
            modelBuilder.Entity<InvitationRequest>()
                .HasOne(r => r.TargetProject)
                .WithMany(p => p.SentInvitations)
                .HasForeignKey(r => r.TargetProjectId)
                .OnDelete(DeleteBehavior.Restrict);

            //   Project -> ProjectEvaluation
            modelBuilder.Entity<ProjectEvaluation>()
               .HasOne(pe => pe.Project)
               .WithMany(p => p.Evaluations)
               .HasForeignKey(pe => pe.ProjectId)
               .OnDelete(DeleteBehavior.Restrict);

            //         < --ProjectMember-- >

            //  ProjectMember -> JoinRequest
            modelBuilder.Entity<JoinRequest>()
                .HasOne(j => j.Requester)
                .WithMany(r => r.SentJoinRequests)
                .HasForeignKey(j => j.RequesterId)
                .OnDelete(DeleteBehavior.Restrict);

            //  ProjectMember -> JoinRequest
            modelBuilder.Entity<InvitationRequest>()
                .HasOne(j => j.Receiver)
                .WithMany(r => r.ReceivedInvitations)
                .HasForeignKey(j => j.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            //         < --Expert-- >

            //  Expert -> ProjectEvaluation

            modelBuilder.Entity<ProjectEvaluation>()
                .HasOne(pe => pe.Expert)
                .WithMany(e => e.Evaluations)
                .HasForeignKey(pe => pe.ExpertId)
                .OnDelete(DeleteBehavior.Restrict);

            //         < --Admin-- >

            //  Admin -> Researcher

            modelBuilder.Entity<Researcher>()
                .HasOne(r => r.ApprovedByAdmin)
                .WithMany(p => p.ApprovedResearchers)
                .HasForeignKey(r => r.ApprovedByAdminId)
                .OnDelete(DeleteBehavior.Restrict);

            //  Admin -> Expert

            modelBuilder.Entity<Researcher>()
                .HasOne(r => r.ApprovedByAdmin)
                .WithMany(a => a.ApprovedResearchers)
                .HasForeignKey(r => r.ApprovedByAdminId)
                .OnDelete(DeleteBehavior.Restrict);

            //  Admin -> Project

            modelBuilder.Entity<Project>()
                .HasOne(p => p.ApprovedByAdmin)
                .WithMany(a => a.ApprovedProjects)
                .HasForeignKey(p => p.ApprovedByAdminId)
                .OnDelete(DeleteBehavior.Restrict);

          

            base.OnModelCreating(modelBuilder);
        }

    }
}
