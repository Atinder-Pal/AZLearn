﻿using System;
using System.Linq;
using AZLearn.Data;
using AZLearn.Models;
using AZLearn.Models.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace AZLearn.Controllers
{
    public class TimesheetController : ControllerBase
    {
        /// <summary>
        ///     CreateTimesheetByHomeworkId
        ///     Description: Controller action that creates new time-sheet by HomeworkId
        ///     It expects below parameters, and would populate the same as new time-sheet in the database
        /// </summary>
        /// <param name="homeworkId"></param>
        /// <param name="studentId"></param>
        /// <param name="solvingTime">string provided from frontend, and parsed to float to match model property data type</param>
        /// <param name="studyTime">string provided from frontend, and parsed to float to match model property data type</param>
        public static void CreateTimesheetByHomeworkId(string homeworkId, string studentId, string solvingTime,
            string studyTime)
        {
            var parsedHomeworkId = 0;
            var parsedStudentId = 0;
            float parsedSolvingTime = 0;
            float parsedStudyTime = 0;
            var exception = new ValidationException();
            using var context = new AppDbContext();

            #region Validation

            homeworkId = string.IsNullOrEmpty(homeworkId) || string.IsNullOrWhiteSpace(homeworkId)
                ? null
                : homeworkId.Trim();
            studentId = string.IsNullOrEmpty(studentId) || string.IsNullOrWhiteSpace(studentId)
                ? null
                : studentId.Trim();
            solvingTime = string.IsNullOrEmpty(solvingTime) || string.IsNullOrWhiteSpace(solvingTime)
                ? null
                : solvingTime.Trim();
            studyTime = string.IsNullOrEmpty(studyTime) || string.IsNullOrWhiteSpace(studyTime)
                ? null
                : studyTime.Trim();

            if (string.IsNullOrWhiteSpace(homeworkId))
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(homeworkId),
                    nameof(homeworkId) + " is null."));
            }
            else
            {
                if (!int.TryParse(homeworkId, out parsedHomeworkId))
                    exception.ValidationExceptions.Add(new Exception("Invalid value for homeworkId"));
                if (parsedHomeworkId > 2147483647 || parsedHomeworkId < 1)
                    exception.ValidationExceptions.Add(
                        new Exception("homeworkId value should be between 1 & 2147483647 inclusive"));
                else if (!context.Homeworks.Any(key => key.HomeworkId == parsedHomeworkId))
                    exception.ValidationExceptions.Add(new Exception("homeworkId does not exist"));
                /*To get homework Id that is not archived*/

                else if (!context.Homeworks.Any(key => key.HomeworkId == parsedHomeworkId && key.Archive == false))
                    exception.ValidationExceptions.Add(new Exception("Selected homeworkId is Archived"));
            }

            if (string.IsNullOrWhiteSpace(studentId))
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(studentId),
                    nameof(studentId) + " is null."));
            }
            else
            {
                if (!int.TryParse(studentId, out parsedStudentId))
                    exception.ValidationExceptions.Add(new Exception("Invalid value for studentId"));
                if (parsedStudentId > 2147483647 || parsedStudentId < 1)
                    exception.ValidationExceptions.Add(
                        new Exception("studentId value should be between 1 & 2147483647 inclusive"));
                else if (!context.Users.Any(key => key.UserId == parsedStudentId && key.IsInstructor == false))
                    exception.ValidationExceptions.Add(new Exception("studentId does not exist"));
                else if (!context.Users.Any(key =>
                    key.UserId == parsedStudentId && key.IsInstructor == false && key.Archive == false))
                    exception.ValidationExceptions.Add(new Exception("Selected studentId is Archived"));
            }

            /*To check whether the Timesheet for a given homeworkId and studentId already exists */

            if (context.Timesheets.Any(key => key.StudentId == parsedStudentId && key.HomeworkId == parsedHomeworkId))
                exception.ValidationExceptions.Add(new Exception(
                    "Timesheet already exists for this homeworkId and studentId,please check with the program coordinator"));

            if (string.IsNullOrWhiteSpace(solvingTime))
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(solvingTime),
                    nameof(solvingTime) + " is null."));
            }
            else
            {
                if (!float.TryParse(solvingTime, out parsedSolvingTime))
                    exception.ValidationExceptions.Add(new Exception("Invalid value for solvingTime"));
                else if (parsedSolvingTime > 999.99 || parsedSolvingTime < 0)
                    exception.ValidationExceptions.Add(
                        new Exception("solvingTime value should be between 0 & 999.99 inclusive."));
            }

            if (!string.IsNullOrEmpty(studyTime))
            {
                if (!float.TryParse(studyTime, out parsedStudyTime))
                    exception.ValidationExceptions.Add(new Exception("Invalid value for studyTime"));
                else if (parsedStudyTime > 999.99 || parsedStudyTime < 0)
                    exception.ValidationExceptions.Add(
                        new Exception("studyTime value should be between 0 & 999.99 inclusive."));
            }

            if (exception.ValidationExceptions.Count > 0) throw exception;

            #endregion

            var newTimesheet = new Timesheet
            {
                HomeworkId = parsedHomeworkId,
                StudentId = parsedStudentId,
                SolvingTime = parsedSolvingTime,
                StudyTime = parsedStudyTime
            };

            context.Timesheets.Add(newTimesheet);

            context.SaveChanges();
        }

        /// <summary>
        ///     Update Time-sheet by TimesheetId
        ///     Description: Controller action that updates existing time-sheet by TimesheetId
        ///     It expects below parameters, and would populate the time-sheet by given homework id in the database.
        ///     Assumption:
        ///     The frontend view would populate the time-sheet information first through API call
        ///     User will edit as needed
        ///     Frontend will send update API call to backend with all keys to update database
        /// </summary>
        /// <param name="timesheetId"></param>
        /// <param name="solvingTime"></param>
        /// <param name="studyTime"></param>
        public static void UpdateTimesheetById(string timesheetId, string solvingTime, string studyTime)
        {
            var parsedTimesheetId = 0;
            float parsedSolvingTime = 0;
            float parsedStudyTime = 0;
            var exception = new ValidationException();
            using var context = new AppDbContext();

            #region Validation

            timesheetId = string.IsNullOrEmpty(timesheetId) || string.IsNullOrWhiteSpace(timesheetId)
                ? null
                : timesheetId.Trim();
            solvingTime = string.IsNullOrEmpty(solvingTime) || string.IsNullOrWhiteSpace(solvingTime)
                ? null
                : solvingTime.Trim();
            studyTime = string.IsNullOrEmpty(studyTime) || string.IsNullOrWhiteSpace(studyTime)
                ? null
                : studyTime.Trim();

            if (string.IsNullOrWhiteSpace(timesheetId))
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(timesheetId),
                    nameof(timesheetId) + " is null."));
            }
            else
            {
                if (!int.TryParse(timesheetId, out parsedTimesheetId))
                    exception.ValidationExceptions.Add(new Exception("Invalid value for timesheetId"));
                if (parsedTimesheetId > 2147483647 || parsedTimesheetId < 1)
                    exception.ValidationExceptions.Add(
                        new Exception("timesheetId value should be between 1 & 2147483647 inclusive"));
                /*If the Homework is Archived timesheet cannot be updated*/

                else if (!context.Timesheets.Any(key => key.TimesheetId == parsedTimesheetId))
                    exception.ValidationExceptions.Add(new Exception("timesheetId does not exist"));

                else if (!context.Timesheets.Any(key => key.TimesheetId == parsedTimesheetId && key.Archive == false))
                    exception.ValidationExceptions.Add(new Exception("Selected timesheetId is Archived"));
            }

            if (string.IsNullOrWhiteSpace(solvingTime))
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(solvingTime),
                    nameof(solvingTime) + " is null."));
            }
            else
            {
                if (!float.TryParse(solvingTime, out parsedSolvingTime))
                    exception.ValidationExceptions.Add(new Exception("Invalid value for solvingTime"));
                else if (parsedSolvingTime > 999.99 || parsedSolvingTime < 0)
                    exception.ValidationExceptions.Add(
                        new Exception("solvingTime value should be between 0 & 999.99 inclusive."));
            }

            if (!string.IsNullOrEmpty(studyTime))
            {
                if (!float.TryParse(studyTime, out parsedStudyTime))
                    exception.ValidationExceptions.Add(new Exception("Invalid value for studyTime"));
                else if (parsedStudyTime > 999.99 || parsedStudyTime < 0)
                    exception.ValidationExceptions.Add(
                        new Exception("studyTime value should be between 0 & 999.99 inclusive."));
            }

            if (exception.ValidationExceptions.Count > 0) throw exception;

            #endregion

            var timesheet = context.Timesheets.Find(parsedTimesheetId);
            timesheet.SolvingTime = parsedSolvingTime;
            timesheet.StudyTime = parsedStudyTime;

            context.SaveChanges();
        }

        /// <summary>
        ///     GetTimesheetByHomeworkId
        ///     This Action returns Timesheet of a specified student for a specified Homework.
        /// </summary>
        /// <param name="homeworkId">Homework Id</param>
        /// <param name="studentId">Student Id</param>
        /// <returns>Record from Timesheet Table</returns>
        public static Timesheet GetTimesheetByHomeworkId(string homeworkId, string studentId)
        {
            var parsedHomeworkId = 0;
            var parsedStudentId = 0;
            var exception = new ValidationException();
            using var context = new AppDbContext();

            #region Validation

            homeworkId = string.IsNullOrEmpty(homeworkId) || string.IsNullOrWhiteSpace(homeworkId)
                ? null
                : homeworkId.Trim();
            studentId = string.IsNullOrEmpty(studentId) || string.IsNullOrWhiteSpace(studentId)
                ? null
                : studentId.Trim();

            if (string.IsNullOrWhiteSpace(homeworkId))
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(homeworkId),
                    nameof(homeworkId) + " is null."));
            }
            else
            {
                if (!int.TryParse(homeworkId, out parsedHomeworkId))
                    exception.ValidationExceptions.Add(new Exception("Invalid value for homeworkId"));
                if (parsedHomeworkId > 2147483647 || parsedHomeworkId < 1)
                    exception.ValidationExceptions.Add(
                        new Exception("homeworkId value should be between 1 & 2147483647 inclusive"));
                else if (!context.Homeworks.Any(key => key.HomeworkId == parsedHomeworkId))
                    exception.ValidationExceptions.Add(new Exception("homeworkId does not exist"));
            }

            if (string.IsNullOrWhiteSpace(studentId))
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(studentId),
                    nameof(studentId) + " is null."));
            }
            else
            {
                if (!int.TryParse(studentId, out parsedStudentId))
                    exception.ValidationExceptions.Add(new Exception("Invalid value for studentId"));
                if (parsedStudentId > 2147483647 || parsedStudentId < 1)
                    exception.ValidationExceptions.Add(
                        new Exception("studentId value should be between 1 & 2147483647 inclusive"));

                else if (!context.Users.Any(key => key.UserId == parsedStudentId && key.IsInstructor == false))
                    exception.ValidationExceptions.Add(new Exception("studentId does not exist"));
            }

            if (exception.ValidationExceptions.Count > 0) throw exception;

            #endregion

            var timesheet = new Timesheet();

            if (context.Timesheets.Any(key => key.StudentId == parsedStudentId && key.HomeworkId == parsedHomeworkId))
                timesheet = context.Timesheets.SingleOrDefault(key =>
                    key.HomeworkId == parsedHomeworkId && key.StudentId == parsedStudentId);
            else
                /*In HomeworkView for Students if there is no timesheet created for a Homework it will display dummy data  */

                timesheet = new Timesheet
                {
                    TimesheetId = 0,
                    HomeworkId = parsedHomeworkId,
                    StudentId = parsedStudentId,
                    SolvingTime = 0,
                    StudyTime = 0
                };

            return timesheet;
        }

        /// <summary>
        ///     ArchiveTimesheetById
        ///     Description: This action archives rubrics by timesheetId PK
        /// </summary>
        /// <param name="timesheetId"></param>
        public static void ArchiveTimesheetById(string timesheetId)
        {
            var parsedTimesheetId = 0;
            var exception = new ValidationException();
            using var context = new AppDbContext();

            #region Validation

            timesheetId = string.IsNullOrEmpty(timesheetId) || string.IsNullOrWhiteSpace(timesheetId)
                ? null
                : timesheetId.Trim();

            if (string.IsNullOrWhiteSpace(timesheetId))
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(timesheetId),
                    nameof(timesheetId) + " is null."));
            }
            else
            {
                if (!int.TryParse(timesheetId, out parsedTimesheetId))
                    exception.ValidationExceptions.Add(new Exception("Invalid value for timesheetId"));
                if (parsedTimesheetId > 2147483647 || parsedTimesheetId < 1)
                    exception.ValidationExceptions.Add(
                        new Exception("timesheetId value should be between 1 & 2147483647 inclusive"));
                else if (!context.Timesheets.Any(key => key.TimesheetId == parsedTimesheetId))
                    exception.ValidationExceptions.Add(new Exception("timesheetId does not exist"));
                else if (!context.Timesheets.Any(key => key.TimesheetId == parsedTimesheetId && key.Archive == false))
                    exception.ValidationExceptions.Add(new Exception("Selected timesheetId is already archived"));
            }

            if (exception.ValidationExceptions.Count > 0) throw exception;

            #endregion

            var timesheet = context.Timesheets.Find(parsedTimesheetId);
            timesheet.Archive = true;

            context.SaveChanges();
        }
    }
}