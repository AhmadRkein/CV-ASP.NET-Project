using I3332Proj.Models;

namespace I3332Proj.Services
{
    public class GradingService
    {
        public int CalculateGrade(CvBindingModel cv)
        {
            int grade = 0;

            foreach (var skill in cv.ProgSkills)
            {
                grade += 10;
            }

            if(cv.Gender.ToLower() == "male")
            {
                grade += 5;
            }
            else
            {
                grade += 10;
            }

            return grade;
        }
    }
}
