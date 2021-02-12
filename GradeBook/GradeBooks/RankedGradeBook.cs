using GradeBook.Enums;
using System;
using System.Linq;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        private const int minNumberStudents = 5;
        private const double twentyPercent = 0.2;

        public RankedGradeBook(string name) : base(name)
        {
            Type = GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if (Students.Count < minNumberStudents)
            {
                throw new InvalidOperationException("Ranked-grading requires a minimum of 5 students to work.");
            }

            // Number of students per grade
            int numStudentsPerGrade = (int)Math.Floor(Students.Count * twentyPercent);

            // Sorted list of average grades (desc)
            var avgGrades = Students.Select(s => s.AverageGrade).OrderByDescending(a => a);

            // Identify letter grade
            int indexAvgGrade = avgGrades.ToList().IndexOf(averageGrade);

            // If in top 20%, award 'A'
            if (indexAvgGrade < numStudentsPerGrade * 1)
            {
                return 'A';
            }
            // If in top 40%, award 'B'
            else if (indexAvgGrade < (numStudentsPerGrade * 2))
            {
                return 'B';
            }
            // If in top 60%, award 'C'
            else if (indexAvgGrade < (numStudentsPerGrade * 3))
            {
                return 'C';
            }
            // If in top 80%, award 'D'
            else if (indexAvgGrade < (numStudentsPerGrade * 4))
            {
                return 'D';
            }

            // Otherwise return 'F'
            return 'F';
        }

        public override void CalculateStatistics()
        {
            if (Students.Count < minNumberStudents)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
            }
            else
            {
                base.CalculateStatistics();
            }
        }

        public override void CalculateStudentStatistics(string name)
        {
            if (Students.Count < minNumberStudents)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
            }
            else
            {
                base.CalculateStudentStatistics(name);
            }
        }
    }
}