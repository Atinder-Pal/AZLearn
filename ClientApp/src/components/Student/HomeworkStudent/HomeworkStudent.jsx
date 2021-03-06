import React, { useState } from "react";
import { Table, Container, Button, Form, Row, Col } from "react-bootstrap";
import { useEffect, useRef } from "react";
import { useDispatch, useSelector } from "react-redux";
import {
  updateTimeSheetStudent,
  homeworkStudent,
  createTimeSheetStudent,
  getHomeworkTimesheetStudent,
} from "../../../actions/studentActions";
import {
  getAllCourses,
  getAllInstructors,
} from "../../../actions/instructorActions";
import Loader from "../../shared/Loader/Loader";

const HomeworkStudent = ({ match, history }) => {
  const studentId = match.params.studentId;
  const homeworkId = match.params.homeworkId;
  const [solvingHrs, setSolvingHrs] = useState("");
  const [studyHrs, setStudyHrs] = useState("");
  const [courseId, setCourseId] = useState("");
  const [instructorId, setInstructorId] = useState("");
  const dispatch = useDispatch();

  /*Add validation states*/
  const [validated, setValidated] = useState(false);
  //----------------------------
  /*Anti-tamper validation - States and Variables*/
  const [validData, setValidData] = useState(false);
  const [formSubmitted, setFormSubmitted] = useState(false);
  let validFormData = false;
  let formSubmitIndicator = false;
  
  const { homework, loading } = useSelector((state) => state.homeworkStudent);
  const { loading: loadingUpdate, success: successUpdate, error } = useSelector(
    (state) => state.updateTimeSheetStudent
  );
  const { courses } = useSelector((state) => state.getAllCourses);
  const { instructors } = useSelector((state) => state.getAllInstructors);
  const { timeSheet, loading: loadingTimesheet } = useSelector(
    (state) => state.getTimeSheetStudent
  );

  useEffect(() => {
    dispatch(homeworkStudent(homeworkId));
    dispatch(getAllCourses());
    dispatch(getAllInstructors());
    if (
      !timeSheet ||
      !timeSheet.item2 ||
      timeSheet.item1.homeworkId != homeworkId
    ) {
      dispatch(getHomeworkTimesheetStudent({ homeworkId, studentId }));
    } else {
      setSolvingHrs(timeSheet.item2.solvingTime);
      setStudyHrs(timeSheet.item2.studyTime);
    }
  }, [dispatch, homeworkId, loadingTimesheet]);

  /*Anti-tamper validation - Validate (parameters)*/
  function Validate(solvingHrs, studyHrs) {
    formSubmitIndicator = true;
    try {
      solvingHrs = String(solvingHrs).trim();
      studyHrs = String(studyHrs).trim();

      if (!solvingHrs) {
        validFormData = false;
      } else if (
        parseFloat(solvingHrs) > 999.99 ||
        parseFloat(solvingHrs) < 0
      ) {
        validFormData = false;
      } else if ( studyHrs && (parseFloat(studyHrs) > 999.99 || parseFloat(studyHrs) < 0)) {
        validFormData = false;
      } else {
        validFormData = true;
      }
    } catch (Exception) {
      validFormData = false;
    }
  }
 

  const summitHandler = (e) => {
   /*Add form validation condition block if-else*/
    const form = e.currentTarget;
    if (form.checkValidity() === false) {
      e.preventDefault();
      e.stopPropagation();
    }
    setValidated(true);
    

    /*Anti-tamper validation - Alert message conditions*/
    validFormData = false;
    formSubmitIndicator = true;
    setValidData(validFormData);
    
    e.preventDefault();

     /*Anti-tamper validation - calling Validate*/
     Validate(solvingHrs, studyHrs);
    if (validFormData) {
      setValidData(validFormData);
    
      e.preventDefault();
    dispatch(
      updateTimeSheetStudent(timeSheet.item2.timesheetId, solvingHrs, studyHrs)
    );
  } else {
    /*Anti-tamper validation - Alert message conditions*/
    setValidData(validFormData);
  }
  /*Anti-tamper validation - Alert message conditions*/
  setFormSubmitted(formSubmitIndicator);
  
};

  const goBack = () => {
    history.goBack();
  };

  return (
    <React.Fragment>
      {homework.length < 1 && timeSheet ? (
        <Loader />
      ) : (
        <Container>
          <Row className="justify-content-md-center">
            <Col xs={12} md={6}>
              <h3>Homework</h3>
              {/* Anti-tamper validation - Alert message conditions   */}
              <p
                class={
                  formSubmitted
                    ? validData
                      ? !loading && error
                        ? "alert alert-danger"
                        : !loading && !error && successUpdate
                        ? "alert alert-success"
                        : ""
                      : "alert alert-danger"
                    : ""
                }
                role="alert"
              >
                {formSubmitted
                  ? validData
                    ? !loading && error
                      ? `Unsuccessful attempt to update Timesheet. ${error.data}`
                      : !loading && !error && successUpdate
                      ? "Timesheet was successfully updated"
                      : ""
                    : "Error: Form was submitted with invalid data fields"
                  : ""}
              </p>
              
              <Form>
                <Form.Group controlId="title">
                  <Form.Label>Title</Form.Label>
                  <Form.Control disabled value={homework.Title}></Form.Control>
                </Form.Group>
                <Form.Group controlId="Course">
                  <Form.Label>Course</Form.Label>
                  <Form.Control as="select" required value={courseId} disabled>
                    <option value="">{homework.CourseName}</option>
                    {courses.map((course, index) => (
                      <option value={course.courseId} key={index}>
                        {course.name}
                      </option>
                    ))}
                  </Form.Control>
                </Form.Group>
                <Form.Group controlId="instructor">
                  <Form.Label>Instructor</Form.Label>
                  <Form.Control
                    disabled
                    as="select"
                    required
                    value={instructorId}
                  >
                    <option value="">{homework.InstructorName}</option>
                    {instructors.map((instructor, index) => (
                      <option value={instructor.userId} key={index}>
                        {instructor.name}
                      </option>
                    ))}
                  </Form.Control>
                </Form.Group>

                <Form.Group controlId="Avg Completion Time">
                  <Form.Label>Avg Completion Time</Form.Label>
                  <Form.Control
                    disabled
                    value={homework.AvgCompletionTime}
                  ></Form.Control>
                </Form.Group>

                <Form.Group controlId="Due Date">
                  <Form.Label>Due Date</Form.Label>
                  <Form.Control
                    disabled
                    value={homework.DueDate}
                  ></Form.Control>
                </Form.Group>

                <Form.Group controlId="Release Date">
                  <Form.Label>Release Date</Form.Label>
                  <Form.Control
                    disabled
                    value={homework.ReleaseDate}
                  ></Form.Control>
                </Form.Group>

                <Form.Group controlId="DocLink">
                  <Form.Label>DocLink</Form.Label>
                  <Form.Control
                    disabled
                    value={homework.DocumentLink}
                  ></Form.Control>
                </Form.Group>
                <Form.Group controlId="GitHubLink">
                  <Form.Label>GitHubLink</Form.Label>
                  <Form.Control
                    disabled
                    value={homework.GitHubClassRoomLink}
                  ></Form.Control>
                </Form.Group>
              </Form>
              <Form noValidate validated={validated} onSubmit={summitHandler}>
                <h3>Timesheet</h3>
                <Form.Group controlId="Solving/Troubleshooting">
                  <Form.Label>Solving/Troubleshooting</Form.Label>
                  <Form.Control
                    required
                    type="number"
                    min={0}
                    max={999.99}
                    step="0.25"
                    value={solvingHrs}
                    onChange={(e) => setSolvingHrs(String(e.target.value))}
                  ></Form.Control>
                  <Form.Control.Feedback type="invalid">
                    Please fill in Solving/Troubleshooting Hours. 
                    <p>Range: 0 - 999.99 inclusive</p>                    
                  </Form.Control.Feedback>
                </Form.Group>
                <Form.Group controlId="Study/Research">
                  <Form.Label>Study/Research</Form.Label>
                  <Form.Control
                    type="number"
                    min={0}
                    max={999.99}
                    step="0.25"
                    value={studyHrs}
                    onChange={(e) => setStudyHrs(String(e.target.value))}
                  ></Form.Control>
                  <Form.Control.Feedback type="invalid">
                    Please fill Study/Research Hours in a valid format.
                    <p>Range: 0 - 999.99 inclusive</p>
                  </Form.Control.Feedback>
                </Form.Group>
                <Form.Group controlId="Total">
                  <Form.Label>Total</Form.Label>
                  <Form.Control
                    disabled
                    value={Number(studyHrs) + Number(solvingHrs)}
                  ></Form.Control>
                </Form.Group>
                <button type="button" className="btn btn-link" onClick={goBack}>
                  Back
                </button>{" "}
                <Button type="submit" variant="primary" className="float-right">
                  Save
                </Button>
              </Form>
            </Col>
          </Row>
        </Container>
      )}
    </React.Fragment>
  );
};

export default HomeworkStudent;
