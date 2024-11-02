using System.Reflection.Emit;
using efcoreApp.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace efcoreApp.Data;

public class efcoreAppContext : IdentityDbContext<efcoreAppUser>
{
    public efcoreAppContext(DbContextOptions<efcoreAppContext> options)
        : base(options)
    {
    }
    public DbSet<Kurs> Kurslar => Set<Kurs>();

    public DbSet<Ogrenci> Ogrenciler => Set<Ogrenci>();

    public DbSet<KursKayit> KursKayitlari => Set<KursKayit>();

    public DbSet<Video> Videos => Set<Video>();

    public DbSet<Post> Posts => Set<Post>();

    public DbSet<VideoWatchedCheck> VideoWatchedChecks { get; set; }

    public DbSet<Category> Categories => Set<Category>();

    public DbSet<Quiz> Quizzes => Set<Quiz>();
    public DbSet<Question> Questions => Set<Question>();
    public DbSet<Answer> Answers => Set<Answer>();
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
