

namespace DataAccess.Repositories.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        IApplicationRepository Applications { get; }
        IApplicationUserRepository ApplicationUsers { get; }
        IAssistantRepository Assistants { get; }
        ICommentRepository Comments { get; }
        ICommunityPostRepository CommunityPosts { get; }
        IDoctorAssistantRepository DoctorAssistants { get; }
        IAssistantCourseRepository AssistantCourses { get; }
        IDoctorRepository Doctors { get; }
        IEnrollmentRepository Enrollments { get; }
        IFeedbackRepository Feedbacks { get; }
        IMaterialRepository Materials { get; }
        IOptionalCourseRepository OptionalCourses { get; }
        IUserOptionalCourseRepository UserOptionalCourses { get; }
        IPromoCodeRepository PromoCodes { get; }
        IStudentRepository Students { get; }
        ISubjectTaskRepository SubjectTasks { get; }
        ISupportTicketRepository SupportTickets { get; }
        ITaskSubmissionRepository TaskSubmissions { get; }
        IUniversityCourseRepository UniversityCourses { get; }
        IApplicationUserOtpRepository ApplicationUserOtps {  get; }
        IDepartmentRepository Departments {  get; }
        ITermRepository Terms { get; }
        IInstructorRepository Instructors { get; }

        IPostFileRepository PostFiles { get; }
        IPostLinkRepository PostLinks { get; }
        
        ICourseReviewRepository CourseReviews { get; }
        ICartRepository Carts { get; }
        IOrderRepository Orders { get; }
        ISessionTaskRepository SessionTasks { get; }
        ICourseSessionRepository CourseSessions { get; }


        Task<int> CompleteAsync();
    }
}
