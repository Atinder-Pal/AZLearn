import { combineReducers } from "redux";
import {
  cohortSummaryInstructorReducer,
  cohortCreateReducer,
  cohortEditReducer,
  courseCreateReducer,
  courseEditReducer,
  homeworkSummaryInstructorReducer,
  cohortGetStateReducer,
  manageCourseReducer,
  getCourseReducer,
  cohortArchiveReducer,
  courseArchiveReducer,
  getCoursesByCohortIdReducer,
  getAllCoursesReducer,
  getAllInstructorsReducer,
  courseAssignReducer,
  editAssignedCourseReducer,
  getAssignedCourseReducer,
  homeworkDetailInstructorReducer,
  assignedCourseArchiveReducer,
  gradeSummaryInstructorReducer,
} from "./instructorReducer";
import {
  courseSummaryStudentReducer,
  homeworkSummaryStudentReducer,
  homeworkStudentReducer,
  createTimeSheetStudentReducer,
  updateTimeSheetStudentReducer,
} from "./studentReducer";

const rootReducers = combineReducers({
  cohortSummaryInstructor: cohortSummaryInstructorReducer,
  courseSummaryStudent: courseSummaryStudentReducer,
  homeworkSummaryStudent: homeworkSummaryStudentReducer,
  homeworkStudent: homeworkStudentReducer,
  createTimeSheetStudent: createTimeSheetStudentReducer,
  updateTimeSheetStudent: updateTimeSheetStudentReducer,
  cohortCreate: cohortCreateReducer,
  cohortEdit: cohortEditReducer,
  courseCreate: courseCreateReducer,
  courseEdit: courseEditReducer,
  homeworkSummaryInstructor: homeworkSummaryInstructorReducer,
  cohortGetState: cohortGetStateReducer,
  manageCourse: manageCourseReducer,
  getCourse: getCourseReducer,
  cohortArchive: cohortArchiveReducer,
  courseArchive: courseArchiveReducer,
  getCoursesByCohortId: getCoursesByCohortIdReducer,
  getAllCourses: getAllCoursesReducer,
  getAllInstructors: getAllInstructorsReducer,
  courseAssign: courseAssignReducer,
  editAssignedCourse: editAssignedCourseReducer,
  getAssignedCourse: getAssignedCourseReducer,
  homeworkDetailInstructor: homeworkDetailInstructorReducer,
  assignedCourseArchive: assignedCourseArchiveReducer,
  gradeSummaryInstructor: gradeSummaryInstructorReducer,
});

export default rootReducers;
