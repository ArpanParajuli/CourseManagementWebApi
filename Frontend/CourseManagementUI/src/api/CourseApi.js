import axios from "axios";

const ServerUrl = "https://localhost:7235/api/course"

export async function AddCourse(coursedetails) {
    const response = await axios.post(`${ServerUrl}/create` , coursedetails)
    console.log("create course();")
    return response.data;
}

export async function GetAllCourses() {
  const response = await axios.get(`${ServerUrl}`);
  console.log("GetAllCourses();")
  return response.data;
}

export async function DeleteCourseById(CourseId) {
  const response = await axios.delete(`${ServerUrl}/${CourseId}`);
  console.log("delete course();")
  return response.data;
}