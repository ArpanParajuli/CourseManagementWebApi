import { useEffect, useState } from "react";
import { GetAllCourses } from "../api/CourseApi";
import { Enroll } from "../api/homeapi";

function Home() {
  const [courses, setCourses] = useState([]);
  const [selectedCourseId, setSelectedCourseId] = useState(null);
  const [studentId, setStudentId] = useState("");
  const [isModalOpen, setIsModalOpen] = useState(false);

  useEffect(() => {
    async function loadCourses() {
      const response = await GetAllCourses();
      setCourses(response);
    }
    loadCourses();
  }, []);

  async function HandleEnroll() {
    const payload = { studentid: Number(studentId), courseid: selectedCourseId };
     console.log("Payload:", payload);
    
     await Enroll(payload);

    setIsModalOpen(false);
    setStudentId("");
    setSelectedCourseId(null);
  }

  return (
    <>
      <div className="p-6 grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6">
        {courses.map((course) => (
          <div
            key={course.id}
            className="border rounded-xl p-5 bg-white shadow-sm"
          >
            <h3 className="text-xl font-semibold mb-2">{course.name}</h3>

            <p className="text-gray-600 mb-4">{course.description}</p>

            <button
              onClick={() => {
                setSelectedCourseId(course.id);
                setIsModalOpen(true);
              }}
              className="px-4 py-2 bg-black text-white rounded-lg hover:bg-gray-800"
            >
              Enroll
            </button>
          </div>
        ))}
      </div>


      {isModalOpen && (
        <div className="fixed inset-0 bg-black bg-opacity-40 flex justify-center items-center">
          <div className="bg-white p-6 rounded-xl w-80 shadow-lg">
            <h2 className="text-lg font-semibold mb-3">
              Enter Student ID to Enroll
            </h2>

            <input
              type="number"
              value={studentId}
              onChange={(e) => setStudentId(e.target.value)}
              className="w-full border px-3 py-2 rounded mb-4"
              placeholder="Student ID"
            />

            <div className="flex justify-end gap-3">
              <button
                onClick={() => setIsModalOpen(false)}
                className="px-4 py-2 bg-gray-300 rounded hover:bg-gray-400"
              >
                Cancel
              </button>

              <button
                onClick={HandleEnroll}
                className="px-4 py-2 bg-black text-white rounded hover:bg-gray-800"
              >
                Enroll
              </button>
            </div>
          </div>
        </div>
      )}
    </>
  );
}

export default Home;
