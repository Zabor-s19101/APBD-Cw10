using System;
using System.Collections.Generic;
using System.Linq;
using Cw10.DTOs.Requests;
using Cw10.DTOs.Responses;
using Cw10.Exceptions;
using Cw10.Models;
using Microsoft.EntityFrameworkCore;

namespace Cw10.Services {
    public class EfDbService : IDbService {
        private readonly s19101Context _context;

        public EfDbService(s19101Context context) {
            _context = context;
        }
        
        public List<Student> GetStudents() {
            return _context.Student.ToList();
        }

        public void UpdateStudent(UpdateStudentRequest student) {
            var stud = _context.Student.SingleOrDefault(s => s.IndexNumber.Equals(student.IndexNumber));
            if (stud == null) {
                throw new NotExistException("Nie ma takiego studenta");
            }
            stud.IndexNumber = student.IndexNumber;
            stud.FirstName = student.FirstName;
            stud.LastName = student.LastName;
            stud.BirthDate = student.BirthDate;
            stud.IdEnrollment = student.IdEnrollment;
            _context.SaveChanges();
        }

        public void DeleteStudent(string index) {
            var stud = _context.Student.Where(s => s.IndexNumber.Equals(index)).ToList();
            if (!stud.Any()) {
                throw new NotExistException("Nie ma takiego studenta");
            }
            _context.Remove(stud.First());
            _context.SaveChanges();
        }

        public EnrollStudentResponse EnrollStudent(EnrollStudentRequest request) {
            EnrollStudentResponse response = null;
            if (_context.Student.SingleOrDefault(s => s.IndexNumber.Equals(request.IndexNumber)) != null) {
                throw new ExistException("Taki student już istnieje");
            }
            var idStudy = _context.Studies.FirstOrDefault(s => s.Name.Equals(request.Studies))?.IdStudy;
            if (idStudy == null) {
                throw new NotExistException("Takie studia nie istnieją");
            }
            var idEnrollment = _context.Enrollment.FirstOrDefault(e => e.IdStudy == idStudy && e.Semester == 1)?.IdEnrollment;
            if (idEnrollment == null) {
                idEnrollment = _context.Enrollment.Max(e => e.IdEnrollment) + 1;
                _context.Enrollment.Add(new Enrollment {
                    IdEnrollment = idEnrollment.Value,
                    IdStudy = idStudy.Value,
                    Semester = 1,
                    StartDate = DateTime.Now
                });
            }
            _context.Student.Add(new Student {
                IndexNumber = request.IndexNumber,
                FirstName = request.FirstName,
                LastName = request.LastName,
                BirthDate = request.BirthDate,
                IdEnrollment = idEnrollment.Value
            });
            _context.SaveChanges();

            response = new EnrollStudentResponse {
                IdEnrollment = idEnrollment.Value,
                Semester = 1,
                Study = request.Studies,
                StartDate = _context.Enrollment.Single(e => e.IdEnrollment == (int)idEnrollment).StartDate.ToString("yyyy-MM-dd")
            };
            return response;
        }

        public void EnrollPromotions(EnrollPromotionsRequest request) {
            _context.Database.ExecuteSqlInterpolated($"exec enrollpromotions {request.Studies}, {request.Semester}");
        }
    }
}