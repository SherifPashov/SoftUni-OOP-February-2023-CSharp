using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityCompetition.Core.Contracts;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Models;
using UniversityCompetition.Repositories;
using UniversityCompetition.Utilities.Messages;

namespace UniversityCompetition.Core
{
    public class Controller : IController
    {
        private string[] typeSubject = new string[] { "TechnicalSubject", "EconomicalSubject", "HumanitySubject" };
        private readonly SubjectRepository subjects;
        private readonly StudentRepository students;
        private readonly UniversityRepository universities;

        public Controller()
        {
            this.subjects = new SubjectRepository();
            this.students = new StudentRepository();
            this.universities = new UniversityRepository();
        }

        public string AddSubject(string subjectName, string subjectType)
        {
            if (!typeSubject.Contains(subjectType))
            {
                throw new ArgumentException(string.Format(OutputMessages.SubjectTypeNotSupported, subjectType));
            }

            if (subjects.FindByName(subjectName) != null)
            {
                throw new ArgumentException(string.Format(OutputMessages.AlreadyAddedSubject, subjectName));
            }
            else
            {
                if (subjectType == "TechnicalSubject")
                {
                    TechnicalSubject subject = new(subjects.Models.Count + 1, subjectName);
                    subjects.AddModel(subject);
                }
                else if (subjectType == "EconomicalSubject")
                {
                    EconomicalSubject subject = new(subjects.Models.Count + 1, subjectName);
                    subjects.AddModel(subject);
                }
                else if (subjectType == "HumanitySubject")
                {
                    HumanitySubject subject = new(subjects.Models.Count + 1, subjectName);
                    subjects.AddModel(subject);
                }

                return string.Format(OutputMessages.SubjectAddedSuccessfully, subjectType, subjectName, "SubjectRepository");

            }
        }

        public string AddStudent(string firstName, string lastName)
        {
            if (students.FindByName("{firstName} {lastName}") != null)
            {
                throw new ArgumentException(string.Format(OutputMessages.AlreadyAddedStudent, firstName, lastName));
            }
            else
            {
                Student st = new(this.students.Models.Count + 1, firstName, lastName);
                students.AddModel(st);
                return string.Format(OutputMessages.StudentAddedSuccessfully, firstName, lastName, "StudentRepository");
            }
        }



        public string AddUniversity(string universityName, string category, int capacity, List<string> requiredSubjects)
        {
            if (universities.FindByName(universityName) != null)
            {
                throw new ArgumentException(string.Format(OutputMessages.AlreadyAddedUniversity, universityName));
            }
            else
            {
                List<int> rs = new List<int>();
                foreach (var subName in requiredSubjects)
                {
                    rs.Add(this.subjects.FindByName(subName).Id);
                }
                University universityCurrent =
                    new University(this.universities.Models.Count + 1, universityName, category, capacity, rs);
                universities.AddModel(universityCurrent);


                return string.Format(OutputMessages.UniversityAddedSuccessfully, universityName, nameof(UniversityRepository));
            }
        }

        public string ApplyToUniversity(string studentName, string universityName)
        {
            string[] result = studentName.Split(" ");
            string firstName = result[0];
            string lastName = result[1];
            IStudent student = students.FindByName($"{firstName} {lastName}");
            IUniversity university = universities.FindByName(universityName);
            if (student == null)
            {
                throw new ArgumentException(string.Format(OutputMessages.StudentNotRegitered, firstName, lastName));
            }
            else if (university == null)
            {
                throw new ArgumentException(string.Format(OutputMessages.UniversityNotRegitered, universityName));
            }
            else if (!university.RequiredSubjects.All(x => student.CoveredExams.Any(e => e == x)))
            {
                throw new ArgumentException(string.Format(OutputMessages.StudentHasToCoverExams, studentName, universityName));
            }
            else if (student.University != null && student.University.Name == universityName)
            {
                throw new ArgumentException(string.Format(OutputMessages.StudentAlreadyJoined, firstName, lastName, universityName));
            }
            else
            {
                student.JoinUniversity(university);
                return string.Format(OutputMessages.StudentSuccessfullyJoined, firstName, lastName, universityName);
            }

        }

        public string TakeExam(int studentId, int subjectId)
        {
            if (students.FindById(studentId) == null)
            {
                throw new ArgumentException(string.Format(OutputMessages.InvalidStudentId));
            }
            else if (subjects.FindById(subjectId) == null)
            {
                throw new ArgumentException(string.Format(OutputMessages.InvalidSubjectId));
            }
            else if (students.FindById(studentId).CoveredExams.Any(e => e == subjectId))
            {
                throw new ArgumentException(string.Format(OutputMessages.StudentAlreadyCoveredThatExam,
                    students.FindById(studentId).FirstName,
                    students.FindById(studentId).LastName,
                    subjects.FindById(subjectId).Name));
            }
            else
            {
                var student = this.students.FindById(studentId);
                var subject = this.subjects.FindById(subjectId);

                student.CoverExam(subject);
                return string.Format(OutputMessages.StudentSuccessfullyCoveredExam, students.FindById(studentId).FirstName,
                    students.FindById(studentId).LastName,
                    subjects.FindById(subjectId).Name);
            }
        }

        public string UniversityReport(int universityId)
        {
            IUniversity university = this.universities.FindById(universityId);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"*** {university.Name} ***");
            sb.AppendLine($"Profile: {university.Category}");
            sb.AppendLine($"Students admitted: {this.students.Models.Where(s => s.University == university).Count()}");
            sb.AppendLine($"University vacancy: {university.Capacity - this.students.Models.Where(s => s.University == university).Count()}");

            return sb.ToString().TrimEnd();

        }
    }
}
