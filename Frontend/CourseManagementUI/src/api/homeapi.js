import axios  from "axios"


const ServerUrl = "https://localhost:7235/api/course"

export async function Enroll(info) {
     const response = await axios.post(`${ServerUrl}/enroll`, info);
     console.log("Enroll();")
     return response.data;
}


export async function  UnEnroll(info) {
     const response = await axios.post(`${ServerUrl}/unenroll`, info);
     console.log("UnEnroll();")
     return response.data;
}
