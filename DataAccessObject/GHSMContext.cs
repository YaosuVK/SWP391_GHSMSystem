﻿using BusinessObject.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObject
{
    public class GHSMContext : IdentityDbContext<Account>
    {
        public GHSMContext() : base()
        { }
        public GHSMContext(DbContextOptions<GHSMContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TreatmentOutcome>()
                .HasOne(e => e.Appointment)
                .WithOne(e => e.TreatmentOutcome)
                .HasForeignKey<TreatmentOutcome>(e => e.AppointmentID);

            modelBuilder.Entity<Account>()
                .HasOne(a => a.ConsultantProfile)
                .WithOne(s => s.Account)
                .HasForeignKey<ConsultantProfile>(s => s.AccountID);

            modelBuilder.Entity<Services>()
                .HasOne(s => s.Clinic)
                .WithMany(c => c.Services)
                .HasForeignKey(s => s.ClinicID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Slot>()
                .HasOne(s => s.Clinic)
                .WithMany(c => c.Slots)
                .HasForeignKey(s => s.ClinicID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Customer)
                .WithMany(c => c.CustomerAppointments)
                .HasForeignKey(a => a.CustomerID)
                .OnDelete(DeleteBehavior.Restrict); // Hoặc NoAction

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Consultant)
                .WithMany(c => c.ConsultantAppointments)
                .HasForeignKey(a => a.ConsultantID)
                .OnDelete(DeleteBehavior.Restrict); // Tránh Cascade nhiều nhánh

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Slot)
                .WithMany(s => s.Appointments)
                .HasForeignKey(a => a.SlotID)
                .OnDelete(DeleteBehavior.Restrict); // Chọn chỉ 1 để Cascade nếu thực sự cần

            modelBuilder.Entity<AppointmentDetail>()
                .HasOne(ad => ad.Service)
                .WithMany(s => s.AppointmentDetails)
                .HasForeignKey(ad => ad.ServicesID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TreatmentOutcome>()
                .HasOne(to => to.Customer)
                .WithMany(c => c.CustomerTreatmentOutcomes)
                .HasForeignKey(to => to.CustomerID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TreatmentOutcome>()
                .HasOne(to => to.Consultant)
                .WithMany(c => c.ConsultantTreatmentOutcomes)
                .HasForeignKey(to => to.ConsultantID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<LabTest>()
                .HasOne(l => l.TreatmentOutcome)
                .WithMany(to => to.LabTests)
                .HasForeignKey(l => l.TreatmentID)
                .OnDelete(DeleteBehavior.Restrict); // để tránh lỗi cascade

            modelBuilder.Entity<LabTest>()
                .HasOne(l => l.Staff)         // navigation property ở Labtest
                .WithMany(s => s.LabTests)    // navigation property ở Staff
                .HasForeignKey(l => l.StaffID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MenstrualCycle>()
                .HasOne(m => m.Prediction)
                .WithOne(p => p.MenstrualCycle)
                .HasForeignKey<CyclePrediction>(p => p.MenstrualCycleID);

            /*modelBuilder.Entity<BusinessObject.Model.FeedBack>()
                .HasOne(f => f.Service)
                .WithMany(s => s.FeedBacks)
                .HasForeignKey(f => f.ServicesID)
                .OnDelete(DeleteBehavior.NoAction);*/

            modelBuilder.Entity<ConsultantSlot>()
            .HasKey(x => new { x.ConsultantID, x.SlotID }); // 👈 KHÓA CHÍNH tổ hợp

            modelBuilder.Entity<ConsultantSlot>()
                .HasOne(cs => cs.Consultant)
                .WithMany(a => a.ConsultantSlots)
                .HasForeignKey(cs => cs.ConsultantID)
                .OnDelete(DeleteBehavior.Restrict); // Tránh xóa dây chuyền nếu cần

            modelBuilder.Entity<ConsultantSlot>()
                .HasOne(cs => cs.Slot)
                .WithMany(s => s.ConsultantSlots)
                .HasForeignKey(cs => cs.SlotID);

            // Configure QnAMessage entity
            modelBuilder.Entity<QnAMessage>()
                .HasOne(m => m.Question)
                .WithMany(q => q.Messages)
                .HasForeignKey(m => m.QuestionID)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            modelBuilder.Entity<QnAMessage>()
                .HasOne(m => m.Customer)
                .WithMany(a => a.QnACustomerMessages)
                .HasForeignKey(m => m.CustomerID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<QnAMessage>()
                .HasOne(m => m.Consultant)
                .WithMany(a => a.QnAConsultantMessages)
                .HasForeignKey(m => m.ConsultantID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<QnAMessage>()
                .HasOne(m => m.ParentMessage)
                .WithMany(m => m.Replies)
                .HasForeignKey(m => m.ParentMessageId)
                .IsRequired(false) // ParentMessageId can be null
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            // Configure Message entity (for chat)
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Conversation)
                .WithMany(c => c.Messages)
                .HasForeignKey(m => m.ConversationID)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete messages when conversation is deleted

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany(a => a.SentChatMessages)
                .HasForeignKey(m => m.SenderID)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Receiver)
                .WithMany(a => a.ReceivedChatMessages)
                .HasForeignKey(m => m.ReceiverID)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            modelBuilder.Entity<Account>()
                .HasMany(a => a.SentChatMessages)
                .WithOne(m => m.Sender)
                .HasForeignKey(m => m.SenderID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Account>()
                .HasMany(a => a.ReceivedChatMessages)
                .WithOne(m => m.Receiver)
                .HasForeignKey(m => m.ReceiverID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Conversation>()
                .HasOne(c => c.User1)
                .WithMany(a => a.ConversationsAsUser1)
                .HasForeignKey(c => c.User1ID)
                .OnDelete(DeleteBehavior.NoAction); // Change to NoAction

            modelBuilder.Entity<Conversation>()
                .HasOne(c => c.User2)
                .WithMany(a => a.ConversationsAsUser2)
                .HasForeignKey(c => c.User2ID)
                .OnDelete(DeleteBehavior.NoAction); // Change to NoAction

            /*List<IdentityRole> roles = new List<IdentityRole>
               {
                   new IdentityRole
                   {
                       Name = "Admin",
                       NormalizedName = "ADMIN"
                   },
                   new IdentityRole
                   {
                       Name = "Customer",
                       NormalizedName = "CUSTOMER"
                   },
                   new IdentityRole
                  {
                       Name = "Consultant",
                       NormalizedName = "CONSULTANT"
                   },
                     new IdentityRole
                  {
                       Name = "Manager",
                       NormalizedName = "MANAGER"
                   },
                   new IdentityRole
                  {
                       Name = "Staff",
                       NormalizedName = "STAFF"
                   }
              };
            modelBuilder.Entity<IdentityRole>().HasData(roles);*/

            /*//Add this to keep the asp.net userrole, then migrations
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { RoleId = "your-admin-role-id", UserId = "your-admin-user-id" },
                new IdentityUserRole<string> { RoleId = "your-customer-role-id", UserId = "your-customer-user-id" },
                new IdentityUserRole<string> { RoleId = "your-admin-role-id", UserId = "your-admin-user-id" },
                new IdentityUserRole<string> { RoleId = "your-customer-role-id", UserId = "your-customer-user-id" },
                new IdentityUserRole<string> { RoleId = "your-admin-role-id", UserId = "your-admin-user-id" },
                new IdentityUserRole<string> { RoleId = "your-customer-role-id", UserId = "your-customer-user-id" }
            );*/
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<ImageBlog> ImageBlogs { get; set; }
        public DbSet<ConsultantProfile> ConsultantProfiles { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentDetail> AppointmentDetails { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Services> Services { get; set; }
        public DbSet<ImageService> ImageServices { get; set; }
        public DbSet<TreatmentOutcome> TreatmentOutcomes { get; set; }
        public DbSet<MenstrualCycle> MenstrualCycles { get; set; }
        public DbSet<CyclePrediction> CyclePredictions { get; set; }
        public DbSet<Slot> Slots { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<FeedBack> FeedBacks { get; set; }
        public DbSet<WorkingHour> WorkingHours { get; set; }
        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<LabTest> Labtests { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<ConsultantSlot> ConsultantSlots { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<QnAMessage> QnAMessages { get; set; } // New DbSet for Q&A messages
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                if (!optionsBuilder.IsConfigured)
                {
                    optionsBuilder.UseSqlServer(GetConnectionString());
                }
            }
        }
        private string GetConnectionString()
        {
            IConfiguration config = new ConfigurationBuilder()
                 .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..", "GHSMSystem"))
                 .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                 .Build();
            var strConn = config["ConnectionStrings:DbConnection"];

            return strConn;
        }
    }
}
