using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Expense_Tracking.Models
{
    public partial class ExpenseTrackingContext : DbContext
    {
        public ExpenseTrackingContext()
        {
        }

        public ExpenseTrackingContext(DbContextOptions<ExpenseTrackingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Expenses> Expenses { get; set; }
        public virtual DbSet<ItemList> ItemList { get; set; }
        public virtual DbSet<Items> Items { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=SUMEETHKUMAR\\SQLEXPRESS;Database=ExpenseTracking;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CatId)
                    .HasName("PK__category__DD5DDDBDFE2150CC");

                entity.ToTable("category");

                entity.Property(e => e.CatId)
                    .HasColumnName("cat_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.CatName)
                    .IsRequired()
                    .HasColumnName("cat_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Expenses>(entity =>
            {
                entity.HasKey(e => e.ExpId)
                    .HasName("PK__expenses__FED8E5E90F5D67AC");

                entity.ToTable("expenses");

                entity.Property(e => e.ExpId)
                    .HasColumnName("exp_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.ExpenseDate)
                    .HasColumnName("expense_date")
                    .HasColumnType("date");

                entity.Property(e => e.ItemlId).HasColumnName("iteml_id");

                entity.Property(e => e.TotalExp).HasColumnName("total_exp");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Expenses)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("FK__expenses__userid__2C3393D0");
            });

            modelBuilder.Entity<ItemList>(entity =>
            {
                entity.HasKey(e => e.ItemlId)
                    .HasName("PK__item_lis__F59EDD179BBD531E");

                entity.ToTable("item_list");

                entity.Property(e => e.ItemlId)
                    .HasColumnName("iteml_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.ExpId).HasColumnName("exp_id");

                entity.Property(e => e.ItemId).HasColumnName("item_id");

                entity.HasOne(d => d.Exp)
                    .WithMany(p => p.ItemList)
                    .HasForeignKey(d => d.ExpId)
                    .HasConstraintName("FK__item_list__exp_i__31EC6D26");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.ItemList)
                    .HasForeignKey(d => d.ItemId)
                    .HasConstraintName("FK__item_list__item___30F848ED");
            });

            modelBuilder.Entity<Items>(entity =>
            {
                entity.HasKey(e => e.ItemId)
                    .HasName("PK__items__52020FDDC4C9FD60");

                entity.ToTable("items");

                entity.Property(e => e.ItemId)
                    .HasColumnName("item_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.CatId).HasColumnName("cat_id");

                entity.Property(e => e.ItemBill)
                    .HasColumnName("item_bill")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ItemName)
                    .IsRequired()
                    .HasColumnName("item_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ItemPrice).HasColumnName("item_price");

                entity.HasOne(d => d.Cat)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.CatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__items__cat_id__267ABA7A");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.Userid)
                    .HasName("PK__users__CBA1B257DB561EF1");

                entity.ToTable("users");

                entity.Property(e => e.Userid)
                    .HasColumnName("userid")
                    .ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('name@123')");

                entity.Property(e => e.Phoneno).HasColumnName("phoneno");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
