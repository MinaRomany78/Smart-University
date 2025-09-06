using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Application> Applications { get; set; }
        public DbSet<ApplicationUserOtp> ApplicationUserOtps { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Assistant> Assistants { get; set; }
        public DbSet<PromoCode> PromoCodes { get; set; }
        public DbSet<UniversityCourse> UniversityCourses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<OptionalCourse> OptionalCourses { get; set; }
        public DbSet<CourseReview> CourseReviews { get; set; }
        public DbSet<UserOptionalCourse> UserOptionalCourses { get; set; }
        public DbSet<AssistantCourse> AssistantCourses { get; set; }
        public DbSet<DoctorAssistant> DoctorAssistants { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<SubjectTask> SubjectTasks { get; set; }
        public DbSet<TaskSubmission> TaskSubmissions { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<CommunityPost> CommunityPosts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<PostFile> PostFiles { get; set; }
        public DbSet<PostLink> PostLinks {  get; set; }
        public DbSet<SupportTicket> SupportTickets { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Term> Terms { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CourseSession> courseSessions { get; set; }
        public DbSet<SessionTask> SessionTask { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // PromoCode PK
            builder.Entity<PromoCode>()
                .HasKey(p => p.Code);

            // Student ↔ ApplicationUser (1-to-1)
            builder.Entity<Student>()
                .HasOne(s => s.ApplicationUser)
                .WithOne(u => u.Student)
                .HasForeignKey<Student>(s => s.ApplicationUserId);

            // Doctor ↔ ApplicationUser (1-to-1)
            builder.Entity<Doctor>()
                .HasOne(d => d.ApplicationUser)
                .WithOne(u => u.Doctor)
                .HasForeignKey<Doctor>(d => d.ApplicationUserId);

            // Assistant ↔ ApplicationUser (1-to-1)
            builder.Entity<Assistant>()
                .HasOne(a => a.ApplicationUser)
                .WithOne(u => u.Assistant)
                .HasForeignKey<Assistant>(a => a.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade على المستخدم

            // Student ↔ PromoCode (many-to-1)
            builder.Entity<Student>()
                .HasOne(s => s.PromoCodeEntity)
                .WithMany(p => p.Students)
                .HasForeignKey(s => s.PromoCode)
                .IsRequired(false);

            // OptionalCourse ↔ PromoCode (many-to-1)
            builder.Entity<OptionalCourse>()
                .HasOne(o => o.PromoCodeEntity)
                .WithMany(p => p.OptionalCourses)
                .HasForeignKey(o => o.PromoCode)
                .IsRequired(false);

            // Comment author (Student or Assistant) - no cascade
            builder.Entity<Comment>()
                .HasOne(c => c.Author)
                .WithMany()
                .HasForeignKey(c => c.AuthorId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<CommunityPost>()
                .HasOne(p => p.Author)
                .WithMany()
                .HasForeignKey(p => p.AuthorId)
                .OnDelete(DeleteBehavior.NoAction);

            // CommunityPost ↔ UniversityCourse (many-to-1)
            builder.Entity<CommunityPost>()
                .HasOne(p => p.UniversityCourse)
                .WithMany()
                .HasForeignKey(p => p.CourseID);

            // Feedback ↔ Assistant (no cascade)
            builder.Entity<Feedback>()
                .HasOne(f => f.Assistant)
                .WithMany(a => a.Feedbacks)
                .HasForeignKey(f => f.AssistantID)
                .OnDelete(DeleteBehavior.NoAction);

           // Feedback ↔ SubjectTask(no cascade) - منع المسارات المكررة
            builder.Entity<Feedback>()
                .HasOne(f => f.Task)
                .WithMany(t => t.Feedbacks)
                .HasForeignKey(f => f.TaskID)
                .OnDelete(DeleteBehavior.Restrict);

            // علاقة OptionalCourse <---> CourseReview
            builder.Entity<CourseReview>()
                .HasOne(r => r.Course)
                .WithMany(c => c.Reviews)
                .HasForeignKey(r => r.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            // علاقة CourseReview <---> ApplicationUser
            builder.Entity<CourseReview>()
                .HasOne(r => r.ApplicationUser)
                .WithMany() // لو حابب تضيف ICollection<CourseReview> في ApplicationUser حطها هنا
                .HasForeignKey(r => r.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<CommunityPost>()
                .HasOne(p => p.Author)
                .WithMany()
                .HasForeignKey(p => p.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<SessionTask>()
                .HasOne(t => t.CourseSession)
                .WithMany(s => s.Tasks)
                .HasForeignKey(t => t.CourseSessionId)
                .OnDelete(DeleteBehavior.Cascade);




            // Feedback ↔ Student (cascade ممكن نسيبها)
            builder.Entity<Feedback>()
                .HasOne(f => f.Student)
                .WithMany(s => s.Feedbacks)
                .HasForeignKey(f => f.StudentID)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Department>().HasData(
   new Department { Id = 1, Name = "General" },
       new Department { Id = 2, Name = "Computer Science" },
       new Department { Id = 3, Name = "Information Systems" }
       );

            // الترمات
            builder.Entity<Term>().HasData(
                new Term { Id = 1, Year = 1, TermNumber = 1 },
                new Term { Id = 2, Year = 1, TermNumber = 2 },
                new Term { Id = 3, Year = 2, TermNumber = 1 },
                new Term { Id = 4, Year = 2, TermNumber = 2 },
                new Term { Id = 5, Year = 3, TermNumber = 1 },
                new Term { Id = 6, Year = 3, TermNumber = 2 },
                new Term { Id = 7, Year = 4, TermNumber = 1 },
                new Term { Id = 8, Year = 4, TermNumber = 2 }
            );
            builder.Entity<UniversityCourse>().HasData(
                // ---- Year 1 Term 1 (General)
                new UniversityCourse { Id = 1, Name = "Mathematics 1", CreditHours = 3, DepartmentID = 1, TermId = 1, DoctorID = 1 },
                new UniversityCourse { Id = 2, Name = "Programming Basics", CreditHours = 3, DepartmentID = 1, TermId = 1, DoctorID = 2 },
                new UniversityCourse { Id = 3, Name = "Physics 1", CreditHours = 3, DepartmentID = 1, TermId = 1, DoctorID = 3 },
                new UniversityCourse { Id = 4, Name = "English 1", CreditHours = 3, DepartmentID = 1, TermId = 1, DoctorID = 4 },
                new UniversityCourse { Id = 5, Name = "Introduction to IT", CreditHours = 3, DepartmentID = 1, TermId = 1, DoctorID = 5 },
                new UniversityCourse { Id = 6, Name = "Critical Thinking", CreditHours = 3, DepartmentID = 1, TermId = 1, DoctorID = 6 },

                // ---- Year 1 Term 2 (General)
                new UniversityCourse { Id = 7, Name = "Mathematics 2", CreditHours = 3, DepartmentID = 1, TermId = 2, DoctorID = 7 },
                new UniversityCourse { Id = 8, Name = "Object Oriented Programming", CreditHours = 3, DepartmentID = 1, TermId = 2, DoctorID = 8 },
                new UniversityCourse { Id = 9, Name = "Physics 2", CreditHours = 3, DepartmentID = 1, TermId = 2, DoctorID = 9 },
                new UniversityCourse { Id = 10, Name = "English 2", CreditHours = 3, DepartmentID = 1, TermId = 2, DoctorID = 10 },
                new UniversityCourse { Id = 11, Name = "Introduction to Database", CreditHours = 3, DepartmentID = 1, TermId = 2, DoctorID = 11 },
                new UniversityCourse { Id = 12, Name = "Communication Skills", CreditHours = 3, DepartmentID = 1, TermId = 2, DoctorID = 12 },

                // ---- Year 2 Term 1 (General)
                new UniversityCourse { Id = 13, Name = "Mathematics 3", CreditHours = 3, DepartmentID = 1, TermId = 3, DoctorID = 13 },
                new UniversityCourse { Id = 14, Name = "Data Structures", CreditHours = 3, DepartmentID = 1, TermId = 3, DoctorID = 14 },
                new UniversityCourse { Id = 15, Name = "Computer Organization", CreditHours = 3, DepartmentID = 1, TermId = 3, DoctorID = 15 },
                new UniversityCourse { Id = 16, Name = "Probability & Statistics", CreditHours = 3, DepartmentID = 1, TermId = 3, DoctorID = 1 },
                new UniversityCourse { Id = 17, Name = "Operating Systems Basics", CreditHours = 3, DepartmentID = 1, TermId = 3, DoctorID = 2 },
                new UniversityCourse { Id = 18, Name = "Ethics", CreditHours = 3, DepartmentID = 1, TermId = 3, DoctorID = 3 },

                // ---- Year 2 Term 2 (General)
                new UniversityCourse { Id = 19, Name = "Mathematics 4", CreditHours = 3, DepartmentID = 1, TermId = 4, DoctorID = 4 },
                new UniversityCourse { Id = 20, Name = "Algorithms", CreditHours = 3, DepartmentID = 1, TermId = 4, DoctorID = 5 },
                new UniversityCourse { Id = 21, Name = "Digital Logic", CreditHours = 3, DepartmentID = 1, TermId = 4, DoctorID = 6 },
                new UniversityCourse { Id = 22, Name = "Software Engineering Basics", CreditHours = 3, DepartmentID = 1, TermId = 4, DoctorID = 7 },
                new UniversityCourse { Id = 23, Name = "Database Systems", CreditHours = 3, DepartmentID = 1, TermId = 4, DoctorID = 8 },
                new UniversityCourse { Id = 24, Name = "Technical Writing", CreditHours = 3, DepartmentID = 1, TermId = 4, DoctorID = 9 },

                // ---- Year 3 Term 1 (Specialization - CS)
                new UniversityCourse { Id = 25, Name = "Advanced Algorithms", CreditHours = 3, DepartmentID = 2, TermId = 5, DoctorID = 10 },
                new UniversityCourse { Id = 26, Name = "Theory of Computation", CreditHours = 3, DepartmentID = 2, TermId = 5, DoctorID = 11 },
                new UniversityCourse { Id = 27, Name = "Operating Systems", CreditHours = 3, DepartmentID = 2, TermId = 5, DoctorID = 12 },
                new UniversityCourse { Id = 28, Name = "Computer Networks", CreditHours = 3, DepartmentID = 2, TermId = 5, DoctorID = 13 },
                new UniversityCourse { Id = 29, Name = "Artificial Intelligence", CreditHours = 3, DepartmentID = 2, TermId = 5, DoctorID = 14 },
                new UniversityCourse { Id = 30, Name = "Compiler Design", CreditHours = 3, DepartmentID = 2, TermId = 5, DoctorID = 15 },

                // ---- Year 3 Term 1 (Specialization - IS)
                new UniversityCourse { Id = 31, Name = "Information Systems Analysis", CreditHours = 3, DepartmentID = 3, TermId = 5, DoctorID = 1 },
                new UniversityCourse { Id = 32, Name = "Business Process Management", CreditHours = 3, DepartmentID = 3, TermId = 5, DoctorID = 2 },
                new UniversityCourse { Id = 33, Name = "Database Administration", CreditHours = 3, DepartmentID = 3, TermId = 5, DoctorID = 3 },
                new UniversityCourse { Id = 34, Name = "Enterprise Systems", CreditHours = 3, DepartmentID = 3, TermId = 5, DoctorID = 4 },
                new UniversityCourse { Id = 35, Name = "Systems Security", CreditHours = 3, DepartmentID = 3, TermId = 5, DoctorID = 5 },
                new UniversityCourse { Id = 36, Name = "Decision Support Systems", CreditHours = 3, DepartmentID = 3, TermId = 5, DoctorID = 6 },

                // ---- Year 3 Term 2 (Specialization - CS)
                new UniversityCourse { Id = 37, Name = "Parallel Computing", CreditHours = 3, DepartmentID = 2, TermId = 6, DoctorID = 7 },
                new UniversityCourse { Id = 38, Name = "Advanced Computer Networks", CreditHours = 3, DepartmentID = 2, TermId = 6, DoctorID = 8 },
                new UniversityCourse { Id = 39, Name = "Machine Learning", CreditHours = 3, DepartmentID = 2, TermId = 6, DoctorID = 9 },
                new UniversityCourse { Id = 40, Name = "Database Systems Advanced", CreditHours = 3, DepartmentID = 2, TermId = 6, DoctorID = 10 },
                new UniversityCourse { Id = 41, Name = "Web Technologies", CreditHours = 3, DepartmentID = 2, TermId = 6, DoctorID = 11 },
                new UniversityCourse { Id = 42, Name = "Human Computer Interaction", CreditHours = 3, DepartmentID = 2, TermId = 6, DoctorID = 12 },

                // ---- Year 3 Term 2 (Specialization - IS)
                new UniversityCourse { Id = 43, Name = "E-Business Systems", CreditHours = 3, DepartmentID = 3, TermId = 6, DoctorID = 13 },
                new UniversityCourse { Id = 44, Name = "Knowledge Management", CreditHours = 3, DepartmentID = 3, TermId = 6, DoctorID = 14 },
                new UniversityCourse { Id = 45, Name = "Advanced Systems Security", CreditHours = 3, DepartmentID = 3, TermId = 6, DoctorID = 15 },
                new UniversityCourse { Id = 46, Name = "Big Data Analytics", CreditHours = 3, DepartmentID = 3, TermId = 6, DoctorID = 1 },
                new UniversityCourse { Id = 47, Name = "Cloud Computing", CreditHours = 3, DepartmentID = 3, TermId = 6, DoctorID = 2 },
                new UniversityCourse { Id = 48, Name = "IT Project Management", CreditHours = 3, DepartmentID = 3, TermId = 6, DoctorID = 3 },

                // ---- Year 4 Term 1 (Specialization - CS)
                new UniversityCourse { Id = 49, Name = "Computer Graphics", CreditHours = 3, DepartmentID = 2, TermId = 7, DoctorID = 4 },
                new UniversityCourse { Id = 50, Name = "Cyber Security", CreditHours = 3, DepartmentID = 2, TermId = 7, DoctorID = 5 },
                new UniversityCourse { Id = 51, Name = "Natural Language Processing", CreditHours = 3, DepartmentID = 2, TermId = 7, DoctorID = 6 },
                new UniversityCourse { Id = 52, Name = "Advanced Artificial Intelligence", CreditHours = 3, DepartmentID = 2, TermId = 7, DoctorID = 7 },
                new UniversityCourse { Id = 53, Name = "Software Engineering Advanced", CreditHours = 3, DepartmentID = 2, TermId = 7, DoctorID = 8 },
                new UniversityCourse { Id = 54, Name = "Data Mining", CreditHours = 3, DepartmentID = 2, TermId = 7, DoctorID = 9 },

                // ---- Year 4 Term 1 (Specialization - IS)
                new UniversityCourse { Id = 55, Name = "Enterprise Resource Planning", CreditHours = 3, DepartmentID = 3, TermId = 7, DoctorID = 10 },
                new UniversityCourse { Id = 56, Name = "Advanced Decision Support", CreditHours = 3, DepartmentID = 3, TermId = 7, DoctorID = 11 },
                new UniversityCourse { Id = 57, Name = "Business Intelligence", CreditHours = 3, DepartmentID = 3, TermId = 7, DoctorID = 12 },
                new UniversityCourse { Id = 58, Name = "Information Systems Strategy", CreditHours = 3, DepartmentID = 3, TermId = 7, DoctorID = 13 },
                new UniversityCourse { Id = 59, Name = "Cybersecurity for IS", CreditHours = 3, DepartmentID = 3, TermId = 7, DoctorID = 14 },
                new UniversityCourse { Id = 60, Name = "Mobile Systems", CreditHours = 3, DepartmentID = 3, TermId = 7, DoctorID = 15 },

                // ---- Year 4 Term 2 (Specialization - CS)
                new UniversityCourse { Id = 61, Name = "Advanced Computer Vision", CreditHours = 3, DepartmentID = 2, TermId = 8, DoctorID = 1 },
                new UniversityCourse { Id = 62, Name = "Robotics", CreditHours = 3, DepartmentID = 2, TermId = 8, DoctorID = 2 },
                new UniversityCourse { Id = 63, Name = "Cloud Native Applications", CreditHours = 3, DepartmentID = 2, TermId = 8, DoctorID = 3 },
                new UniversityCourse { Id = 64, Name = "Capstone Project (CS)", CreditHours = 3, DepartmentID = 2, TermId = 8, DoctorID = 4 },
                new UniversityCourse { Id = 65, Name = "Advanced Data Mining", CreditHours = 3, DepartmentID = 2, TermId = 8, DoctorID = 5 },
                new UniversityCourse { Id = 66, Name = "Ethical Hacking", CreditHours = 3, DepartmentID = 2, TermId = 8, DoctorID = 6 },

                // ---- Year 4 Term 2 (Specialization - IS)
                new UniversityCourse { Id = 67, Name = "Digital Transformation", CreditHours = 3, DepartmentID = 3, TermId = 8, DoctorID = 7 },
                new UniversityCourse { Id = 68, Name = "Information Governance", CreditHours = 3, DepartmentID = 3, TermId = 8, DoctorID = 8 },
                new UniversityCourse { Id = 69, Name = "Enterprise Architecture", CreditHours = 3, DepartmentID = 3, TermId = 8, DoctorID = 9 },
                new UniversityCourse { Id = 70, Name = "Capstone Project (IS)", CreditHours = 3, DepartmentID = 3, TermId = 8, DoctorID = 10 },
                new UniversityCourse { Id = 71, Name = "Advanced Business Intelligence", CreditHours = 3, DepartmentID = 3, TermId = 8, DoctorID = 11 },
                new UniversityCourse { Id = 72, Name = "IT Governance & Compliance", CreditHours = 3, DepartmentID = 3, TermId = 8, DoctorID = 12 }
            );



            // --- AspNetUsers (Fake data) ---
            builder.Entity<ApplicationUser>().HasData(
                new ApplicationUser { Id = "inst-user-100", UserName = "ahmed@test.com", NormalizedUserName = "AHMED@TEST.COM", Email = "ahmed@test.com", NormalizedEmail = "AHMED@TEST.COM", EmailConfirmed = true, PasswordHash = "FAKE_HASH", FirstName = "Ahmed", LastName = "Kamal", FullName = "Ahmed Kamal" },
                new ApplicationUser { Id = "inst-user-101", UserName = "mona@test.com", NormalizedUserName = "MONA@TEST.COM", Email = "mona@test.com", NormalizedEmail = "MONA@TEST.COM", EmailConfirmed = true, PasswordHash = "FAKE_HASH", FirstName = "Mona", LastName = "Ali", FullName = "Mona Ali" },
                new ApplicationUser { Id = "inst-user-102", UserName = "hossam@test.com", NormalizedUserName = "HOSSAM@TEST.COM", Email = "hossam@test.com", NormalizedEmail = "HOSSAM@TEST.COM", EmailConfirmed = true, PasswordHash = "FAKE_HASH", FirstName = "Hossam", LastName = "Yehia", FullName = "Hossam Yehia" },
                new ApplicationUser { Id = "inst-user-103", UserName = "sara@test.com", NormalizedUserName = "SARA@TEST.COM", Email = "sara@test.com", NormalizedEmail = "SARA@TEST.COM", EmailConfirmed = true, PasswordHash = "FAKE_HASH", FirstName = "Sara", LastName = "Ibrahim", FullName = "Sara Ibrahim" },
                new ApplicationUser { Id = "inst-user-104", UserName = "khaled@test.com", NormalizedUserName = "KHALED@TEST.COM", Email = "khaled@test.com", NormalizedEmail = "KHALED@TEST.COM", EmailConfirmed = true, PasswordHash = "FAKE_HASH", FirstName = "Khaled", LastName = "Mostafa", FullName = "Khaled Mostafa" }
            );

            // --- Instructors ---
            builder.Entity<Instructor>().HasData(
                new Instructor { Id = 100, ApplicationUserId = "inst-user-100" },
                new Instructor { Id = 101, ApplicationUserId = "inst-user-101" },
                new Instructor { Id = 102, ApplicationUserId = "inst-user-102" },
                new Instructor { Id = 103, ApplicationUserId = "inst-user-103" },
                new Instructor { Id = 104, ApplicationUserId = "inst-user-104" }
            );

            // --- PromoCodes ---
            builder.Entity<PromoCode>().HasData(
                new PromoCode { Code = "PROMO10", DiscountPercent = 10, IsForUniversityStudentsOnly = false },
                new PromoCode { Code = "STUDENT20", DiscountPercent = 20, IsForUniversityStudentsOnly = true }
            );

            builder.Entity<OptionalCourse>().HasData(
                new OptionalCourse { Id = 300, Name = "C# Basics", Description = "Intro to C# and .NET", MainImg = "csharp.png", Price = 800, IsAvailableForUniversityStudents = true, InstructorId = 101, PromoCode = "PROMO10" },
                new OptionalCourse { Id = 301, Name = "Entity Framework Core", Description = "Learn EF Core ORM", MainImg = "efcore.png", Price = 1200, IsAvailableForUniversityStudents = true, InstructorId = 101, PromoCode = "PROMO10" },
                new OptionalCourse { Id = 302, Name = "React Fundamentals", Description = "Frontend development with React", MainImg = "react.png", Price = 1500, IsAvailableForUniversityStudents = false, InstructorId = 102, PromoCode = "PROMO10" },
                new OptionalCourse { Id = 303, Name = "Angular Crash Course", Description = "Learn Angular fast", MainImg = "angular.png", Price = 1400, IsAvailableForUniversityStudents = true, InstructorId = 102, PromoCode = "PROMO10" },
                new OptionalCourse { Id = 304, Name = "Python for Data Science", Description = "Pandas, NumPy, and basics of ML", MainImg = "python.png", Price = 1600, IsAvailableForUniversityStudents = false, InstructorId = 103, PromoCode = "PROMO10" },
                new OptionalCourse { Id = 305, Name = "Machine Learning 101", Description = "Intro to ML concepts", MainImg = "ml.png", Price = 2000, IsAvailableForUniversityStudents = true, InstructorId = 103, PromoCode = "PROMO10" },
                new OptionalCourse { Id = 306, Name = "UI/UX Advanced", Description = "Wireframes & Prototyping", MainImg = "uiux.png", Price = 1300, IsAvailableForUniversityStudents = true, InstructorId = 104, PromoCode = "PROMO10" },
                new OptionalCourse { Id = 307, Name = "Mobile Development with Flutter", Description = "Cross-platform apps", MainImg = "flutter.png", Price = 1800, IsAvailableForUniversityStudents = true, InstructorId = 104, PromoCode = "PROMO10" },
                new OptionalCourse { Id = 308, Name = "Cybersecurity Basics", Description = "Security principles and practices", MainImg = "cyber.png", Price = 2200, IsAvailableForUniversityStudents = false, InstructorId = 100, PromoCode = "PROMO10" },
                new OptionalCourse { Id = 309, Name = "Cloud with Azure", Description = "Azure fundamentals", MainImg = "azure.png", Price = 2100, IsAvailableForUniversityStudents = true, InstructorId = 100, PromoCode = "PROMO10" }
            );
            builder.Entity<AssistantCourse>().HasData(
             new AssistantCourse { AssistantId = 11, CourseId = 1 },
                 new AssistantCourse { AssistantId = 4, CourseId = 1 },

                 new AssistantCourse { AssistantId = 4, CourseId = 2 },
                 new AssistantCourse { AssistantId = 7, CourseId = 2 },

                 new AssistantCourse { AssistantId = 2, CourseId = 3 },
                 new AssistantCourse { AssistantId = 10, CourseId = 3 },

                 new AssistantCourse { AssistantId = 7, CourseId = 4 },
                 new AssistantCourse { AssistantId = 12, CourseId = 4 },

                 new AssistantCourse { AssistantId = 5, CourseId = 5 },
                 new AssistantCourse { AssistantId = 15, CourseId = 5 },

                 new AssistantCourse { AssistantId = 8, CourseId = 6 },
                 new AssistantCourse { AssistantId = 6, CourseId = 6 },

                 new AssistantCourse { AssistantId = 14, CourseId = 7 },
                 new AssistantCourse { AssistantId = 1, CourseId = 7 },

                 new AssistantCourse { AssistantId = 15, CourseId = 8 },
                 new AssistantCourse { AssistantId = 9, CourseId = 8 },

                 new AssistantCourse { AssistantId = 10, CourseId = 9 },
                 new AssistantCourse { AssistantId = 3, CourseId = 9 },

                 new AssistantCourse { AssistantId = 2, CourseId = 10 },
                 new AssistantCourse { AssistantId = 5, CourseId = 10 },

                 new AssistantCourse { AssistantId = 11, CourseId = 11 },
                 new AssistantCourse { AssistantId = 8, CourseId = 11 },

                 new AssistantCourse { AssistantId = 13, CourseId = 12 },
                 new AssistantCourse { AssistantId = 4, CourseId = 12 },

                 new AssistantCourse { AssistantId = 11, CourseId = 13 },
                 new AssistantCourse { AssistantId = 7, CourseId = 13 },

                 new AssistantCourse { AssistantId = 7, CourseId = 14 },
                 new AssistantCourse { AssistantId = 12, CourseId = 14 },

                 new AssistantCourse { AssistantId = 14, CourseId = 15 },
                 new AssistantCourse { AssistantId = 6, CourseId = 15 },

                 new AssistantCourse { AssistantId = 6, CourseId = 16 },
                 new AssistantCourse { AssistantId = 1, CourseId = 16 },

                 new AssistantCourse { AssistantId = 10, CourseId = 17 },
                 new AssistantCourse { AssistantId = 9, CourseId = 17 },

                 new AssistantCourse { AssistantId = 15, CourseId = 18 },
                 new AssistantCourse { AssistantId = 3, CourseId = 18 },

                 new AssistantCourse { AssistantId = 13, CourseId = 19 },
                 new AssistantCourse { AssistantId = 5, CourseId = 19 },

                 new AssistantCourse { AssistantId = 1, CourseId = 20 },
                 new AssistantCourse { AssistantId = 8, CourseId = 20 },

                 new AssistantCourse { AssistantId = 3, CourseId = 21 },
                 new AssistantCourse { AssistantId = 12, CourseId = 21 },

                 new AssistantCourse { AssistantId = 9, CourseId = 22 },
                 new AssistantCourse { AssistantId = 2, CourseId = 22 },

                 new AssistantCourse { AssistantId = 12, CourseId = 23 },
                 new AssistantCourse { AssistantId = 14, CourseId = 23 },

                 new AssistantCourse { AssistantId = 8, CourseId = 24 },
                 new AssistantCourse { AssistantId = 7, CourseId = 24 },

                 new AssistantCourse { AssistantId = 5, CourseId = 25 },
                 new AssistantCourse { AssistantId = 15, CourseId = 25 },

                 new AssistantCourse { AssistantId = 2, CourseId = 26 },
                 new AssistantCourse { AssistantId = 6, CourseId = 26 },

                 new AssistantCourse { AssistantId = 14, CourseId = 27 },
                 new AssistantCourse { AssistantId = 11, CourseId = 27 },

                 new AssistantCourse { AssistantId = 7, CourseId = 28 },
                 new AssistantCourse { AssistantId = 13, CourseId = 28 },

                 new AssistantCourse { AssistantId = 15, CourseId = 29 },
                 new AssistantCourse { AssistantId = 4, CourseId = 29 },

                 new AssistantCourse { AssistantId = 6, CourseId = 30 },
                 new AssistantCourse { AssistantId = 1, CourseId = 30 },

                 new AssistantCourse { AssistantId = 9, CourseId = 31 },
                 new AssistantCourse { AssistantId = 3, CourseId = 31 },

                 new AssistantCourse { AssistantId = 1, CourseId = 32 },
                 new AssistantCourse { AssistantId = 8, CourseId = 32 },

                 new AssistantCourse { AssistantId = 3, CourseId = 33 },
                 new AssistantCourse { AssistantId = 10, CourseId = 33 },

                 new AssistantCourse { AssistantId = 12, CourseId = 34 },
                 new AssistantCourse { AssistantId = 14, CourseId = 34 },

                 new AssistantCourse { AssistantId = 8, CourseId = 35 },
                 new AssistantCourse { AssistantId = 5, CourseId = 35 },

                 new AssistantCourse { AssistantId = 4, CourseId = 36 },
                 new AssistantCourse { AssistantId = 11, CourseId = 36 },

                 new AssistantCourse { AssistantId = 10, CourseId = 37 },
                 new AssistantCourse { AssistantId = 6, CourseId = 37 },

                 new AssistantCourse { AssistantId = 11, CourseId = 38 },
                 new AssistantCourse { AssistantId = 7, CourseId = 38 },

                 new AssistantCourse { AssistantId = 13, CourseId = 39 },
                 new AssistantCourse { AssistantId = 15, CourseId = 39 },

                 new AssistantCourse { AssistantId = 14, CourseId = 40 },
                 new AssistantCourse { AssistantId = 2, CourseId = 40 },

                 new AssistantCourse { AssistantId = 2, CourseId = 41 },
                 new AssistantCourse { AssistantId = 5, CourseId = 41 },

                 new AssistantCourse { AssistantId = 5, CourseId = 42 },
                 new AssistantCourse { AssistantId = 9, CourseId = 42 },

                 new AssistantCourse { AssistantId = 7, CourseId = 43 },
                 new AssistantCourse { AssistantId = 12, CourseId = 43 },

                 new AssistantCourse { AssistantId = 9, CourseId = 44 },
                 new AssistantCourse { AssistantId = 4, CourseId = 44 },

                 new AssistantCourse { AssistantId = 15, CourseId = 45 },
                 new AssistantCourse { AssistantId = 1, CourseId = 45 },

                 new AssistantCourse { AssistantId = 6, CourseId = 46 },
                 new AssistantCourse { AssistantId = 13, CourseId = 46 },

                 new AssistantCourse { AssistantId = 1, CourseId = 47 },
                 new AssistantCourse { AssistantId = 10, CourseId = 47 },

                 new AssistantCourse { AssistantId = 3, CourseId = 48 },
                 new AssistantCourse { AssistantId = 11, CourseId = 48 },

                 new AssistantCourse { AssistantId = 12, CourseId = 49 },
                 new AssistantCourse { AssistantId = 14, CourseId = 49 },

                 new AssistantCourse { AssistantId = 8, CourseId = 50 },
                 new AssistantCourse { AssistantId = 7, CourseId = 50 },

                 new AssistantCourse { AssistantId = 10, CourseId = 51 },
                 new AssistantCourse { AssistantId = 6, CourseId = 51 },

                 new AssistantCourse { AssistantId = 13, CourseId = 52 },
                 new AssistantCourse { AssistantId = 5, CourseId = 52 },

                 new AssistantCourse { AssistantId = 11, CourseId = 53 },
                 new AssistantCourse { AssistantId = 2, CourseId = 53 },

                 new AssistantCourse { AssistantId = 4, CourseId = 54 },
                 new AssistantCourse { AssistantId = 9, CourseId = 54 },

                 new AssistantCourse { AssistantId = 7, CourseId = 55 },
                 new AssistantCourse { AssistantId = 15, CourseId = 55 },

                 new AssistantCourse { AssistantId = 2, CourseId = 56 },
                 new AssistantCourse { AssistantId = 12, CourseId = 56 },

                 new AssistantCourse { AssistantId = 14, CourseId = 57 },
                 new AssistantCourse { AssistantId = 3, CourseId = 57 },

                 new AssistantCourse { AssistantId = 5, CourseId = 58 },
                 new AssistantCourse { AssistantId = 8, CourseId = 58 },

                 new AssistantCourse { AssistantId = 9, CourseId = 59 },
                 new AssistantCourse { AssistantId = 6, CourseId = 59 },

                 new AssistantCourse { AssistantId = 15, CourseId = 60 },
                 new AssistantCourse { AssistantId = 1, CourseId = 60 },

                 new AssistantCourse { AssistantId = 6, CourseId = 61 },
                 new AssistantCourse { AssistantId = 10, CourseId = 61 },

                 new AssistantCourse { AssistantId = 1, CourseId = 62 },
                 new AssistantCourse { AssistantId = 13, CourseId = 62 },

                 new AssistantCourse { AssistantId = 12, CourseId = 63 },
                 new AssistantCourse { AssistantId = 4, CourseId = 63 },

                 new AssistantCourse { AssistantId = 8, CourseId = 64 },
                 new AssistantCourse { AssistantId = 7, CourseId = 64 },

                 new AssistantCourse { AssistantId = 10, CourseId = 65 },
                 new AssistantCourse { AssistantId = 5, CourseId = 65 },

                 new AssistantCourse { AssistantId = 3, CourseId = 66 },
                 new AssistantCourse { AssistantId = 11, CourseId = 66 },

                 new AssistantCourse { AssistantId = 13, CourseId = 67 },
                 new AssistantCourse { AssistantId = 2, CourseId = 67 },

                 new AssistantCourse { AssistantId = 11, CourseId = 68 },
                 new AssistantCourse { AssistantId = 14, CourseId = 68 },

                 new AssistantCourse { AssistantId = 14, CourseId = 69 },
                 new AssistantCourse { AssistantId = 7, CourseId = 69 },

                 new AssistantCourse { AssistantId = 2, CourseId = 70 },
                 new AssistantCourse { AssistantId = 9, CourseId = 70 },

                 new AssistantCourse { AssistantId = 7, CourseId = 71 },
                 new AssistantCourse { AssistantId = 15, CourseId = 71 },

                 new AssistantCourse { AssistantId = 3, CourseId = 72 },
                 new AssistantCourse { AssistantId = 6, CourseId = 72 }
             );
            builder.Entity<DoctorAssistant>().HasData(
                // Doctor 1
                new DoctorAssistant { DoctorId = 1, AssistantId = 11 },
                new DoctorAssistant { DoctorId = 1, AssistantId = 4 },
                new DoctorAssistant { DoctorId = 1, AssistantId = 6 },
                new DoctorAssistant { DoctorId = 1, AssistantId = 1 },
                new DoctorAssistant { DoctorId = 1, AssistantId = 9 },

                // Doctor 2
                new DoctorAssistant { DoctorId = 2, AssistantId = 7 },
                new DoctorAssistant { DoctorId = 2, AssistantId = 10 },
                new DoctorAssistant { DoctorId = 2, AssistantId = 9 },
                new DoctorAssistant { DoctorId = 2, AssistantId = 13 },

                // Doctor 3
                new DoctorAssistant { DoctorId = 3, AssistantId = 2 },
                new DoctorAssistant { DoctorId = 3, AssistantId = 15 },
                new DoctorAssistant { DoctorId = 3, AssistantId = 3 },
                new DoctorAssistant { DoctorId = 3, AssistantId = 12 },

                // Doctor 4
                new DoctorAssistant { DoctorId = 4, AssistantId = 7 },
                new DoctorAssistant { DoctorId = 4, AssistantId = 13 },
                new DoctorAssistant { DoctorId = 4, AssistantId = 5 },
                new DoctorAssistant { DoctorId = 4, AssistantId = 14 },

                // Doctor 5
                new DoctorAssistant { DoctorId = 5, AssistantId = 5 },
                new DoctorAssistant { DoctorId = 5, AssistantId = 15 },
                new DoctorAssistant { DoctorId = 5, AssistantId = 1 },
                new DoctorAssistant { DoctorId = 5, AssistantId = 8 },

                // Doctor 6
                new DoctorAssistant { DoctorId = 6, AssistantId = 8 },
                new DoctorAssistant { DoctorId = 6, AssistantId = 6 },
                new DoctorAssistant { DoctorId = 6, AssistantId = 3 },
                new DoctorAssistant { DoctorId = 6, AssistantId = 12 },

                // Doctor 7
                new DoctorAssistant { DoctorId = 7, AssistantId = 14 },
                new DoctorAssistant { DoctorId = 7, AssistantId = 1 },
                new DoctorAssistant { DoctorId = 7, AssistantId = 9 },
                new DoctorAssistant { DoctorId = 7, AssistantId = 10 },

                // Doctor 8
                new DoctorAssistant { DoctorId = 8, AssistantId = 15 },
                new DoctorAssistant { DoctorId = 8, AssistantId = 9 },
                new DoctorAssistant { DoctorId = 8, AssistantId = 12 },
                new DoctorAssistant { DoctorId = 8, AssistantId = 11 },

                // Doctor 9
                new DoctorAssistant { DoctorId = 9, AssistantId = 10 },
                new DoctorAssistant { DoctorId = 9, AssistantId = 3 },
                new DoctorAssistant { DoctorId = 9, AssistantId = 8 },
                new DoctorAssistant { DoctorId = 9, AssistantId = 7 },

                // Doctor 10
                new DoctorAssistant { DoctorId = 10, AssistantId = 2 },
                new DoctorAssistant { DoctorId = 10, AssistantId = 5 },
                new DoctorAssistant { DoctorId = 10, AssistantId = 15 },
                new DoctorAssistant { DoctorId = 10, AssistantId = 14 },

                // Doctor 11
                new DoctorAssistant { DoctorId = 11, AssistantId = 11 },
                new DoctorAssistant { DoctorId = 11, AssistantId = 8 },
                new DoctorAssistant { DoctorId = 11, AssistantId = 6 },
                new DoctorAssistant { DoctorId = 11, AssistantId = 12 },

                // Doctor 12
                new DoctorAssistant { DoctorId = 12, AssistantId = 13 },
                new DoctorAssistant { DoctorId = 12, AssistantId = 4 },
                new DoctorAssistant { DoctorId = 12, AssistantId = 14 },
                new DoctorAssistant { DoctorId = 12, AssistantId = 9 },

                // Doctor 13
                new DoctorAssistant { DoctorId = 13, AssistantId = 11 },
                new DoctorAssistant { DoctorId = 13, AssistantId = 7 },
                new DoctorAssistant { DoctorId = 13, AssistantId = 13 },
                new DoctorAssistant { DoctorId = 13, AssistantId = 12 },

                // Doctor 14
                new DoctorAssistant { DoctorId = 14, AssistantId = 12 },
                new DoctorAssistant { DoctorId = 14, AssistantId = 15 },
                new DoctorAssistant { DoctorId = 14, AssistantId = 4 },
                new DoctorAssistant { DoctorId = 14, AssistantId = 6 },

                // Doctor 15
                new DoctorAssistant { DoctorId = 15, AssistantId = 14 },
                new DoctorAssistant { DoctorId = 15, AssistantId = 6 },
                new DoctorAssistant { DoctorId = 15, AssistantId = 1 },
                new DoctorAssistant { DoctorId = 15, AssistantId = 15 }
);


        }

        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings =>
                warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }
    }
}
