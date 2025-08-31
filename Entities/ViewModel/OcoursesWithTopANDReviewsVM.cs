using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModel
{
    public class OcoursesWithTopANDReviewsVM
    {
        public OptionalCourse OptionalCourse { get; set; } = null!;
        public IEnumerable<OptionalCourse> TopCourses { get; set; } = new List<OptionalCourse>();
        public IEnumerable<CourseReview> Reviews { get; set; } = new List<CourseReview>();
        public double AverageRating { get; set; }
    }
}
