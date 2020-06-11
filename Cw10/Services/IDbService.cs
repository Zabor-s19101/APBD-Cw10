using System.Collections.Generic;
using Cw10.DTOs.Requests;
using Cw10.DTOs.Responses;
using Cw10.Models;

namespace Cw10.Services {
    public interface IDbService {
        List<Student> GetStudents();
        void UpdateStudent(UpdateStudentRequest student);

        void DeleteStudent(string index);

        EnrollStudentResponse EnrollStudent(EnrollStudentRequest request);

        void EnrollPromotions(EnrollPromotionsRequest request);
    }
}