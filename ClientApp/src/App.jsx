import React from "react";
import { BrowserRouter as Router, Route, Switch } from "react-router-dom";
import Header from "./components/shared/Header/Header";
import Login from "./screens/Login/Login";
import Register from "./components/Instructor/Register/Register";
import CourseSummaryStudent from "./components/Student/CourseSummaryStudent/CourseSummaryStudent";
import CourseSummaryInstructor from "./components/Instructor/CourseSummaryInstructor/CourseSummaryInstructor";
import HomeworkSummaryStudent from "./components/Student/HomeworkSummaryStudent/HomeworkSummaryStudent";
import HomeworkStudent from "./components/Student/HomeworkStudent/HomeworkStudent";
import GradesStudent from "./components/Student/GradesStudent/GradesStudent";
import CohortCreate from "./components/Instructor/CohortCreate/CohortCreate";
import CohortEdit from "./components/Instructor/CohortEdit/CohortEdit";
import CohortSummary from "./components/Instructor/CohortSummary/CohortSummary";
import GradingSummary from "./components/Instructor/GradingSummary/GradingSummary";
import CourseCreate from "./components/Instructor/CourseCreate/CourseCreate";
import CourseEdit from "./components/Instructor/CourseEdit/CourseEdit";
import ManageCourse from "./components/Instructor/ManageCourses/ManageCourses";
import CourseEditAssigned from "./components/Instructor/CourseEditAssigned/CourseEditAssigned";
import CourseAssign from "./components/Instructor/CourseAssign/CourseAssign";
import HomeworkSummaryInstructor from "./components/Instructor/HomeworkSummaryInstructor/HomeworkSummaryInstructor";
import HomeworkViewInstructor from "./components/Instructor/HomeworkViewInstructor/HomeworkViewInstructor";
import HomeworkCreateInstructor from "./components/Instructor/HomeworkCreateInstructor/HomeworkCreateInstructor";
const App = () => {
  return (
    <React.Fragment>
      <Router>
        <Header />
        <Switch>
          <Route path="/" exact component={Login} />
          <Route
            path="/student/:userId"
            exact
            component={CourseSummaryStudent}
          />
          <Route
            path="/coursesummary/:id"
            exact
            component={CourseSummaryInstructor}
          />
          <Route
            path="/studenthomework/:studentId/:courseId"
            exact
            component={HomeworkSummaryStudent}
          />
          <Route
            path="/instructorhomework/:id/:courseId"
            exact
            component={HomeworkSummaryInstructor}
          />
          <Route
            path="/instructorcreatehomework/:id"
            exact
            component={HomeworkCreateInstructor}
          />
          <Route
            path="/homeworkcardstudent/:studentId/:homeworkId"
            exact
            component={HomeworkStudent}
          />
          <Route path="/gradesstudent" exact component={GradesStudent} />
          <Route path="/cohortcreate" exact component={CohortCreate} />
          <Route path="/cohortedit/:id" exact component={CohortEdit} />
          <Route path="/cohortsummary" exact component={CohortSummary} />
          <Route
            path="/gradingsummary/:cohortId/:homeworkId/:courseId"
            exact
            component={GradingSummary}
          />
          <Route path="/coursecreate" exact component={CourseCreate} />
          <Route path="/courseedit/:id" exact component={CourseEdit} />
          <Route
            path="/CourseSummaryStudent"
            exact
            component={CourseSummaryStudent}
          />
          <Route path="/managecourse" exact component={ManageCourse} />
          <Route path="/registeruser" exact component={Register} />
          <Route path="/courseassign/:id" exact component={CourseAssign} />
          <Route
            path="/courseeditassigned/:id/:courseId"
            exact
            component={CourseEditAssigned}
          />
          <Route
            path="/homeworkviewinstructor/:id"
            exact
            component={HomeworkViewInstructor}
          />
        </Switch>
      </Router>
    </React.Fragment>
  );
};

export default App;
