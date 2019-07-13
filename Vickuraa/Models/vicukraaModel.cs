namespace Vickuraa.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class vicukraaModel : DbContext
    {
        public vicukraaModel()
            : base("name=vicukraaModel")
        {
        }

        public virtual DbSet<booking> bookings { get; set; }
        public virtual DbSet<crew> crews { get; set; }
        public virtual DbSet<crewRank> crewRanks { get; set; }
        public virtual DbSet<destination> destinations { get; set; }
        public virtual DbSet<package> packages { get; set; }
        public virtual DbSet<packageType> packageTypes { get; set; }
        public virtual DbSet<route> routes { get; set; }
        public virtual DbSet<schedule> schedules { get; set; }
        public virtual DbSet<user> users { get; set; }
        public virtual DbSet<userDetail> userDetails { get; set; }
        public virtual DbSet<vessel> vessels { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<booking>()
                .Property(e => e.receiverName)
                .IsUnicode(false);

            modelBuilder.Entity<booking>()
                .Property(e => e.receiverAddress)
                .IsUnicode(false);

            modelBuilder.Entity<booking>()
                .Property(e => e.senderName)
                .IsUnicode(false);

            modelBuilder.Entity<crew>()
                .Property(e => e.crewName)
                .IsUnicode(false);

            modelBuilder.Entity<crewRank>()
                .Property(e => e.rankName)
                .IsUnicode(false);

            modelBuilder.Entity<crewRank>()
                .HasMany(e => e.crews)
                .WithRequired(e => e.crewRank)
                .HasForeignKey(e => e.rankId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<destination>()
                .Property(e => e.destinationName)
                .IsUnicode(false);

            modelBuilder.Entity<destination>()
                .HasMany(e => e.routes)
                .WithOptional(e => e.destination)
                .HasForeignKey(e => e.arrivalportID);

            modelBuilder.Entity<destination>()
                .HasMany(e => e.routes1)
                .WithOptional(e => e.destination1)
                .HasForeignKey(e => e.departureportID);

            modelBuilder.Entity<package>()
                .HasMany(e => e.bookings)
                .WithRequired(e => e.package)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<packageType>()
                .Property(e => e.packageType1)
                .IsUnicode(false);

            modelBuilder.Entity<packageType>()
                .HasMany(e => e.packages)
                .WithRequired(e => e.packageType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<route>()
                .HasMany(e => e.schedules)
                .WithRequired(e => e.route)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<schedule>()
                .HasMany(e => e.bookings)
                .WithRequired(e => e.schedule)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<user>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.type)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.bookings)
                .WithOptional(e => e.user)
                .HasForeignKey(e => e.customerID);

            modelBuilder.Entity<user>()
                .HasMany(e => e.crews)
                .WithRequired(e => e.user)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.schedules)
                .WithRequired(e => e.user)
                .HasForeignKey(e => e.enteredUser)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.vessels)
                .WithRequired(e => e.user)
                .HasForeignKey(e => e.ownerID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<userDetail>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<userDetail>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<userDetail>()
                .Property(e => e.ContactNo)
                .IsUnicode(false);

            modelBuilder.Entity<userDetail>()
                .Property(e => e.ContactNo2)
                .IsUnicode(false);

            modelBuilder.Entity<vessel>()
                .Property(e => e.vesselName)
                .IsUnicode(false);

            modelBuilder.Entity<vessel>()
                .HasMany(e => e.crews)
                .WithRequired(e => e.vessel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<vessel>()
                .HasMany(e => e.packages)
                .WithRequired(e => e.vessel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<vessel>()
                .HasMany(e => e.schedules)
                .WithRequired(e => e.vessel)
                .WillCascadeOnDelete(false);
        }
    }
}
