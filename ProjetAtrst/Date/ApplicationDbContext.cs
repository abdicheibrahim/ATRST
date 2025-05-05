using ProjetAtrst.Models;

namespace ProjetAtrst.Date
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Project> Projects { get; set; } 
        public DbSet<Admin> Admins { get; set; } 
        public DbSet<Expert> Experts { get; set; } 
        public DbSet<Researcher> Researchers { get; set; } 
        public DbSet<ProjectExpert> ProjectExperts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // التهيئة السابقة لـ ProjectExpert
            modelBuilder.Entity<ProjectExpert>()
                .HasKey(e => new { e.ProjectId, e.ExpertId });

            modelBuilder.Entity<ProjectExpert>()
                .HasOne(pe => pe.Project)
                .WithMany(p => p.ProjectExperts)
                .HasForeignKey(pe => pe.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProjectExpert>()
                .HasOne(pe => pe.Expert)
                .WithMany(e => e.ProjectExperts)
                .HasForeignKey(pe => pe.ExpertId)
                .OnDelete(DeleteBehavior.Restrict);

            // تهيئة العلاقة بين Researcher وProject
            modelBuilder.Entity<Researcher>()
                .HasOne(r => r.Project)
                .WithMany(p => p.Researchers)
                .HasForeignKey(r => r.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);

            // تهيئة العلاقة بين Admin وExperts
            modelBuilder.Entity<Expert>()
                .HasOne(e => e.Admin)
                .WithMany(a => a.Experts)
                .HasForeignKey(e => e.AdminId)
                .OnDelete(DeleteBehavior.Restrict);

            // تهيئة العلاقة بين Admin وResearchers
            modelBuilder.Entity<Researcher>()
                .HasOne(r => r.Admin)
                .WithMany(a => a.Researchers)
                .HasForeignKey(r => r.AdminId)
                .OnDelete(DeleteBehavior.Restrict);

            // تهيئة العلاقة بين Admin وProjects
            modelBuilder.Entity<Project>()
                .HasOne(p => p.Admin)
                .WithMany(a => a.Projects)
                .HasForeignKey(p => p.AdminId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }

    }
}
