import axios from 'axios';


const ServerUrl = "https://localhost:7235/api"

export async function GetAllStudents()
{
   const response = await axios.get(`${ServerUrl}/student`);
   console.log("GetAllStudent api is called!");
   return response.data;
}

export async function GetAllStudentCourseById(studentid) {
    const response = await axios.post(`${ServerUrl}/student/${studentid}/courses`);
    console.log("GetAllStudentCourseById();");
    return response.data;
}

export async function DeleteStudent(studentid) {
   const response = await axios.delete(`${ServerUrl}/student/delete/${studentid}`);
   console.log("Student delete func called!");
   return response.data;
}

export async function AddStudent(studentdata) {
    const response = await axios.post(`${ServerUrl}/student/create` , studentdata)
    console.log("student create();")
    return response.data;
}