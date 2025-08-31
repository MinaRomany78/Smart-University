using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "FullName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "inst-user-100", 0, null, "350ace40-45de-4bae-b10d-eb00975637d5", "ahmed@test.com", true, "Ahmed", "Ahmed Kamal", "Kamal", false, null, "AHMED@TEST.COM", "AHMED@TEST.COM", "FAKE_HASH", null, false, "51cc866c-be23-430e-a797-e24d7c8616b6", false, "ahmed@test.com" },
                    { "inst-user-101", 0, null, "f7cc90c1-bc52-469e-b22e-5b7f81dac6fe", "mona@test.com", true, "Mona", "Mona Ali", "Ali", false, null, "MONA@TEST.COM", "MONA@TEST.COM", "FAKE_HASH", null, false, "b89b3ad2-e0b1-4c7e-885a-416faed67c37", false, "mona@test.com" },
                    { "inst-user-102", 0, null, "3909ac12-06f6-418a-9877-a380b76f2142", "hossam@test.com", true, "Hossam", "Hossam Yehia", "Yehia", false, null, "HOSSAM@TEST.COM", "HOSSAM@TEST.COM", "FAKE_HASH", null, false, "677de183-dca7-49eb-b8b1-e646b4765321", false, "hossam@test.com" },
                    { "inst-user-103", 0, null, "e82459d8-a15d-4c9f-af80-0413bb7b0677", "sara@test.com", true, "Sara", "Sara Ibrahim", "Ibrahim", false, null, "SARA@TEST.COM", "SARA@TEST.COM", "FAKE_HASH", null, false, "586f17e2-bfac-404a-80f2-94eaba4e8e53", false, "sara@test.com" },
                    { "inst-user-104", 0, null, "64de5830-1396-429f-90b7-b207906b33e5", "khaled@test.com", true, "Khaled", "Khaled Mostafa", "Mostafa", false, null, "KHALED@TEST.COM", "KHALED@TEST.COM", "FAKE_HASH", null, false, "19df7176-b69d-4c5b-b681-1549a3fd5e19", false, "khaled@test.com" }
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "General" },
                    { 2, "Computer Science" },
                    { 3, "Information Systems" }
                });

            migrationBuilder.InsertData(
                table: "DoctorAssistants",
                columns: new[] { "AssistantId", "DoctorId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 4, 1 },
                    { 6, 1 },
                    { 9, 1 },
                    { 11, 1 },
                    { 7, 2 },
                    { 9, 2 },
                    { 10, 2 },
                    { 13, 2 },
                    { 2, 3 },
                    { 3, 3 },
                    { 12, 3 },
                    { 15, 3 },
                    { 5, 4 },
                    { 7, 4 },
                    { 13, 4 },
                    { 14, 4 },
                    { 1, 5 },
                    { 5, 5 },
                    { 8, 5 },
                    { 15, 5 },
                    { 3, 6 },
                    { 6, 6 },
                    { 8, 6 },
                    { 12, 6 },
                    { 1, 7 },
                    { 9, 7 },
                    { 10, 7 },
                    { 14, 7 },
                    { 9, 8 },
                    { 11, 8 },
                    { 12, 8 },
                    { 15, 8 },
                    { 3, 9 },
                    { 7, 9 },
                    { 8, 9 },
                    { 10, 9 },
                    { 2, 10 },
                    { 5, 10 },
                    { 14, 10 },
                    { 15, 10 },
                    { 6, 11 },
                    { 8, 11 },
                    { 11, 11 },
                    { 12, 11 },
                    { 4, 12 },
                    { 9, 12 },
                    { 13, 12 },
                    { 14, 12 },
                    { 7, 13 },
                    { 11, 13 },
                    { 12, 13 },
                    { 13, 13 },
                    { 4, 14 },
                    { 6, 14 },
                    { 12, 14 },
                    { 15, 14 },
                    { 1, 15 },
                    { 6, 15 },
                    { 14, 15 },
                    { 15, 15 }
                });

            migrationBuilder.InsertData(
                table: "PromoCodes",
                columns: new[] { "Code", "DiscountPercent", "IsForUniversityStudentsOnly" },
                values: new object[,]
                {
                    { "PROMO10", 10m, false },
                    { "STUDENT20", 20m, true }
                });

            migrationBuilder.InsertData(
                table: "Terms",
                columns: new[] { "Id", "TermNumber", "Year" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 1 },
                    { 3, 1, 2 },
                    { 4, 2, 2 },
                    { 5, 1, 3 },
                    { 6, 2, 3 },
                    { 7, 1, 4 },
                    { 8, 2, 4 }
                });

            migrationBuilder.InsertData(
                table: "Instructors",
                columns: new[] { "Id", "ApplicationUserId" },
                values: new object[,]
                {
                    { 100, "inst-user-100" },
                    { 101, "inst-user-101" },
                    { 102, "inst-user-102" },
                    { 103, "inst-user-103" },
                    { 104, "inst-user-104" }
                });

            migrationBuilder.InsertData(
                table: "UniversityCourses",
                columns: new[] { "Id", "CreditHours", "DepartmentID", "Description", "DoctorID", "Name", "TermId" },
                values: new object[,]
                {
                    { 1, 3, 1, "", 1, "Mathematics 1", 1 },
                    { 2, 3, 1, "", 2, "Programming Basics", 1 },
                    { 3, 3, 1, "", 3, "Physics 1", 1 },
                    { 4, 3, 1, "", 4, "English 1", 1 },
                    { 5, 3, 1, "", 5, "Introduction to IT", 1 },
                    { 6, 3, 1, "", 6, "Critical Thinking", 1 },
                    { 7, 3, 1, "", 7, "Mathematics 2", 2 },
                    { 8, 3, 1, "", 8, "Object Oriented Programming", 2 },
                    { 9, 3, 1, "", 9, "Physics 2", 2 },
                    { 10, 3, 1, "", 10, "English 2", 2 },
                    { 11, 3, 1, "", 11, "Introduction to Database", 2 },
                    { 12, 3, 1, "", 12, "Communication Skills", 2 },
                    { 13, 3, 1, "", 13, "Mathematics 3", 3 },
                    { 14, 3, 1, "", 14, "Data Structures", 3 },
                    { 15, 3, 1, "", 15, "Computer Organization", 3 },
                    { 16, 3, 1, "", 1, "Probability & Statistics", 3 },
                    { 17, 3, 1, "", 2, "Operating Systems Basics", 3 },
                    { 18, 3, 1, "", 3, "Ethics", 3 },
                    { 19, 3, 1, "", 4, "Mathematics 4", 4 },
                    { 20, 3, 1, "", 5, "Algorithms", 4 },
                    { 21, 3, 1, "", 6, "Digital Logic", 4 },
                    { 22, 3, 1, "", 7, "Software Engineering Basics", 4 },
                    { 23, 3, 1, "", 8, "Database Systems", 4 },
                    { 24, 3, 1, "", 9, "Technical Writing", 4 },
                    { 25, 3, 2, "", 10, "Advanced Algorithms", 5 },
                    { 26, 3, 2, "", 11, "Theory of Computation", 5 },
                    { 27, 3, 2, "", 12, "Operating Systems", 5 },
                    { 28, 3, 2, "", 13, "Computer Networks", 5 },
                    { 29, 3, 2, "", 14, "Artificial Intelligence", 5 },
                    { 30, 3, 2, "", 15, "Compiler Design", 5 },
                    { 31, 3, 3, "", 1, "Information Systems Analysis", 5 },
                    { 32, 3, 3, "", 2, "Business Process Management", 5 },
                    { 33, 3, 3, "", 3, "Database Administration", 5 },
                    { 34, 3, 3, "", 4, "Enterprise Systems", 5 },
                    { 35, 3, 3, "", 5, "Systems Security", 5 },
                    { 36, 3, 3, "", 6, "Decision Support Systems", 5 },
                    { 37, 3, 2, "", 7, "Parallel Computing", 6 },
                    { 38, 3, 2, "", 8, "Advanced Computer Networks", 6 },
                    { 39, 3, 2, "", 9, "Machine Learning", 6 },
                    { 40, 3, 2, "", 10, "Database Systems Advanced", 6 },
                    { 41, 3, 2, "", 11, "Web Technologies", 6 },
                    { 42, 3, 2, "", 12, "Human Computer Interaction", 6 },
                    { 43, 3, 3, "", 13, "E-Business Systems", 6 },
                    { 44, 3, 3, "", 14, "Knowledge Management", 6 },
                    { 45, 3, 3, "", 15, "Advanced Systems Security", 6 },
                    { 46, 3, 3, "", 1, "Big Data Analytics", 6 },
                    { 47, 3, 3, "", 2, "Cloud Computing", 6 },
                    { 48, 3, 3, "", 3, "IT Project Management", 6 },
                    { 49, 3, 2, "", 4, "Computer Graphics", 7 },
                    { 50, 3, 2, "", 5, "Cyber Security", 7 },
                    { 51, 3, 2, "", 6, "Natural Language Processing", 7 },
                    { 52, 3, 2, "", 7, "Advanced Artificial Intelligence", 7 },
                    { 53, 3, 2, "", 8, "Software Engineering Advanced", 7 },
                    { 54, 3, 2, "", 9, "Data Mining", 7 },
                    { 55, 3, 3, "", 10, "Enterprise Resource Planning", 7 },
                    { 56, 3, 3, "", 11, "Advanced Decision Support", 7 },
                    { 57, 3, 3, "", 12, "Business Intelligence", 7 },
                    { 58, 3, 3, "", 13, "Information Systems Strategy", 7 },
                    { 59, 3, 3, "", 14, "Cybersecurity for IS", 7 },
                    { 60, 3, 3, "", 15, "Mobile Systems", 7 },
                    { 61, 3, 2, "", 1, "Advanced Computer Vision", 8 },
                    { 62, 3, 2, "", 2, "Robotics", 8 },
                    { 63, 3, 2, "", 3, "Cloud Native Applications", 8 },
                    { 64, 3, 2, "", 4, "Capstone Project (CS)", 8 },
                    { 65, 3, 2, "", 5, "Advanced Data Mining", 8 },
                    { 66, 3, 2, "", 6, "Ethical Hacking", 8 },
                    { 67, 3, 3, "", 7, "Digital Transformation", 8 },
                    { 68, 3, 3, "", 8, "Information Governance", 8 },
                    { 69, 3, 3, "", 9, "Enterprise Architecture", 8 },
                    { 70, 3, 3, "", 10, "Capstone Project (IS)", 8 },
                    { 71, 3, 3, "", 11, "Advanced Business Intelligence", 8 },
                    { 72, 3, 3, "", 12, "IT Governance & Compliance", 8 }
                });

            migrationBuilder.InsertData(
                table: "AssistantCourses",
                columns: new[] { "AssistantId", "CourseId" },
                values: new object[,]
                {
                    { 1, 7 },
                    { 1, 16 },
                    { 1, 20 },
                    { 1, 30 },
                    { 1, 32 },
                    { 1, 45 },
                    { 1, 47 },
                    { 1, 60 },
                    { 1, 62 },
                    { 2, 3 },
                    { 2, 10 },
                    { 2, 22 },
                    { 2, 26 },
                    { 2, 40 },
                    { 2, 41 },
                    { 2, 53 },
                    { 2, 56 },
                    { 2, 67 },
                    { 2, 70 },
                    { 3, 9 },
                    { 3, 18 },
                    { 3, 21 },
                    { 3, 31 },
                    { 3, 33 },
                    { 3, 48 },
                    { 3, 57 },
                    { 3, 66 },
                    { 3, 72 },
                    { 4, 1 },
                    { 4, 2 },
                    { 4, 12 },
                    { 4, 29 },
                    { 4, 36 },
                    { 4, 44 },
                    { 4, 54 },
                    { 4, 63 },
                    { 5, 5 },
                    { 5, 10 },
                    { 5, 19 },
                    { 5, 25 },
                    { 5, 35 },
                    { 5, 41 },
                    { 5, 42 },
                    { 5, 52 },
                    { 5, 58 },
                    { 5, 65 },
                    { 6, 6 },
                    { 6, 15 },
                    { 6, 16 },
                    { 6, 26 },
                    { 6, 30 },
                    { 6, 37 },
                    { 6, 46 },
                    { 6, 51 },
                    { 6, 59 },
                    { 6, 61 },
                    { 6, 72 },
                    { 7, 2 },
                    { 7, 4 },
                    { 7, 13 },
                    { 7, 14 },
                    { 7, 24 },
                    { 7, 28 },
                    { 7, 38 },
                    { 7, 43 },
                    { 7, 50 },
                    { 7, 55 },
                    { 7, 64 },
                    { 7, 69 },
                    { 7, 71 },
                    { 8, 6 },
                    { 8, 11 },
                    { 8, 20 },
                    { 8, 24 },
                    { 8, 32 },
                    { 8, 35 },
                    { 8, 50 },
                    { 8, 58 },
                    { 8, 64 },
                    { 9, 8 },
                    { 9, 17 },
                    { 9, 22 },
                    { 9, 31 },
                    { 9, 42 },
                    { 9, 44 },
                    { 9, 54 },
                    { 9, 59 },
                    { 9, 70 },
                    { 10, 3 },
                    { 10, 9 },
                    { 10, 17 },
                    { 10, 33 },
                    { 10, 37 },
                    { 10, 47 },
                    { 10, 51 },
                    { 10, 61 },
                    { 10, 65 },
                    { 11, 1 },
                    { 11, 11 },
                    { 11, 13 },
                    { 11, 27 },
                    { 11, 36 },
                    { 11, 38 },
                    { 11, 48 },
                    { 11, 53 },
                    { 11, 66 },
                    { 11, 68 },
                    { 12, 4 },
                    { 12, 14 },
                    { 12, 21 },
                    { 12, 23 },
                    { 12, 34 },
                    { 12, 43 },
                    { 12, 49 },
                    { 12, 56 },
                    { 12, 63 },
                    { 13, 12 },
                    { 13, 19 },
                    { 13, 28 },
                    { 13, 39 },
                    { 13, 46 },
                    { 13, 52 },
                    { 13, 62 },
                    { 13, 67 },
                    { 14, 7 },
                    { 14, 15 },
                    { 14, 23 },
                    { 14, 27 },
                    { 14, 34 },
                    { 14, 40 },
                    { 14, 49 },
                    { 14, 57 },
                    { 14, 68 },
                    { 14, 69 },
                    { 15, 5 },
                    { 15, 8 },
                    { 15, 18 },
                    { 15, 25 },
                    { 15, 29 },
                    { 15, 39 },
                    { 15, 45 },
                    { 15, 55 },
                    { 15, 60 },
                    { 15, 71 }
                });

            migrationBuilder.InsertData(
                table: "OptionalCourses",
                columns: new[] { "Id", "Description", "InstructorId", "IsAvailableForUniversityStudents", "MainImg", "Name", "Price", "PromoCode", "Traffic" },
                values: new object[,]
                {
                    { 300, "Intro to C# and .NET", 101, true, "csharp.png", "C# Basics", 800m, "PROMO10", 0 },
                    { 301, "Learn EF Core ORM", 101, true, "efcore.png", "Entity Framework Core", 1200m, "PROMO10", 0 },
                    { 302, "Frontend development with React", 102, false, "react.png", "React Fundamentals", 1500m, "PROMO10", 0 },
                    { 303, "Learn Angular fast", 102, true, "angular.png", "Angular Crash Course", 1400m, "PROMO10", 0 },
                    { 304, "Pandas, NumPy, and basics of ML", 103, false, "python.png", "Python for Data Science", 1600m, "PROMO10", 0 },
                    { 305, "Intro to ML concepts", 103, true, "ml.png", "Machine Learning 101", 2000m, "PROMO10", 0 },
                    { 306, "Wireframes & Prototyping", 104, true, "uiux.png", "UI/UX Advanced", 1300m, "PROMO10", 0 },
                    { 307, "Cross-platform apps", 104, true, "flutter.png", "Mobile Development with Flutter", 1800m, "PROMO10", 0 },
                    { 308, "Security principles and practices", 100, false, "cyber.png", "Cybersecurity Basics", 2200m, "PROMO10", 0 },
                    { 309, "Azure fundamentals", 100, true, "azure.png", "Cloud with Azure", 2100m, "PROMO10", 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 1, 7 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 1, 16 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 1, 20 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 1, 30 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 1, 32 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 1, 45 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 1, 47 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 1, 60 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 1, 62 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 2, 10 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 2, 22 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 2, 26 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 2, 40 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 2, 41 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 2, 53 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 2, 56 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 2, 67 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 2, 70 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 3, 9 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 3, 18 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 3, 21 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 3, 31 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 3, 33 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 3, 48 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 3, 57 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 3, 66 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 3, 72 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 4, 1 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 4, 2 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 4, 12 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 4, 29 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 4, 36 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 4, 44 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 4, 54 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 4, 63 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 5, 5 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 5, 10 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 5, 19 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 5, 25 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 5, 35 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 5, 41 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 5, 42 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 5, 52 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 5, 58 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 5, 65 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 6, 6 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 6, 15 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 6, 16 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 6, 26 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 6, 30 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 6, 37 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 6, 46 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 6, 51 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 6, 59 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 6, 61 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 6, 72 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 7, 2 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 7, 4 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 7, 13 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 7, 14 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 7, 24 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 7, 28 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 7, 38 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 7, 43 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 7, 50 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 7, 55 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 7, 64 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 7, 69 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 7, 71 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 8, 6 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 8, 11 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 8, 20 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 8, 24 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 8, 32 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 8, 35 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 8, 50 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 8, 58 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 8, 64 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 9, 8 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 9, 17 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 9, 22 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 9, 31 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 9, 42 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 9, 44 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 9, 54 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 9, 59 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 9, 70 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 10, 3 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 10, 9 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 10, 17 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 10, 33 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 10, 37 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 10, 47 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 10, 51 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 10, 61 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 10, 65 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 11, 1 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 11, 11 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 11, 13 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 11, 27 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 11, 36 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 11, 38 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 11, 48 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 11, 53 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 11, 66 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 11, 68 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 12, 4 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 12, 14 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 12, 21 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 12, 23 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 12, 34 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 12, 43 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 12, 49 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 12, 56 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 12, 63 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 13, 12 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 13, 19 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 13, 28 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 13, 39 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 13, 46 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 13, 52 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 13, 62 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 13, 67 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 14, 7 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 14, 15 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 14, 23 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 14, 27 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 14, 34 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 14, 40 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 14, 49 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 14, 57 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 14, 68 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 14, 69 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 15, 5 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 15, 8 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 15, 18 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 15, 25 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 15, 29 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 15, 39 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 15, 45 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 15, 55 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 15, 60 });

            migrationBuilder.DeleteData(
                table: "AssistantCourses",
                keyColumns: new[] { "AssistantId", "CourseId" },
                keyValues: new object[] { 15, 71 });

            migrationBuilder.DeleteData(
                table: "DoctorAssistants",
                keyColumns: new[] { "AssistantId", "DoctorId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "DoctorAssistants",
                keyColumns: new[] { "AssistantId", "DoctorId" },
                keyValues: new object[] { 4, 1 });

            migrationBuilder.DeleteData(
                table: "DoctorAssistants",
                keyColumns: new[] { "AssistantId", "DoctorId" },
                keyValues: new object[] { 6, 1 });

            migrationBuilder.DeleteData(
                table: "DoctorAssistants",
                keyColumns: new[] { "AssistantId", "DoctorId" },
                keyValues: new object[] { 9, 1 });

            migrationBuilder.DeleteData(
                table: "DoctorAssistants",
                keyColumns: new[] { "AssistantId", "DoctorId" },
                keyValues: new object[] { 11, 1 });

            migrationBuilder.DeleteData(
                table: "DoctorAssistants",
                keyColumns: new[] { "AssistantId", "DoctorId" },
                keyValues: new object[] { 7, 2 });

            migrationBuilder.DeleteData(
                table: "DoctorAssistants",
                keyColumns: new[] { "AssistantId", "DoctorId" },
                keyValues: new object[] { 9, 2 });

            migrationBuilder.DeleteData(
                table: "DoctorAssistants",
                keyColumns: new[] { "AssistantId", "DoctorId" },
                keyValues: new object[] { 10, 2 });

            migrationBuilder.DeleteData(
                table: "DoctorAssistants",
                keyColumns: new[] { "AssistantId", "DoctorId" },
                keyValues: new object[] { 13, 2 });

            migrationBuilder.DeleteData(
                table: "DoctorAssistants",
                keyColumns: new[] { "AssistantId", "DoctorId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "DoctorAssistants",
                keyColumns: new[] { "AssistantId", "DoctorId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "DoctorAssistants",
                keyColumns: new[] { "AssistantId", "DoctorId" },
                keyValues: new object[] { 12, 3 });

            migrationBuilder.DeleteData(
                table: "DoctorAssistants",
                keyColumns: new[] { "AssistantId", "DoctorId" },
                keyValues: new object[] { 15, 3 });

            migrationBuilder.DeleteData(
                table: "DoctorAssistants",
                keyColumns: new[] { "AssistantId", "DoctorId" },
                keyValues: new object[] { 5, 4 });

            migrationBuilder.DeleteData(
                table: "DoctorAssistants",
                keyColumns: new[] { "AssistantId", "DoctorId" },
                keyValues: new object[] { 7, 4 });

            migrationBuilder.DeleteData(
                table: "DoctorAssistants",
                keyColumns: new[] { "AssistantId", "DoctorId" },
                keyValues: new object[] { 13, 4 });

            migrationBuilder.DeleteData(
                table: "DoctorAssistants",
                keyColumns: new[] { "AssistantId", "DoctorId" },
                keyValues: new object[] { 14, 4 });

            migrationBuilder.DeleteData(
                table: "DoctorAssistants",
                keyColumns: new[] { "AssistantId", "DoctorId" },
                keyValues: new object[] { 1, 5 });

            migrationBuilder.DeleteData(
                table: "DoctorAssistants",
                keyColumns: new[] { "AssistantId", "DoctorId" },
                keyValues: new object[] { 5, 5 });

            migrationBuilder.DeleteData(
                table: "DoctorAssistants",
                keyColumns: new[] { "AssistantId", "DoctorId" },
                keyValues: new object[] { 8, 5 });

            migrationBuilder.DeleteData(
                table: "DoctorAssistants",
                keyColumns: new[] { "AssistantId", "DoctorId" },
                keyValues: new object[] { 15, 5 });

            migrationBuilder.DeleteData(
                table: "DoctorAssistants",
                keyColumns: new[] { "AssistantId", "DoctorId" },
                keyValues: new object[] { 3, 6 });

            migrationBuilder.DeleteData(
                table: "DoctorAssistants",
                keyColumns: new[] { "AssistantId", "DoctorId" },
                keyValues: new object[] { 6, 6 });

            migrationBuilder.DeleteData(
                table: "DoctorAssistants",
                keyColumns: new[] { "AssistantId", "DoctorId" },
                keyValues: new object[] { 8, 6 });

            migrationBuilder.DeleteData(
                table: "DoctorAssistants",
                keyColumns: new[] { "AssistantId", "DoctorId" },
                keyValues: new object[] { 12, 6 });

            migrationBuilder.DeleteData(
                table: "DoctorAssistants",
                keyColumns: new[] { "AssistantId", "DoctorId" },
                keyValues: new object[] { 1, 7 });

            migrationBuilder.DeleteData(
                table: "DoctorAssistants",
                keyColumns: new[] { "AssistantId", "DoctorId" },
                keyValues: new object[] { 9, 7 });

            migrationBuilder.DeleteData(
                table: "DoctorAssistants",
                keyColumns: new[] { "AssistantId", "DoctorId" },
                keyValues: new object[] { 10, 7 });

            migrationBuilder.DeleteData(
                table: "DoctorAssistants",
                keyColumns: new[] { "AssistantId", "DoctorId" },
                keyValues: new object[] { 14, 7 });

            migrationBuilder.DeleteData(
                table: "DoctorAssistants",
                keyColumns: new[] { "AssistantId", "DoctorId" },
                keyValues: new object[] { 9, 8 });

            migrationBuilder.DeleteData(
                table: "DoctorAssistants",
                keyColumns: new[] { "AssistantId", "DoctorId" },
                keyValues: new object[] { 11, 8 });

            migrationBuilder.DeleteData(
                table: "DoctorAssistants",
                keyColumns: new[] { "AssistantId", "DoctorId" },
                keyValues: new object[] { 12, 8 });

            migrationBuilder.DeleteData(
                table: "DoctorAssistants",
                keyColumns: new[] { "AssistantId", "DoctorId" },
                keyValues: new object[] { 15, 8 });

            migrationBuilder.DeleteData(
                table: "DoctorAssistants",
                keyColumns: new[] { "AssistantId", "DoctorId" },
                keyValues: new object[] { 3, 9 });

            migrationBuilder.DeleteData(
                table: "DoctorAssistants",
                keyColumns: new[] { "AssistantId", "DoctorId" },
                keyValues: new object[] { 7, 9 });

            migrationBuilder.DeleteData(
                table: "DoctorAssistants",
                keyColumns: new[] { "AssistantId", "DoctorId" },
                keyValues: new object[] { 8, 9 });

            migrationBuilder.DeleteData(
                table: "DoctorAssistants",
                keyColumns: new[] { "AssistantId", "DoctorId" },
                keyValues: new object[] { 10, 9 });

            migrationBuilder.DeleteData(
                table: "DoctorAssistants",
                keyColumns: new[] { "AssistantId", "DoctorId" },
                keyValues: new object[] { 2, 10 });

            migrationBuilder.DeleteData(
                table: "DoctorAssistants",
                keyColumns: new[] { "AssistantId", "DoctorId" },
                keyValues: new object[] { 5, 10 });

            migrationBuilder.DeleteData(
                table: "DoctorAssistants",
                keyColumns: new[] { "AssistantId", "DoctorId" },
                keyValues: new object[] { 14, 10 });

            migrationBuilder.DeleteData(
                table: "DoctorAssistants",
                keyColumns: new[] { "AssistantId", "DoctorId" },
                keyValues: new object[] { 15, 10 });

            migrationBuilder.DeleteData(
                table: "DoctorAssistants",
                keyColumns: new[] { "AssistantId", "DoctorId" },
                keyValues: new object[] { 6, 11 });

            migrationBuilder.DeleteData(
                table: "DoctorAssistants",
                keyColumns: new[] { "AssistantId", "DoctorId" },
                keyValues: new object[] { 8, 11 });

            migrationBuilder.DeleteData(
                table: "DoctorAssistants",
                keyColumns: new[] { "AssistantId", "DoctorId" },
                keyValues: new object[] { 11, 11 });

            migrationBuilder.DeleteData(
                table: "DoctorAssistants",
                keyColumns: new[] { "AssistantId", "DoctorId" },
                keyValues: new object[] { 12, 11 });

            migrationBuilder.DeleteData(
                table: "DoctorAssistants",
                keyColumns: new[] { "AssistantId", "DoctorId" },
                keyValues: new object[] { 4, 12 });

            migrationBuilder.DeleteData(
                table: "DoctorAssistants",
                keyColumns: new[] { "AssistantId", "DoctorId" },
                keyValues: new object[] { 9, 12 });

            migrationBuilder.DeleteData(
                table: "DoctorAssistants",
                keyColumns: new[] { "AssistantId", "DoctorId" },
                keyValues: new object[] { 13, 12 });

            migrationBuilder.DeleteData(
                table: "DoctorAssistants",
                keyColumns: new[] { "AssistantId", "DoctorId" },
                keyValues: new object[] { 14, 12 });

            migrationBuilder.DeleteData(
                table: "DoctorAssistants",
                keyColumns: new[] { "AssistantId", "DoctorId" },
                keyValues: new object[] { 7, 13 });

            migrationBuilder.DeleteData(
                table: "DoctorAssistants",
                keyColumns: new[] { "AssistantId", "DoctorId" },
                keyValues: new object[] { 11, 13 });

            migrationBuilder.DeleteData(
                table: "DoctorAssistants",
                keyColumns: new[] { "AssistantId", "DoctorId" },
                keyValues: new object[] { 12, 13 });

            migrationBuilder.DeleteData(
                table: "DoctorAssistants",
                keyColumns: new[] { "AssistantId", "DoctorId" },
                keyValues: new object[] { 13, 13 });

            migrationBuilder.DeleteData(
                table: "DoctorAssistants",
                keyColumns: new[] { "AssistantId", "DoctorId" },
                keyValues: new object[] { 4, 14 });

            migrationBuilder.DeleteData(
                table: "DoctorAssistants",
                keyColumns: new[] { "AssistantId", "DoctorId" },
                keyValues: new object[] { 6, 14 });

            migrationBuilder.DeleteData(
                table: "DoctorAssistants",
                keyColumns: new[] { "AssistantId", "DoctorId" },
                keyValues: new object[] { 12, 14 });

            migrationBuilder.DeleteData(
                table: "DoctorAssistants",
                keyColumns: new[] { "AssistantId", "DoctorId" },
                keyValues: new object[] { 15, 14 });

            migrationBuilder.DeleteData(
                table: "DoctorAssistants",
                keyColumns: new[] { "AssistantId", "DoctorId" },
                keyValues: new object[] { 1, 15 });

            migrationBuilder.DeleteData(
                table: "DoctorAssistants",
                keyColumns: new[] { "AssistantId", "DoctorId" },
                keyValues: new object[] { 6, 15 });

            migrationBuilder.DeleteData(
                table: "DoctorAssistants",
                keyColumns: new[] { "AssistantId", "DoctorId" },
                keyValues: new object[] { 14, 15 });

            migrationBuilder.DeleteData(
                table: "DoctorAssistants",
                keyColumns: new[] { "AssistantId", "DoctorId" },
                keyValues: new object[] { 15, 15 });

            migrationBuilder.DeleteData(
                table: "OptionalCourses",
                keyColumn: "Id",
                keyValue: 300);

            migrationBuilder.DeleteData(
                table: "OptionalCourses",
                keyColumn: "Id",
                keyValue: 301);

            migrationBuilder.DeleteData(
                table: "OptionalCourses",
                keyColumn: "Id",
                keyValue: 302);

            migrationBuilder.DeleteData(
                table: "OptionalCourses",
                keyColumn: "Id",
                keyValue: 303);

            migrationBuilder.DeleteData(
                table: "OptionalCourses",
                keyColumn: "Id",
                keyValue: 304);

            migrationBuilder.DeleteData(
                table: "OptionalCourses",
                keyColumn: "Id",
                keyValue: 305);

            migrationBuilder.DeleteData(
                table: "OptionalCourses",
                keyColumn: "Id",
                keyValue: 306);

            migrationBuilder.DeleteData(
                table: "OptionalCourses",
                keyColumn: "Id",
                keyValue: 307);

            migrationBuilder.DeleteData(
                table: "OptionalCourses",
                keyColumn: "Id",
                keyValue: 308);

            migrationBuilder.DeleteData(
                table: "OptionalCourses",
                keyColumn: "Id",
                keyValue: 309);

            migrationBuilder.DeleteData(
                table: "PromoCodes",
                keyColumn: "Code",
                keyValue: "STUDENT20");

            migrationBuilder.DeleteData(
                table: "Instructors",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "Instructors",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Instructors",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "Instructors",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "Instructors",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "PromoCodes",
                keyColumn: "Code",
                keyValue: "PROMO10");

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "UniversityCourses",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-100");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-101");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-102");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-103");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-104");

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Terms",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Terms",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Terms",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Terms",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Terms",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Terms",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Terms",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Terms",
                keyColumn: "Id",
                keyValue: 8);
        }
    }
}
