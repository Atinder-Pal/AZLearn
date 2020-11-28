﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using AZLearn.Models;
using AZLearn.Models.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS;
using Microsoft.Data;

namespace AZLearn.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApplicationController : Controller
    {
        #region CohortController

        #region /application/CreateCohort

        [HttpPost(nameof(CreateCohort))]
        public ActionResult CreateCohort(string name, string capacity, string city,
            string modeOfTeaching, string startDate, string endDate)

        {
            ActionResult result;
            try
            {
                CohortController.CreateCohort(name, capacity, city,
                    modeOfTeaching, startDate, endDate);
                result = StatusCode(200, "Successfully Created the Cohort");
            }
            catch (ValidationException e)
            {
                var error = "Error(s) During CreateCohort: " +
                            e.ValidationExceptions.Select(x => x.Message)
                                .Aggregate((x, y) => x + ", " + y);

                result = BadRequest(error);
            }
            catch (Exception e)
            {
                result = StatusCode(500,
                    "Unexpected server/database error occurred. System error message(s): " + e.Message);
            }

            return result;
        }

        #endregion

        #region /application/UpdateCohort

        [HttpPatch(nameof(UpdateCohort))]
        public ActionResult UpdateCohort(string cohortId, string name, string capacity, string city,
            string modeOfTeaching, string startDate, string endDate)

        {
            ActionResult result;
            try
            {
                CohortController.UpdateCohortById(cohortId, name, capacity, city,
                    modeOfTeaching, startDate, endDate);
                result = StatusCode(200, "Successfully Updated the Cohort details");
            }
            catch (ValidationException e)
            {
                var error = "Error(s) During UpdateCohort: " +
                            e.ValidationExceptions.Select(x => x.Message)
                                .Aggregate((x, y) => x + ", " + y);
                result = BadRequest(error);
            }
            catch (Exception e)
            {
                result = StatusCode(500,
                    "Unexpected server/database error occurred. System error message(s): " + e.Message);
            }

            return result;
        }

        #endregion

        #region /application/GetCohorts

        [HttpGet(nameof(GetCohorts))]
        public ActionResult<List<Cohort>> GetCohorts()
        {
            ActionResult<List<Cohort>> result;
            try
            {
                result = CohortController.GetCohorts();
            }
            catch (Exception e)
            {
                result = StatusCode(500,
                    "Unexpected server/database error occurred. System error message(s): " + e.Message);
            }

            return result;
        }

        #endregion

        #endregion

        #region CourseController

        #region /application/CreateCourse

        /// <summary>
        ///     CreateCourseByCohortId
        ///     Description:The API End Point looks for action CreateCourseByCohortId in CourseController and creates the course
        ///     information on database with specified parameters as defined below.
        ///     EndPoint Testing : localhost:xxxxx/application/CreateCourseByCohortId?cohortId=2&instructorId=1&name=PHP
        ///     &description=Basics of PHP&durationHrs=5&resourcesLink=www.php.com&startDate=2020-10-04&endDate=2020-10-15
        ///     Test Passed
        /// </summary>
        /// <param name="cohortId"></param>
        /// <param name="instructorId"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="durationHrs"></param>
        /// <param name="resourcesLink"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [HttpPost(nameof(CreateCourse))]
        public ActionResult CreateCourse
        (string name, string description,
            string durationHrs)
        {
            ActionResult result;
            try
            {
                CourseController.CreateCourse(name, description,
                    durationHrs);
                result = StatusCode(200, "Successfully Created Course");
            }
            catch (ValidationException e)
            {
                var error = "Error(s) During Creation: " +
                            e.ValidationExceptions.Select(x => x.Message)
                                .Aggregate((x, y) => x + ", " + y);

                result = BadRequest(error);
            }
            catch (Exception e)
            {
                result = StatusCode(500,
                    "Unexpected server/database error occurred. System error message(s): " + e.Message);
            }

            return result;
        }


        #endregion

        #region /application/UpdateCourse

        /// <summary>
        ///     UpdateCourseById
        ///     Description:The API End Point looks for action UpdateCourseById in CourseController and updates the information of
        ///     the course on database as per specified requested edit parameters.
        ///     EndPoint Testing :localhost:xxxxx/application/UpdateCourseById?courseId=6&instructorId=2&name=REDUX&description
        ///     =Basics&durationHrs=2&resourcesLink=www.redux.com
        ///     Test Passed
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="durationHrs"></param>
        /// <returns></returns>
        [HttpPatch("UpdateCourse")]
        public ActionResult UpdateCourseById(string courseId, string name, string description,
            string durationHrs)
        {
            ActionResult result;
            try
            {
                CourseController.UpdateCourseById(courseId, name, description,
                    durationHrs);
                result = StatusCode(200, "Successfully Updated Course");
            }
            catch (ValidationException e)
            {
                var error = "Error(s) During Creation: " +
                            e.ValidationExceptions.Select(x => x.Message)
                                .Aggregate((x, y) => x + ", " + y);

                result = BadRequest(error);
            }
            catch (Exception e)
            {
                result = StatusCode(500,
                    "Unexpected server/database error occurred. System error message(s): " + e.Message);
            }

            return result;
        }

        #endregion

        #region /application/UpdateAssignedCourse

        /// <summary>
        ///     UpdateAssignedCourse
        ///     Description:The API End Point looks for action UpdateAssignedCourse in CourseController and update a
        ///     course according to specified Course id and Cohort id .
        ///     EndPoint Testing : //localhost:xxxxx/application/UpdateAssignedCourse
        ///     Test Passed
        /// </summary>
        /// <param name="cohortId"></param>
        /// <param name="courseId"></param>
        /// <returns> </returns>
        [HttpPatch(nameof(UpdateAssignedCourse))]
        public ActionResult UpdateAssignedCourse(string cohortId, string courseId, string instructorId, string startDate, string endDate, string resourcesLink)
        {
            ActionResult result;
            try
            {
                CohortCourseController.UpdateAssignedCourse(cohortId, courseId, instructorId, startDate, endDate, resourcesLink);
                result = StatusCode(200, "Successfully Assigned Course to Cohort");
            }
            catch (ValidationException e)
            {
                var error = "Error(s) During UpdateAssignedCourse: " +
                            e.ValidationExceptions.Select(x => x.Message)
                                .Aggregate((x, y) => x + ", " + y);

                result = BadRequest(error);
            }
            catch (Exception e)
            {
                result = StatusCode(500,
                    "Unexpected server/database error occurred. System error message(s): " + e.Message);
            }

            return result;

        }
        #endregion

        #region /application/AssignCourse

        /// <summary>
        ///     AssignCourseByCohortId
        ///     Description:The API End Point looks for action AssignCourseByCohortId in CourseController and assigns/creates a
        ///     course according to specified Course id and Cohort id .
        ///     EndPoint Testing : //localhost:xxxxx/application/AssignCourseByCohortId?cohortId=2&courseId=3
        ///     Test Passed
        /// </summary>
        /// <param name="cohortId"></param>
        /// <param name="courseId"></param>
        /// <returns>The End Point returns the Course according to the specified cohort id </returns>
        [HttpPost("AssignCourse")]
        public ActionResult AssignCourseByCohortId(string cohortId, string courseId, string instructorId, string startDate, string endDate, string resourcesLink)
        {
            ActionResult result;
            try
            {
                CohortCourseController.AssignCourseByCohortId(cohortId, courseId, instructorId, startDate, endDate, resourcesLink);
                result = StatusCode(200, "Successfully Assigned Course to Cohort");
            }
            catch (ValidationException e)
            {
                var error = "Error(s) During AssignCourseByCohortId: " +
                            e.ValidationExceptions.Select(x => x.Message)
                                .Aggregate((x, y) => x + ", " + y);

                result = BadRequest(error);
            }
            catch (Exception e)
            {
                result = StatusCode(500,
                    "Unexpected server/database error occurred. System error message(s): " + e.Message);
            }

            return result;
        }
        #endregion

        #region /application/GetAssignedCourse
        [HttpGet(nameof(GetAssignedCourse))]
        public ActionResult<Course> GetAssignedCourse(string courseId, string cohortId)
        {
            ActionResult<Course> result;
            try
            {
                result = CourseController.GetCourseByCohortId(courseId, cohortId);
            }
            catch (ValidationException e)
            {
                var error = "Error(s) During GetAssignedCourse: " +
                            e.ValidationExceptions.Select(x => x.Message)
                                .Aggregate((x, y) => x + ", " + y);

                result = BadRequest(error);
            }
            catch (Exception e)
            {
                result = StatusCode(500,
                    "Unexpected server/database error occurred. System error message(s): " + e.Message);
            }
            return result;
        }
        #endregion

        #region /application/GetCourses

        /// <summary>
        ///     GetCourses
        ///     Description:The API End Point looks for action GetCourses in CourseController and retrieves the information of all
        ///     courses from database.
        ///     EndPoint Testing : localhost:xxxxx/application/GetCourses
        ///     Test Passed
        /// </summary>
        /// <returns>The API End Point returns list of all Courses in database.</returns>
        [HttpGet(nameof(GetCourses))]
        public ActionResult<List<Course>> GetCourses()
        {
            ActionResult<List<Course>> result;
            try
            {
                result = CourseController.GetCourses();
            }
            catch (ValidationException e)
            {
                var error = "Error(s) During GetCourses: " +
                            e.ValidationExceptions.Select(x => x.Message)
                                .Aggregate((x, y) => x + ", " + y);

                result = BadRequest(error);
            }
            catch (Exception e)
            {
                result = StatusCode(500,
                    "Unexpected server/database error occurred. System error message(s): " + e.Message);
            }
            return result;
        }

        #endregion

        #region /application/GetCourseSummary

        /// <summary>
        ///     GetCourseSummary
        ///     Description:The API End Point looks for action GetCoursesByCohortID in CourseController and retrieves the
        ///     information of all courses based on the Cohort id  from database.
        ///     EndPoint Testing : localhost:xxxxx/application/getcoursesummary?cohortId=1
        ///     /*Test Passed*/
        /// </summary>
        /// <param name="cohortId"></param>
        /// <returns></returns>
        [HttpGet(nameof(GetCourseSummary))]
        public ActionResult<List<Course>> GetCourseSummary(string cohortId)
        {
            ActionResult<List<Course>> result;
            try
            {
                result = CourseController.GetCoursesByCohortId(cohortId);
            }
            catch (ValidationException e)
            {
                var error = "Error(s) During GetCourseSummary: " +
                            e.ValidationExceptions.Select(x => x.Message)
                                .Aggregate((x, y) => x + ", " + y);

                result = BadRequest(error);
            }
            catch (Exception e)
            {
                result = StatusCode(500,
                    "Unexpected server/database error occurred. System error message(s): " + e.Message);
            }
            return result;
        }

        #endregion

        #endregion

        #region HomeworkController

        #region /application/CreateHomework

        [HttpPost(nameof(CreateHomework))]
        public ActionResult CreateHomework(string courseId, string instructorId, string cohortId,
            string isAssignment, string title, string avgCompletionTime, string dueDate, string releaseDate,
            string documentLink, string gitHubClassRoomLink)
        {
            ActionResult result;
            try
            {
                HomeworkController.CreateHomeworkByCourseId(courseId, instructorId, cohortId,
                    isAssignment, title, avgCompletionTime, dueDate, releaseDate,
                    documentLink, gitHubClassRoomLink);
                result = StatusCode(200, "Successfully created new Homework");
            }
            catch (ValidationException e)
            {
                var error = "Error(s) During CreateHomework: " +
                            e.ValidationExceptions.Select(x => x.Message)
                                .Aggregate((x, y) => x + ", " + y);

                result = BadRequest(error);
            }
            catch (Exception e)
            {
                result = StatusCode(500,
                    "Unexpected server/database error occurred. System error message(s): " + e.Message);
            }
            return result;
        }

        #endregion

        #region /application/UpdateHomework

        [HttpPatch(nameof(UpdateHomework))]
        public ActionResult UpdateHomework(string homeworkId, string courseId, string instructorId, string cohortId,
            string isAssignment, string title, string avgCompletionTime, string dueDate, string releaseDate,
            string documentLink, string gitHubClassRoomLink)

        {
            ActionResult result;
            try
            {
                HomeworkController.UpdateHomeworkById(homeworkId, courseId, instructorId, cohortId,
                    isAssignment, title, avgCompletionTime, dueDate, releaseDate,
                    documentLink, gitHubClassRoomLink);
                result = StatusCode(200, "Successfully updated Homework");
            }
            catch (ValidationException e)
            {
                var error = "Error(s) During UpdateHomework: " +
                            e.ValidationExceptions.Select(x => x.Message)
                                .Aggregate((x, y) => x + ", " + y);

                result = BadRequest(error);
            }
            catch (Exception e)
            {
                result = StatusCode(500,
                    "Unexpected server/database error occurred. System error message(s): " + e.Message);
            }

            return result;
        }

        /*UpdateHomeworkById(string homeworkId, string courseId, string instructorId, string cohortId,
                    string isAssignment, string title, string avgCompletionTime, string dueDate, string releaseDate,
                    string documentLink, string gitHubClassRoomLink)*/

        #endregion

        #region /application/HomeworkTimesheet

        /// <summary>
        ///     GetHomeworkTimesheetForStudent
        ///     Request Type: GET
        ///     This End point takes in Homework Id from link clicked, Student Id from global store and return associated homework
        ///     record and timesheet record.
        /// </summary>
        /// <param name="homeworkId"></param>
        /// <param name="studentId"></param>
        /// <returns>Tuple of homework record, timesheet record</returns>
        [HttpGet("HomeworkTimesheet")]
        public ActionResult<Tuple<Homework, Timesheet>> GetHomeworkTimesheetForStudent(string homeworkId,
            string studentId)
        {
            ActionResult<Tuple<Homework, Timesheet>> result;
            try
            {
                var homework = HomeworkController.GetHomeworkById(homeworkId);
                var timesheet = TimesheetController.GetTimesheetByHomeworkId(homeworkId, studentId);
                result = new Tuple<Homework, Timesheet>(homework, timesheet);
            }
            catch (ValidationException e)
            {
                var error = "Error(s) During GetHomeworkTimesheetForStudent: " +
                            e.ValidationExceptions.Select(x => x.Message)
                                .Aggregate((x, y) => x + ", " + y);

                result = BadRequest(error);
            }
            catch (Exception e)
            {
                result = StatusCode(500,
                    "Unexpected server/database error occurred. System error message(s): " + e.Message);
            }

            return result;
        }

        #endregion

        #region /application/GetHomework

        /// <summary>
        ///     GetHomeworkForInstructor
        ///     Description:The API End Point looks for action GetHomeworkById in HomeworkController and GetRubricsByHomeworkId in
        ///     RubricController retrieves the information of the Homework with its rubrics from database.
        ///     https://localhost:xxxxx/application/GetHomeworkForInstructor?homeworkId=-1
        /// </summary>
        /// <param name="homeworkId"></param>
        /// <returns></returns>
        [HttpGet("GetHomework")]
        public ActionResult<Tuple<Homework, List<Rubric>, List<User>, List<Course>>> GetHomeworkForInstructor(string homeworkId)
        {
            ActionResult<Tuple<Homework, List<Rubric>, List<User>, List<Course>>> result;
            try
            {
                var homework = HomeworkController.GetHomeworkById(homeworkId);

                var rubricsList = RubricController.GetRubricsByHomeworkId(homeworkId);

                var coursesList = CourseController.GetCourses();

                var instructorsList = UserController.GetInstructors();

                result = new Tuple<Homework, List<Rubric>, List<User>, List<Course>>(homework, rubricsList, instructorsList, coursesList);
            }
            catch (ValidationException e)
            {
                var error = "Error(s) During GetHomeworkForInstructor: " +
                            e.ValidationExceptions.Select(x => x.Message)
                                .Aggregate((x, y) => x + ", " + y);

                result = BadRequest(error);
            }
            catch (Exception e)
            {
                result = StatusCode(500,
                    "Unexpected server/database error occurred. System error message(s): " + e.Message);
            }
            return result;
        }
        #endregion

        #region /application/HomeworkSummary

        /// <summary>     
        ///     GetHomeworkSummary
        ///     Request Type: GET
        ///     This End point takes in Cohort Id and Course Id from global store and return List of homeworks associated with that
        ///     Course for specified Cohort.
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="cohortId"></param>
        /// <returns>List Of Homeworks related to specified Course and Cohort</returns>
        [HttpGet("HomeworkSummary")]
        public ActionResult<IEnumerable<Homework>> GetHomeworkSummary(string courseId, string cohortId)
        {
            ActionResult<IEnumerable<Homework>> result;
            try
            {
                result = HomeworkController.GetHomeworksByCourseId(courseId, cohortId);
            }
            catch (ValidationException e)
            {
                var error = "Error(s) During GetHomeworkSummary: " +
                            e.ValidationExceptions.Select(x => x.Message)
                                .Aggregate((x, y) => x + ", " + y);

                result = BadRequest(error);
            }
            catch (Exception e)
            {
                result = StatusCode(500,
                    "Unexpected server/database error occurred. System error message(s): " + e.Message);
            }

            return result;
        }

        #endregion

        #endregion

        #region GradeController

        #region /application/CreateGrading

        [HttpPost(nameof(CreateGrading))]
        public ActionResult CreateGrading(string studentId,
            [FromBody] Dictionary<string, Tuple<string, string>> gradings)
        {
            ActionResult result;
            try
            {
                GradeController.CreateGradingByStudentId(studentId, gradings);
                result = StatusCode(200, "Successfully Created Grade");
            }
            catch (ValidationException e)
            {
                var error = "Error(s) During CreateGrading: " +
                            e.ValidationExceptions.Select(x => x.Message)
                                .Aggregate((x, y) => x + ", " + y);

                result = BadRequest(error);
            }
            catch (Exception e)
            {
                result = StatusCode(500,
                    "Unexpected server/database error occurred. System error message(s): " + e.Message);
            }
            return result;
        }

        #endregion

        #region /application/UpdateGrading

        [HttpPatch(nameof(UpdateGrading))]
        public ActionResult UpdateGrading(string studentId, [FromBody]
            Dictionary<string, Tuple<string, string>> gradings)
        {
            ActionResult result;
            try
            {
                GradeController.UpdateGradingByStudentId(studentId, gradings);
                result = StatusCode(200, "Successfully Updated the Grade Fields");
            }
            catch (ValidationException e)
            {
                var error = "Error(s) During UpdateGrading: " +
                            e.ValidationExceptions.Select(x => x.Message)
                                .Aggregate((x, y) => x + ", " + y);

                result = BadRequest(error);
            }
            catch (Exception e)
            {
                result = StatusCode(500,
                    "Unexpected server/database error occurred. System error message(s): " + e.Message);
            }
            return result;
        }

        #endregion

        #region /application/UpdateStudentFeedback

        [HttpPatch(nameof(UpdateStudentFeedback))]
        public ActionResult UpdateStudentFeedback(string studentId,
            [FromBody] Dictionary<string, string> studentComment)
        {
            ActionResult result;
            try
            {
                GradeController.UpdateGradingByStudentId(studentId, studentComment);
                result = StatusCode(200, "Success Message");
            }
            catch (ValidationException e)
            {
                var error = "Error(s) During UpdateStudentFeedback: " +
                            e.ValidationExceptions.Select(x => x.Message)
                                .Aggregate((x, y) => x + ", " + y);

                result = BadRequest(error);
            }
            catch (Exception e)
            {
                result = StatusCode(500,
                    "Unexpected server/database error occurred. System error message(s): " + e.Message);
            }

            return result;
        }

        #endregion

        #region /application/StudentGradesSummary

        /// <summary>
        ///     GetGrades
        ///     Request Type: GET
        ///     This Endpoint returns Grades of a specified student for a specified course.
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="homeworkId"></param>
        /// <returns>Grades for one student for one course</returns>
        [HttpGet("StudentGradesSummary")]
        public ActionResult<List<Grade>> GetGrades(string studentId, string homeworkId)
        {
            ActionResult<List<Grade>> result;
            try
            {
                result = GradeController.GetGradesByStudentId(studentId, homeworkId);
            }
            catch (ValidationException e)
            {
                var error = "Error(s) During GetGrades: " +
                            e.ValidationExceptions.Select(x => x.Message)
                                .Aggregate((x, y) => x + ", " + y);

                result = BadRequest(error);
            }
            catch (Exception e)
            {
                result = StatusCode(500,
                    "Unexpected server/database error occurred. System error message(s): " + e.Message);
            }

            return result;
        }

        #endregion

        #region /application/InstructorGradeSummary

        /// <summary>
        ///     GetGradeSummaryForInstructor
        ///     Request Type: GET
        ///     This Endpoint returns Grade Summary and Timesheet Summary for all students in a cohort for a specified Homework.
        /// </summary>
        /// <param name="cohortId"></param>
        /// <param name="homeworkId"></param>
        /// <returns>custom object contains GradeSummery and Timesheet Summary for all students for a specified Homework</returns>
        [HttpGet("InstructorGradeSummary")]
        public ActionResult<List<GradeSummaryTypeForInstructor>> GetGradeSummaryForInstructor(string cohortId,
            string homeworkId)
        {
            ActionResult<List<GradeSummaryTypeForInstructor>> result;
            try
            {
                result = GradeController.GetGradeSummaryForInstructor(cohortId, homeworkId);
            }
            catch (ValidationException e)
            {
                var error = "Error(s) During Creation: " +
                            e.ValidationExceptions.Select(x => x.Message)
                                .Aggregate((x, y) => x + ", " + y);

                result = BadRequest(error);
            }
            catch (Exception e)
            {
                result = StatusCode(500,
                    "Unexpected server/database error occurred. System error message(s): " + e.Message);
            }

            return result;

        }

        #endregion

        #endregion

        #region TimesheetController

        #region /application/CreateTimesheet

        /// <summary>
        ///     CreateTimesheetByHomeworkId
        ///     Request Type: POST
        ///     Ths Endpoint takes in the below parameters and create a Timesheet record in the DB.
        /// </summary>
        /// <param name="homeworkId"></param>
        /// <param name="studentId"></param>
        /// <param name="solvingTime"></param>
        /// <param name="studyTime"></param>
        /// <returns>Success/Error message</returns>

        [HttpPost("CreateTimesheet")]
        public ActionResult CreateTimesheetByHomeworkId(string homeworkId, string studentId, string solvingTime,
            string studyTime)
        {
            ActionResult result;
            try
            {
                TimesheetController.CreateTimesheetByHomeworkId(homeworkId, studentId, solvingTime, studyTime);
                result = StatusCode(200, "Successfully created TimeSheet");
            }
            catch (ValidationException e)
            {
                var error = "Error(s) During CreateTimesheet: " +
                            e.ValidationExceptions.Select(x => x.Message)
                                .Aggregate((x, y) => x + ", " + y);

                result = BadRequest(error);
            }
            catch (Exception e)
            {
                result = StatusCode(500,
                    "Unexpected server/database error occurred. System error message(s): " + e.Message);
            }

            return result;
        }

        #endregion

        #region /application/UpdateTimesheet


        /// <summary>
        ///     UpdateTimesheetById
        ///     Description:The API End Point looks for action UpdateTimesheetById in TimesheetController and updates the
        ///     information of the timesheet on database as per specified requested edit parameters.
        ///     EndPoint Testing : localhost:xxxxx/application/UpdateTimesheetById
        ///     Test Passed
        /// </summary>
        /// <param name="timesheetId"></param>
        /// <param name="solvingTime"></param>
        /// <param name="studyTime"></param>
        /// <returns>The End Point returns Success Message and Updates the Timesheet according to parameters specified </returns>
        [HttpPatch("UpdateTimesheet")]
        public ActionResult UpdateTimesheetById(string timesheetId, string solvingTime, string studyTime)
        {
            ActionResult result;
            try
            {
                TimesheetController.UpdateTimesheetById(timesheetId, solvingTime, studyTime);
                result = StatusCode(200, "Successfully Updated Timesheet");
            }
            catch (ValidationException e)
            {
                var error = "Error(s) During UpdateTimesheetById: " +
                            e.ValidationExceptions.Select(x => x.Message)
                                .Aggregate((x, y) => x + ", " + y);

                result = BadRequest(error);
            }
            catch (Exception e)
            {
                result = StatusCode(500,
                    "Unexpected server/database error occurred. System error message(s): " + e.Message);
            }

            return result;
        }

        #endregion

        #endregion

        #region RubricController

        #region /application/CreateRubrics

        [HttpPost(nameof(CreateRubrics))]
        public ActionResult CreateRubrics(string homeworkId, [FromBody] List<Tuple<string, string, string>> rubrics)
        {
            ActionResult result;
            try
            {
                RubricController.CreateRubricsByHomeworkId(homeworkId, rubrics);
                result = StatusCode(200, "Successfully created Rubric for Homework.");
            }
            catch (ValidationException e)
            {
                var error = "Error(s) During CreateRubric: " +
                            e.ValidationExceptions.Select(x => x.Message)
                                .Aggregate((x, y) => x + ", " + y);

                result = BadRequest(error);
            }
            catch (Exception e)
            {
                result = StatusCode(500,
                    "Unexpected server/database error occurred. System error message(s): " + e.Message);
            }
            return result;
        }

        #endregion

        #region /application/UpdateRubrics

        [HttpPatch(nameof(UpdateRubrics))]
        public ActionResult UpdateRubrics([FromBody] Dictionary<string, Tuple<string, string, string>> rubrics)
        {
            ActionResult result;
            try
            {
                RubricController.UpdateRubricsById(rubrics);
                result = StatusCode(200, "Successfully updated the rubric");
            }
            catch (ValidationException e)
            {
                var error = "Error(s) During UpdateRubric: " +
                            e.ValidationExceptions.Select(x => x.Message)
                                .Aggregate((x, y) => x + ", " + y);

                result = BadRequest(error);
            }
            catch (Exception e)
            {
                result = StatusCode(500,
                    "Unexpected server/database error occurred. System error message(s): " + e.Message);
            }
            return result;
        }

        #endregion

        #endregion

        #region UserController

        #region /application/GetInstructors

        /// <summary>
        ///     GetCourses
        ///     Description:The API End Point looks for action GetInstructors in UserController and retrieves the information of
        ///     all instructors from database.
        ///     EndPoint Testing : localhost:xxxxx/application/GetInstructors
        ///     /*Test Passed*/
        /// </summary>
        /// <returns>The API End Point returns list of all Instructors in database</returns>
     [HttpGet(nameof(GetInstructors))]
        public ActionResult<List<User>> GetInstructors()
        {
            ActionResult<List<User>> result;
            try
            {
                result = UserController.GetInstructors();
            } 
            catch (Exception e)
            {
                result = StatusCode(500, "Unexpected server/database error occurred. System error message(s): " + e.Message);
            }
            return result;

        }

        #endregion

        #endregion

        #region Archive EndPoints

        #region /application/ArchiveCohort

        [HttpPatch(nameof(ArchiveCohort))]
        public ActionResult ArchiveCohort(string cohortId)
        {
            ActionResult result;

            try
            {
                CohortController.ArchiveCohortById(cohortId);

                result = StatusCode(200, $"Successfully Archived Cohort Id: {cohortId.Trim()}");
            }
            catch (ValidationException e)
            {
                var error = "Error(s) During ArchiveCohort: " +
                            e.ValidationExceptions.Select(x => x.Message)
                                .Aggregate((x, y) => x + ", " + y);
                result = BadRequest(error);
            }
            catch (Exception e)
            {
                result = StatusCode(500,
                    "Unexpected server/database error occurred. System error message(s): " + e.Message);
            }

            return result;
        }

        #endregion

        #region /application/ArchiveCourse

        [HttpPatch(nameof(ArchiveCourse))]
        public ActionResult ArchiveCourse(string courseId)
        {
            ActionResult result;

            try
            {
                CourseController.ArchiveCourseById(courseId);

                result = StatusCode(200, $"Successfully Archived Course Id: {courseId.Trim()}");
            }
            catch (ValidationException e)
            {
                var error = "Error(s) During ArchiveCourse: " +
                            e.ValidationExceptions.Select(x => x.Message)
                                .Aggregate((x, y) => x + ", " + y);
                result = BadRequest(error);
            }
            catch (Exception e)
            {
                result = StatusCode(500,
                    "Unexpected server/database error occurred. System error message(s): " + e.Message);
            }

            return result;


        }
        #endregion

        #region /application/ArchiveAssignedCourse

        [HttpPatch(nameof(ArchiveAssignedCourse))]
        public ActionResult ArchiveAssignedCourse(string cohortId,string courseId)
        {
            ActionResult result;

            try
            {
                CohortCourseController.ArchiveAssignedCourse(cohortId, courseId);

                result = StatusCode(200, $"Successfully Archived course assignment. Course Id: {courseId.Trim()} | Cohort Id: {cohortId.Trim()}");
            }
            catch (ValidationException e)
            {
                var error = "Error(s) During ArchiveAssignedCourse: " +
                            e.ValidationExceptions.Select(x => x.Message)
                                .Aggregate((x, y) => x + ", " + y);
                result = BadRequest(error);
            }
            catch (Exception e)
            {
                result = StatusCode(500,
                    "Unexpected server/database error occurred. System error message(s): " + e.Message);
            }

            return result;

        }

        #endregion

        #region /application/ArchiveHomework

        [HttpPatch(nameof(ArchiveHomework))]
        public ActionResult ArchiveHomework (string homeworkId)
        {
            ActionResult result;

            try
            {
                HomeworkController.ArchiveHomeworkById(homeworkId);

                result = StatusCode(200, $"Successfully Archived Homework Id: {homeworkId.Trim()}");
            }
            catch (ValidationException e)
            {
                var error = "Error(s) During ArchiveHomework: " +
                            e.ValidationExceptions.Select(x => x.Message)
                                .Aggregate((x, y) => x + ", " + y);
                result = BadRequest(error);
            }
            catch (Exception e)
            {
                result = StatusCode(500,
                    "Unexpected server/database error occurred. System error message(s): " + e.Message);
            }
            return result;
        }

        #endregion

        #endregion
    }
}