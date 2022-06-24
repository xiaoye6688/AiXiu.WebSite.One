using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace AiXiu.Model
{
    public partial class AiXiuDB : DbContext
    {
        public AiXiuDB()
            : base("name=AiXiuDB")
        {
        }

        public virtual DbSet<TBLogins> TBLogins { get; set; }
        public virtual DbSet<TBUsers> TBUsers { get; set; }
        public virtual DbSet<TBVideos> TBVideos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TBLogins>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<TBLogins>()
                .Property(e => e.MobileNumber)
                .IsUnicode(false);

            modelBuilder.Entity<TBLogins>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<TBLogins>()
                .HasOptional(e => e.TBUsers)
                .WithRequired(e => e.TBLogins);

            modelBuilder.Entity<TBUsers>()
                .Property(e => e.Avatar)
                .IsUnicode(false);

            modelBuilder.Entity<TBUsers>()
                .Property(e => e.ADDress)
                .IsUnicode(false);

            modelBuilder.Entity<TBUsers>()
                .HasMany(e => e.TBVideos)
                .WithOptional(e => e.TBUsers)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<TBVideos>()
                .Property(e => e.VideoId)
                .IsUnicode(false);

            modelBuilder.Entity<TBVideos>()
                .Property(e => e.Location)
                .IsUnicode(false);

            modelBuilder.Entity<TBVideos>()
                .Property(e => e.CoverURL)
                .IsUnicode(false);

            modelBuilder.Entity<TBVideos>()
                .Property(e => e.UploadTime)
                .IsUnicode(false);
        }
    }
}
