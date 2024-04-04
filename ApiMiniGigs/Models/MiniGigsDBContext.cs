using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ApiMiniGigs.Models
{
    public partial class MiniGigsDBContext : DbContext
    {
        public MiniGigsDBContext()
        {
        }

        public MiniGigsDBContext(DbContextOptions<MiniGigsDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Balance> Balances { get; set; } = null!;
        public virtual DbSet<MyWork> MyWorks { get; set; } = null!;
        public virtual DbSet<WorkCompletion> WorkCompletions { get; set; } = null!;
        public virtual DbSet<ProgressUser> ProgressUsers { get; set; } = null!;
        public virtual DbSet<HistoryPayment> HistoryPayments { get; set; } = null!;
        public virtual DbSet<FinishedTask> FinishedTasks { get; set; } = null!;
        public virtual DbSet<OperationHistory> OperationHistories { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderStatus> OrderStatuses { get; set; } = null!;
        public virtual DbSet<OrderType> OrderTypes { get; set; } = null!;
        public virtual DbSet<Platform> Platforms { get; set; } = null!;
        public virtual DbSet<Profile> Profiles { get; set; } = null!;
        public virtual DbSet<Rating> Ratings { get; set; } = null!;
        public virtual DbSet<Tariff> Tariffs { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Setting> Settings { get; set; } = null!;
        public virtual DbSet<SupportMessage> SupportMessages { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserLevel> UserLevels { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=ALEXANDER-LAPTO\\SQLEXPRESS;Initial Catalog=MiniGigsDB;Persist Security Info=True;User ID=sa;Password=123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Balance>(entity =>
            {
                entity.HasKey(e => e.IdBalance)
                    .HasName("PK__Balance__D826D76AA1255197");

                entity.ToTable("Balance");

                entity.Property(e => e.IdBalance)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id_balance");

                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(15, 2)")
                    .HasColumnName("amount");
            });

            modelBuilder.Entity<FinishedTask>(entity =>
            {
                entity.HasKey(e => e.IdFinishedTask)
                    .HasName("PK__Finished__858418893AB0CF6A");

                entity.ToTable("FinishedTask");

                entity.Property(e => e.IdFinishedTask)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id_finished_task");

                entity.Property(e => e.IdOrder).HasColumnName("id_order");

                entity.Property(e => e.ReportDescription).HasColumnName("report_description");

            });

            modelBuilder.Entity<OperationHistory>(entity =>
            {
                entity.HasKey(e => e.OperationNumber)
                    .HasName("PK__Operatio__A8C498E3A772DEE8");

                entity.ToTable("OperationHistory");

                entity.Property(e => e.OperationNumber)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("operation_number");

                entity.Property(e => e.DateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("date_time");

                entity.Property(e => e.IdBalance).HasColumnName("id_balance");

                entity.Property(e => e.IdSetting).HasColumnName("id_setting");

            });

            modelBuilder.Entity<Tariff>(entity =>
            {
                entity.HasKey(e => e.IdTariff)
                       .HasName("PK__Tariff__747D0274982970CC");

                entity.ToTable("Tariff");

                entity.Property(e => e.IdTariff)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id_tariff");
                entity.Property(e => e.NameTariff).HasColumnName("name_tariff");

            });

            modelBuilder.Entity<HistoryPayment>(entity =>
            {
                entity.HasKey(e => e.IdHistoryPayment) // Определение первичного ключа
                    .HasName("PK_HistoryPayment");

                entity.ToTable("HistoryPayment");

                entity.Property(e => e.IdHistoryPayment)
                    .HasColumnName("id_histiry_payment")
                    .ValueGeneratedOnAdd(); // Автоинкремент

                entity.Property(e => e.IdUser)
                   .HasColumnName("id_user");

                entity.Property(e => e.Task)
                  .IsRequired()
                  .HasMaxLength(100)
                  .HasColumnName("task");

                entity.Property(e => e.DateTime)
               .HasColumnType("datetime")
               .HasColumnName("date_time");

                entity.Property(e => e.TotalCost)
                  .HasColumnType("decimal(10, 2)")
                  .HasColumnName("total_cost");
            });

            modelBuilder.Entity<ProgressUser>(entity =>
            {
                entity.HasKey(e => e.IdProgressUser) // Определение первичного ключа
                    .HasName("PK_ProgressUser");

                entity.ToTable("ProgressUser");

                entity.Property(e => e.IdProgressUser)
                    .HasColumnName("id_progress_user")
                    .ValueGeneratedOnAdd(); // Автоинкремент

                entity.Property(e => e.IdUser)
                   .HasColumnName("id_user");


                entity.Property(e => e.Value)
                  .HasColumnType("decimal(10, 2)")
                  .HasColumnName("value");
            });

            modelBuilder.Entity<MyWork>(entity =>
            {
                entity.HasKey(e => e.IdMyWork) // Определение первичного ключа
                    .HasName("PK_MyWork");

                entity.ToTable("MyWork");

                entity.Property(e => e.IdMyWork)
                    .HasColumnName("id_my_work")
                    .ValueGeneratedOnAdd(); // Автоинкремент

                entity.Property(e => e.IdUser)
                   .HasColumnName("id_user");
                entity.Property(e => e.IdOrder)
              .HasColumnName("id_order");
                entity.HasOne(m => m.Order)
                    .WithMany()
                    .HasForeignKey(m => m.IdOrder);

                entity.Property(e => e.NameWorkStatus)
                        .HasMaxLength(50)
                        .HasColumnName("name_work_status");
            });

            modelBuilder.Entity<WorkCompletion>(entity =>
            {
                entity.HasKey(e => e.IdWorkCompletion) // Определение первичного ключа
                    .HasName("PK_WorkCompletion");

                entity.ToTable("WorkCompletion");

                entity.Property(e => e.IdWorkCompletion)
                    .HasColumnName("id_work_completion")
                    .ValueGeneratedOnAdd(); // Автоинкремент

                entity.Property(e => e.IdUser)
                   .HasColumnName("id_user");
                entity.Property(e => e.IdOrder)
              .HasColumnName("id_order");
                entity.HasOne(m => m.Order)
                    .WithMany()
                    .HasForeignKey(m => m.IdOrder);

                entity.Property(e => e.Comment)
                        .HasMaxLength(500)
                        .HasColumnName("Comment");
                entity.Property(e => e.PhotoPath)
                        .HasMaxLength(255)
                        .HasColumnName("PhotoPath");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.IdOrder) // Определение первичного ключа
                    .HasName("PK_Order");

                entity.ToTable("Order");

                entity.Property(e => e.IdOrder)
                    .HasColumnName("id_order")
                    .ValueGeneratedOnAdd(); // Автоинкремент

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("title");

                entity.Property(e => e.TaskDescription)
                    .HasColumnType("nvarchar(max)")
                    .HasColumnName("task_description");

                entity.Property(e => e.Link)
                    .HasMaxLength(255)
                    .HasColumnName("link");

                entity.Property(e => e.ReportComment)
                    .HasColumnType("nvarchar(max)")
                    .HasColumnName("report_comment");

                entity.Property(e => e.Cost)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("cost");

                entity.Property(e => e.Counts)
                    .HasColumnName("Counts");

                entity.Property(e => e.TaskAddition)
                    .HasColumnType("nvarchar(max)")
                    .HasColumnName("task_addition");

                entity.Property(e => e.CompletionTime)
                    .HasColumnType("datetime")
                    .HasColumnName("completion_time");

                entity.Property(e => e.IdOrderType)
                    .HasColumnName("id_order_type");

                entity.Property(e => e.IdTariff)
                    .HasColumnName("id_tariff");

                entity.Property(e => e.IdUser)
                    .HasColumnName("id_user");

                entity.Property(e => e.IdPlatform)
                    .HasColumnName("id_platform");

                entity.Property(e => e.IdOrderStatus)
                    .HasColumnName("id_order_status");
                
                entity.HasOne(o => o.User) // Указывает на то, что у заказа есть один пользователь
                .WithMany(u => u.Orders) // Предполагается, что в классе User есть коллекция Orders
                .HasForeignKey(o => o.IdUser);
                /*      // Настройка внешних ключей и связей
                      entity.HasOne(d => d.IdOrderStatusNavigation)
                          .WithMany(p => p.Orders)
                          .HasForeignKey(d => d.IdOrderStatus)
                          .HasConstraintName("FK_Order_OrderStatus");

                      entity.HasOne(d => d.IdTariffNavigation)
                          .WithMany(p => p.Orders)
                          .HasForeignKey(d => d.IdTariff)
                          .HasConstraintName("FK_Order_Tariff");

                      entity.HasOne(d => d.IdOrderTypeNavigation)
                          .WithMany(p => p.Orders)
                          .HasForeignKey(d => d.IdOrderType)
                          .HasConstraintName("FK_Order_OrderType");

                      entity.HasOne(d => d.IdPlatformNavigation)
                          .WithMany(p => p.Orders)
                          .HasForeignKey(d => d.IdPlatform)
                          .HasConstraintName("FK_Order_Platform");

                      entity.HasOne(d => d.IdUserNavigation)
                          .WithMany(p => p.Orders)
                          .HasForeignKey(d => d.IdUser)
                          .HasConstraintName("FK_Order_User");*/
            });


            modelBuilder.Entity<OrderStatus>(entity =>
            {
                entity.HasKey(e => e.IdOrderStatus)
                    .HasName("PK__OrderSta__D86D9D6C2FA7024E");

                entity.ToTable("OrderStatus");

                entity.Property(e => e.IdOrderStatus)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id_order_status");

                entity.Property(e => e.NameOrderStatus)
                    .HasMaxLength(50)
                    .HasColumnName("name_order_status");
            });

            modelBuilder.Entity<OrderType>(entity =>
            {
                entity.HasKey(e => e.IdOrderType)
                    .HasName("PK__OrderTyp__FCE1788739C691AC");

                entity.ToTable("OrderType");

                entity.Property(e => e.IdOrderType)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id_order_type");

                entity.Property(e => e.NameOrderType)
                    .HasMaxLength(100)
                    .HasColumnName("name_order_type");
            });

            modelBuilder.Entity<Platform>(entity =>
            {
                entity.HasKey(e => e.IdPlatform)
                    .HasName("PK__Platform__4B55E5B2E133BEA4");

                entity.ToTable("Platform");

                entity.Property(e => e.IdPlatform)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id_platform");

                entity.Property(e => e.AddressPlatform)
                    .HasMaxLength(255)
                    .HasColumnName("address_platform");

                entity.Property(e => e.NamePlatform)
                    .HasMaxLength(100)
                    .HasColumnName("name_platform");
            });

            modelBuilder.Entity<Profile>(entity =>
            {
                entity.HasKey(e => e.IdProfile)
                    .HasName("PK__Profile__0981A5762C5214FB");

                entity.ToTable("Profile");

                entity.HasIndex(e => e.IdUser, "UQ__Profile__D2D146367DA3E83D")
                    .IsUnique();

                entity.Property(e => e.IdProfile)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id_profile");

                entity.Property(e => e.IdRating).HasColumnName("id_rating");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.IdUserLevel).HasColumnName("id_user_level");

                entity.Property(e => e.Photo).HasColumnName("photo");

             
/*
                entity.HasOne(d => d.IdUserNavigation)
                    .WithOne(p => p.Profile)
                    .HasForeignKey<Profile>(d => d.IdUser)
                    .HasConstraintName("FK__Profile__id_user__0F624AF8");*/

            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.HasKey(e => e.IdRating)
                    .HasName("PK__Rating__12074E479CE7CD61");

                entity.ToTable("Rating");

                entity.Property(e => e.IdRating)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id_rating");

                entity.Property(e => e.Value)
                    .HasColumnType("decimal(3, 2)")
                    .HasColumnName("value");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.IdRole)
                    .HasName("PK__Role__3D48441D03A897BA");

                entity.ToTable("Role");

                entity.HasIndex(e => e.RoleName, "UQ__Role__8A2B61609A4642B7")
                    .IsUnique();

                entity.Property(e => e.IdRole)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id_role");

                entity.Property(e => e.RoleName).HasMaxLength(50);
            });

            modelBuilder.Entity<Setting>(entity =>
            {
                entity.HasKey(e => e.IdSetting)
                    .HasName("PK__Settings__6C27915AFCB6381F");

                entity.HasIndex(e => e.IdUser, "UQ__Settings__D2D146360D89B8DD")
                    .IsUnique();

                entity.Property(e => e.IdSetting)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id_setting");

                entity.Property(e => e.BirthDate)
                    .HasColumnType("date")
                    .HasColumnName("birth_date");

                entity.Property(e => e.Gender)
                    .HasMaxLength(10)
                    .HasColumnName("gender");

                entity.Property(e => e.IdProfile).HasColumnName("id_profile");

                entity.Property(e => e.IdUser).HasColumnName("id_user");
/*

                entity.HasOne(d => d.IdUserNavigation)
                    .WithOne(p => p.Setting)
                    .HasForeignKey<Setting>(d => d.IdUser)
                    .HasConstraintName("FK__Settings__id_use__160F4887");*/
            });

            modelBuilder.Entity<SupportMessage>(entity =>
            {
                entity.HasKey(e => e.IdMessage)
                    .HasName("PK__SupportM__460F3CF4D605C252");

                entity.ToTable("SupportMessage");

                entity.Property(e => e.IdMessage)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id_message");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.MessageText).HasColumnName("message_text");

            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("PK__User__D2D146374C92865B");

                entity.ToTable("User");

                entity.HasIndex(e => e.PhoneNumber, "UQ__User__A1936A6BFFE7B766")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__User__AB6E61642969C356")
                    .IsUnique();

                entity.Property(e => e.Amount)
                   .HasColumnType("decimal(15, 2)")
                   .HasColumnName("amount");

                entity.Property(e => e.IdUser)
                    .ValueGeneratedOnAdd()
                  .HasColumnName("id_user");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email");
                entity.Property(e => e.BirthDate)
                  .HasColumnType("date")
                  .HasColumnName("birth_date");

                entity.Property(e => e.Gender)
                    .HasMaxLength(10)
                    .HasColumnName("gender");
                entity.Property(e => e.FcmToken).HasMaxLength(255);

                entity.Property(e => e.IdRole).HasColumnName("id_role");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Nickname)
                    .HasMaxLength(50)
                    .HasColumnName("nickname");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .HasColumnName("password");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(100)
                    .HasColumnName("phone_number");

                entity.Property(e => e.Salt)
                    .HasMaxLength(256)
                    .HasColumnName("salt");

          /*      entity.HasMany(d => d.IdOrders)
                    .WithMany(p => p.IdUsers)
                    .UsingEntity<Dictionary<string, object>>(
                        "MyWork",
                        l => l.HasOne<Order>().WithMany().HasForeignKey("IdOrder").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__MyWork__id_order__2180FB33"),
                        r => r.HasOne<User>().WithMany().HasForeignKey("IdUser").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__MyWork__id_user__208CD6FA"),
                        j =>
                        {
                            j.HasKey("IdUser", "IdOrder").HasName("PK__MyWork__3F04FEC4012B79E5");

                            j.ToTable("MyWork");

                            j.IndexerProperty<int>("IdUser").HasColumnName("id_user");

                            j.IndexerProperty<int>("IdOrder").HasColumnName("id_order");
                        });*/
            });

            modelBuilder.Entity<UserLevel>(entity =>
            {
                entity.HasKey(e => e.IdUserLevel)
                    .HasName("PK__UserLeve__B916D127CB0D6805");

                entity.ToTable("UserLevel");

                entity.Property(e => e.IdUserLevel)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id_user_level");

                entity.Property(e => e.Level)
                    .HasMaxLength(50)
                    .HasColumnName("level");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
