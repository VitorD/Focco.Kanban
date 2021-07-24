using FoccoEmFrente.Kanban.Application.Entities;
using FoccoEmFrente.Kanban.Application.Mapping;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FoccoEmFrente.Kanban.Application.Context
{
    public class KanbanContext : IdentityDbContext
    {
        public KanbanContext(DbContextOptions<KanbanContext> options) : base(options)
        {

        }

        public DbSet<Activity> Activities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ActivityMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
