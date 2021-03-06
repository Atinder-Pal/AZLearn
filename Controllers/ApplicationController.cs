﻿using System;
using System.Collections.Generic;
using System.Linq;
using AZLearn.Models;
using AZLearn.Models.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace AZLearn.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApplicationController : Controller
    {
        #region CohortController

        #region /application/CreateCohort

        /// <summary>
        ///     CreateCohort
        ///     Description: This End point returns a Success message upon creation of cohort otherwise returns an error message
        /// </summary>
        /// <param name="name"></param>
        /// <param name="capacity"></param>
        /// <param name="city"></param>
        /// <param name="modeOfTeaching"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns>Success/Error message</returns>
        [HttpPost(nameof(CreateCohort))]
        public ActionResult CreateCohort([FromQuery] string name, [FromQuery] string capacity, [FromQuery] string city,
            [FromQuery] string modeOfTeaching, [FromQuery] string startDate, [FromQuery] string endDate)

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
            // catch (Exception e)
            // {
            //     result = StatusCode(500,
            //         "Unexpected server/database error occurred. System error message(s): " + e.Message);
            // }

            return result;
        }

        #endregion

        #region /application/UpdateCohort

        /// <summary>
        ///     UpdateCohort
        ///     Description: This End point returns a Success message upon update of cohort otherwise returns an error message
        /// </summary>
        /// <param name="cohortId"></param>
        /// <param name="name"></param>
        /// <param name="capacity"></param>
        /// <param name="city"></param>
        /// <param name="modeOfTeaching"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns>Success/Error message</returns>
        [HttpPatch(nameof(UpdateCohort))]
        public ActionResult UpdateCohort([FromQuery] string cohortId, [FromQuery] string name,
            [FromQuery] string capacity, [FromQuery] string city,
            [FromQuery] string modeOfTeaching, [FromQuery] string startDate, [FromQuery] string endDate)

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

        /// <summary>
        ///     GetCohorts
        ///     Description: This Endpoint returns a list of cohorts
        /// </summary>
        /// <returns>List of cohorts</returns>
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

        #region /application/GetCohort

        /// <summary>
        ///     GetCohortById
        ///     Description:The API End Point looks for action GetCohortById in CohortController and retrieves the information of
        ///     that Cohort
        /// </summary>
        /// <returns>The API End Point returns Cohort record with matching CohortId.</returns>
        [HttpGet("GetCohort")]
        public ActionResult<Cohort> GetCohortById(string cohortId)
        {
            ActionResult<Cohort> result;
            try
            {
                result = CohortController.GetCohortById(cohortId);
            }
            catch (ValidationException e)
            {
                var error = "Error(s) During Retrieving a Cohort: " +
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

        #region CourseController

        #region /application/CreateCourse

        /// <summary>
        ///     CreateCourseByCohortId
        ///     Description:The API End Point looks for action CreateCourseByCohortId in CourseController and creates the course
        ///     information on database with specified parameters as defined below.
        /// </summary>
        /// <param name="cohortId"></param>
        /// <param name="instructorId"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="durationHrs"></param>
        /// <param name="resourcesLink"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns>Success/Error Message</returns>
        [HttpPost(nameof(CreateCourse))]
        public ActionResult CreateCourse
        ([FromQuery] string name, [FromQuery] string description,
            [FromQuery] string durationHrs)
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
                var error = "Error(s) During Course Creation: " +
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
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="durationHrs"></param>
        /// <returns>Success/Error message</returns>
        [HttpPatch("UpdateCourse")]
        public ActionResult UpdateCourseById([FromQuery] string courseId, [FromQuery] string name,
            [FromQuery] string description,
            [FromQuery] string durationHrs)
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
                var error = "Error(s) During Updating Course: " +
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
        /// </summary>
        /// <param name="cohortId"></param>
        /// <param name="courseId"></param>
        /// <returns>Success/Error Message </returns>
        [HttpPatch(nameof(UpdateAssignedCourse))]
        public ActionResult UpdateAssignedCourse([FromQuery] string cohortId, [FromQuery] string courseId,
            [FromQuery] string instructorId, [FromQuery] string startDate, [FromQuery] string endDate,
            [FromQuery] string resourcesLink)
        {
            ActionResult result;
            try
            {
                CohortCourseController.UpdateAssignedCourse(cohortId, courseId, instructorId, startDate, endDate,
                    resourcesLink);
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
        /// </summary>
        /// <param name="cohortId"></param>
        /// <param name="courseId"></param>
        /// <returns>The End Point returns the Course according to the specified cohort id </returns>
        [HttpPost("AssignCourse")]
        public ActionResult AssignCourseByCohortId([FromQuery] string cohortId, [FromQuery] string courseId,
            [FromQuery] string instructorId, [FromQuery] string startDate, [FromQuery] string endDate,
            [FromQuery] string resourcesLink)
        {
            ActionResult result;
            try
            {
                CohortCourseController.AssignCourseByCohortId(cohortId, courseId, instructorId, startDate, endDate,
                    resourcesLink);
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

        /// <summary>
        ///     GetAssignedCourse
        ///     Description:The API End Point looks for action GetCourseByCohortId in Course Controller and retrieves the
        ///     information of
        ///     all courses assigned to a specific cohortId from database.
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="cohortId"></param>
        /// <returns>Tuple of courses assigned to a cohort</returns>
        [HttpGet(nameof(GetAssignedCourse))]
        public ActionResult<Tuple<string, string, string, string, string, string, string>> GetAssignedCourse(
            [FromQuery] string courseId, [FromQuery] string cohortId)

        {
            ActionResult<Tuple<string, string, string, string, string, string, string>> result;
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

        #region /application/GetCourse

        /// <summary>
        ///     GetCourseById
        ///     Description:The API End Point looks for action GetCourseById in CourseController and retrieves the information of
        ///     that Course.
        /// </summary>
        /// <returns>The API End Point returns Course record with matching CourseId.</returns>
        [HttpGet("GetCourse")]
        public ActionResult<Course> GetCourseById(string courseId)
        {
            ActionResult<Course> result;
            try
            {
                result = CourseController.GetCourseById(courseId);
            }
            catch (ValidationException e)
            {
                var error = "Error(s) During GetCourseById: " +
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
        /// </summary>
        /// <param name="cohortId"></param>
        /// <returns>list of all courses by cohort id</returns>
        [HttpGet(nameof(GetCourseSummary))]
        public ActionResult<List<Tuple<Course, string>>> GetCourseSummary(string cohortId)
        {
            ActionResult<List<Tuple<Course, string>>> result;
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

        /// <summary>
        ///     CreateHomework
        ///     Description: This Endpoint takes in the below parameters and create a Homework record in the database for
        ///     Instructor screen.
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="instructorId"></param>
        /// <param name="cohortId"></param>
        /// <param name="isAssignment"></param>
        /// <param name="title"></param>
        /// <param name="avgCompletionTime"></param>
        /// <param name="dueDate"></param>
        /// <param name="releaseDate"></param>
        /// <param name="documentLink"></param>
        /// <param name="gitHubClassRoomLink"></param>
        /// <returns>Success/Error message</returns>
        [HttpPost(nameof(CreateHomework))]
        public ActionResult CreateHomework([FromQuery] string courseId, [FromQuery] string instructorId,
            [FromQuery] string cohortId,
            [FromQuery] string isAssignment, [FromQuery] string title, [FromQuery] string avgCompletionTime,
            [FromQuery] string dueDate, [FromQuery] string releaseDate,
            [FromQuery] string documentLink, [FromQuery] string gitHubClassRoomLink)
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

        #region /application/CreateHomeworkRubrics

        /// <summary>
        ///     CreateHomeworkRubrics
        ///     This API End Point takes in below parameters and returns successful message upon creating a rubric  for a homework
        ///     otherwise returns an error message.
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="instructorId"></param>
        /// <param name="cohortId"></param>
        /// <param name="isAssignment"></param>
        /// <param name="title"></param>
        /// <param name="avgCompletionTime"></param>
        /// <param name="dueDate"></param>
        /// <param name="releaseDate"></param>
        /// <param name="documentLink"></param>
        /// <param name="gitHubClassRoomLink"></param>
        /// <param name="rubrics"></param>
        /// <returns>Success/Error message</returns>
        [HttpPost(nameof(CreateHomeworkRubrics))]
        public ActionResult CreateHomeworkRubrics([FromQuery] string courseId, [FromQuery] string instructorId,
            [FromQuery] string cohortId, [FromQuery] string isAssignment, [FromQuery] string title,
            [FromQuery] string avgCompletionTime, [FromQuery] string dueDate, [FromQuery] string releaseDate,
            [FromQuery] string documentLink, [FromQuery] string gitHubClassRoomLink,
            [FromBody] List<Tuple<string, string, string>> rubrics)
        {
            ActionResult result;
            var practiceRubric = new List<Tuple<string, string, string>>();
            try
            {
                var homeworkId = HomeworkController.CreateHomeworkByCourseId(courseId, instructorId, cohortId,
                    isAssignment, title, avgCompletionTime, dueDate, releaseDate,
                    documentLink, gitHubClassRoomLink);

                if (rubrics.Count == 0)
                {
                    var practiceRubricTuple = new Tuple<string, string, string>("false",
                        "Practice submitted on due date to GitHub classroom", "1");
                    practiceRubric.Add(practiceRubricTuple);

                    RubricController.CreateRubricsByHomeworkId(homeworkId, practiceRubric);
                }
                else
                {
                    RubricController.CreateRubricsByHomeworkId(homeworkId, rubrics);
                }

                result = StatusCode(200, "Successfully created new Homework and Rubrics");
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

        /// <summary>
        ///     UpdateHomework
        ///     Description: This End point takes in below parameters and returns a Success message upon update of homework
        ///     otherwise returns an error message
        /// </summary>
        /// <param name="homeworkId"></param>
        /// <param name="courseId"></param>
        /// <param name="instructorId"></param>
        /// <param name="cohortId"></param>
        /// <param name="isAssignment"></param>
        /// <param name="title"></param>
        /// <param name="avgCompletionTime"></param>
        /// <param name="dueDate"></param>
        /// <param name="releaseDate"></param>
        /// <param name="documentLink"></param>
        /// <param name="gitHubClassRoomLink"></param>
        /// <returns>Success/Error message</returns>
        [HttpPatch(nameof(UpdateHomework))]
        public ActionResult UpdateHomework([FromQuery] string homeworkId, [FromQuery] string courseId,
            [FromQuery] string instructorId, [FromQuery] string cohortId,
            [FromQuery] string isAssignment, [FromQuery] string title, [FromQuery] string avgCompletionTime,
            [FromQuery] string dueDate, [FromQuery] string releaseDate,
            [FromQuery] string documentLink, [FromQuery] string gitHubClassRoomLink)

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

        #endregion

        #region /application/UpdateHomeworkRubrics

        /// <summary>
        ///     UpdateHomeworkRubrics
        ///     Description: This End point takes in below parameters and returns a Success message upon update of a rubric for a
        ///     homework otherwise returns an error message
        /// </summary>
        /// <param name="homeworkId"></param>
        /// <param name="courseId"></param>
        /// <param name="instructorId"></param>
        /// <param name="cohortId"></param>
        /// <param name="isAssignment"></param>
        /// <param name="title"></param>
        /// <param name="avgCompletionTime"></param>
        /// <param name="dueDate"></param>
        /// <param name="releaseDate"></param>
        /// <param name="documentLink"></param>
        /// <param name="gitHubClassRoomLink"></param>
        /// <param name="rubrics"></param>
        /// <returns>Success/Error message</returns>
        [HttpPatch(nameof(UpdateHomeworkRubrics))]
        public ActionResult UpdateHomeworkRubrics([FromQuery] string homeworkId, [FromQuery] string courseId,
            [FromQuery] string instructorId, [FromQuery] string cohortId,
            [FromQuery] string isAssignment, [FromQuery] string title, [FromQuery] string avgCompletionTime,
            [FromQuery] string dueDate, [FromQuery] string releaseDate, [FromQuery] string documentLink,
            [FromQuery] string gitHubClassRoomLink,
            [FromBody] Dictionary<string, Tuple<string, string, string>> rubrics)

        {
            ActionResult result;
            try
            {
                HomeworkController.UpdateHomeworkById(homeworkId, courseId, instructorId, cohortId,
                    isAssignment, title, avgCompletionTime, dueDate, releaseDate,
                    documentLink, gitHubClassRoomLink);

                RubricController.UpdateRubricsById(rubrics);

                result = StatusCode(200, "Successfully updated Homework and Rubrics");
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
            var homework = new Homework();
            var timesheet = new Timesheet();
            try
            {
                homework = HomeworkController.GetHomeworkById(homeworkId);
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

            try
            {
                timesheet = TimesheetController.GetTimesheetByHomeworkId(homeworkId, studentId);
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
        /// </summary>
        /// <param name="homeworkId"></param>
        /// <returns>A homework ,instructor name and course name</returns>
        [HttpGet("GetHomework")]
        public ActionResult<Dictionary<string, string>> GetHomeworkForInstructor(string homeworkId)
        {
            ActionResult<Dictionary<string, string>> result;
            try
            {
                var homework = HomeworkController.GetHomeworkById(homeworkId);
                var courseId = homework.CourseId.ToString();
                var courseName = CourseController.GetCourseById(courseId).Name;
                var instructorId = homework.InstructorId.ToString();
                var instructorName = UserController.GetUserById(instructorId).Name;

                result = new Dictionary<string, string>
                {
                    {"HomeworkId", homework.HomeworkId.ToString()},
                    {"CourseId", homework.CourseId.ToString()},
                    {"InstructorId", homework.InstructorId.ToString()},
                    {"CohortId", homework.CohortId.ToString()},
                    {"IsAssignment", homework.IsAssignment.ToString()},
                    {"Title", homework.Title},
                    {"AvgCompletionTime", homework.AvgCompletionTime.ToString()},
                    {"DueDate", homework.DueDate.ToString()},
                    {"ReleaseDate", homework.ReleaseDate.ToString()},
                    {"DocumentLink", homework.DocumentLink},
                    {"GitHubClassRoomLink", homework.GitHubClassRoomLink},
                    {"Archive", homework.Archive.ToString()},
                    {"InstructorName", instructorName},
                    {"CourseName", courseName}
                };
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

        #region /application/GetHomeworkRubrics

        /// <summary>
        ///     GetHomeworkRubricsForInstructor
        ///     Description:The API End Point looks for action GetHomeworkById in HomeworkController and GetRubricsByHomeworkId in
        ///     RubricController retrieves the information of the Homework with its rubrics from database.
        /// </summary>
        /// <param name="homeworkId"></param>
        /// <returns>A homework with list of rubrics,instructor name ,course name</returns>
        [HttpGet("GetHomeworkRubrics")]
        public ActionResult<Tuple<Homework, List<Rubric>, string, string>> GetHomeworkRubricsForInstructor(
            string homeworkId)
        {
            ActionResult<Tuple<Homework, List<Rubric>, string, string>> result;
            try
            {
                var homework = HomeworkController.GetHomeworkById(homeworkId);
                var rubricsList = RubricController.GetRubricsByHomeworkId(homeworkId);
                var courseId = homework.CourseId.ToString();
                var courseName = CourseController.GetCourseById(courseId).Name;
                var instructorId = homework.InstructorId.ToString();
                var instructorName = UserController.GetUserById(instructorId).Name;

                result = new Tuple<Homework, List<Rubric>, string, string>(homework, rubricsList, instructorName,
                    courseName);
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
        ///     Description: This End point takes in Cohort Id and Course Id from global store and return List of homeworks associated with that
        ///     Course for specified Cohort.
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="cohortId"></param>
        /// <returns>List Of Homeworks related to specified Course and Cohort</returns>
        [HttpGet("HomeworkSummary")]
        public ActionResult<IEnumerable<Homework>> GetHomeworkSummary([FromQuery] string courseId,
            [FromQuery] string cohortId)
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

        /// <summary>
        ///     CreateGrading
        ///     Description:This API End Point takes in below parameters and returns successful message upon creating a grade
        ///     otherwise returns an error message.
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="gradings"></param>
        /// <returns>Success/Error message</returns>
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

        /// <summary>
        ///     UpdateGrading
        ///     Description: This API End Point takes in below parameters and returns successful message upon updating a grade
        ///     otherwise returns an error message.
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="gradings"></param>
        /// <returns>Success/Error message</returns>
        [HttpPatch(nameof(UpdateGrading))]
        public ActionResult UpdateGrading(string studentId,
            [FromBody] Dictionary<string, Tuple<string, string>> gradings)
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

        /// <summary>
        ///     UpdateStudentFeedback
        ///     Description: This API End Point takes in below parameters and returns successful message upon updating student
        ///     comment on a grade otherwise returns an error message.
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="studentComment"></param>
        /// <returns>Success/Error message</returns>
        [HttpPatch(nameof(UpdateStudentFeedback))]
        public ActionResult UpdateStudentFeedback(string studentId,
            [FromBody] Dictionary<string, string> studentComment)
        {
            ActionResult result;
            try
            {
                GradeController.UpdateGradingByStudentId(studentId, studentComment);
                result = StatusCode(200, "Successfully Updated Student Comments");
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
        ///     Description:This Endpoint returns Grade Summary and Timesheet Summary for all students in a cohort for a specified Homework.
        /// </summary>
        /// <param name="cohortId"></param>
        /// <param name="homeworkId"></param>
        /// <returns>custom object contains GradeSummery and Timesheet Summary for all students for a specified Homework</returns>
        [HttpGet("InstructorGradeSummary")]
        public ActionResult<List<GradeSummaryTypeForInstructor>> GetGradeSummaryForInstructor(
            [FromQuery] string cohortId,
            [FromQuery] string homeworkId)
        {
            ActionResult<List<GradeSummaryTypeForInstructor>> result;
            try
            {
                result = GradeController.GetGradeSummaryForInstructor(cohortId, homeworkId);
            }
            catch (ValidationException e)
            {
                var error = "Error(s) During Retrieving Grade Summary For Instructor: " +
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
        ///     Description: This Endpoint takes in the below parameters and create a Timesheet record in the DB.
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
        /// </summary>
        /// <param name="timesheetId"></param>
        /// <param name="solvingTime"></param>
        /// <param name="studyTime"></param>
        /// <returns>The End Point returns Success Message and Updates the Timesheet according to parameters specified </returns>
        [HttpPatch("UpdateTimesheet")]
        public ActionResult UpdateTimesheetById([FromQuery] string timesheetId, [FromQuery] string solvingTime,
            [FromQuery] string studyTime)
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

        /// <summary>
        ///     CreateRubrics
        ///     Description: This API End Point takes in below parameters and returns successful message upon creating a rubric
        ///     otherwise returns an error message.
        /// </summary>
        /// <param name="homeworkId"></param>
        /// <param name="rubrics"></param>
        /// <returns>Sucess/Error message</returns>
        [HttpPost(nameof(CreateRubrics))]
        public ActionResult CreateRubrics(string homeworkId, [FromBody] List<Tuple<string, string, string>> rubrics)
        {
            /*IsChalllenge |Criteria| Weight */
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

        /// <summary>
        ///     UpdateRubrics
        ///     Description: This API End Point takes in below parameters and returns successful message upon updating a rubric
        ///     otherwise returns an error message
        /// </summary>
        /// <param name="rubrics"></param>
        /// <returns>Sucess/Error message</returns>
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
        ///     GetInstructors
        ///     Description:The API End Point looks for action GetInstructors in UserController and retrieves the information of
        ///     all instructors from database.
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
                result = StatusCode(500,
                    "Unexpected server/database error occurred. System error message(s): " + e.Message);
            }

            return result;
        }

        #endregion

        #region /application/CreateUser

        /// <summary>
        ///     CreateUser
        ///     Description: This endpoint will look for CreateUser action in User control and would create a user otherwise
        ///     displays an error message
        /// </summary>
        /// <param name="cohortId"></param>
        /// <param name="name"></param>
        /// <param name="passwordHash"></param>
        /// <param name="email"></param>
        /// <param name="isInstructor"></param>
        /// <returns>Success/Error message</returns>
        [HttpPost(nameof(CreateUser))]
        public ActionResult CreateUser([FromQuery] string cohortId, [FromQuery] string name,
            [FromQuery] string passwordHash, [FromQuery] string email, [FromQuery] string isInstructor)
        {
            ActionResult result;
            try
            {
                UserController.CreateUser(cohortId, name, passwordHash, email, isInstructor);
                result = StatusCode(200, "Successfully Registered new User");
            }
            catch (ValidationException e)
            {
                var error = "Error(s) During Registering User: " +
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

        #region /application/Login

        /// <summary>
        ///     GetUserOnLogin
        ///     Description: This endpoint will retrieve the user information  and lets user log in if correct input fields(email
        ///     and password) have been provided by the user otherwise an error message is displayed
        /// </summary>
        /// <param name="email"></param>
        /// <param name="passwordHash"></param>
        /// <returns>Login Successful/Error message </returns>
        [HttpGet("Login")]
        public ActionResult<User> GetUserOnLogin(string email, string passwordHash)
        {
            ActionResult<User> result;
            try
            {
                result = UserController.GetUserOnLogin(email, passwordHash);
            }
            catch (ValidationException e)
            {
                var error = "Error(s) During Login User " +
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

        #region Archive EndPoints

        #region /application/ArchiveCohort

        /// <summary>
        ///     ArchiveCohort
        ///     Description:This API End Point takes in below parameters and archives a cohortId PK
        /// </summary>
        /// <param name="cohortId"></param>
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

        /// <summary>
        ///     ArchiveCourse
        ///     Description:This API End Point takes in below parameters and archives a courseId PK
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
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

        /// <summary>
        ///     ArchiveAssignedCourse
        ///     Description:This API End Point takes in below parameters and archives a courseId PK and cohortId as FK
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="cohortId"></param>
        [HttpPatch(nameof(ArchiveAssignedCourse))]
        public ActionResult ArchiveAssignedCourse([FromQuery] string courseId, [FromQuery] string cohortId)
        {
            ActionResult result;

            try
            {
                CohortCourseController.ArchiveAssignedCourse(courseId, cohortId);

                result = StatusCode(200,
                    $"Successfully Archived course assignment. Course Id: {courseId.Trim()} | Cohort Id: {cohortId.Trim()}");
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

        /// <summary>
        ///     ArchiveHomework
        ///     Description:This API End Point takes in below parameters and archives a homeworkId PK
        /// </summary>
        /// <param name="homeworkId"></param>
        [HttpPatch(nameof(ArchiveHomework))]
        public ActionResult ArchiveHomework(string homeworkId)
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