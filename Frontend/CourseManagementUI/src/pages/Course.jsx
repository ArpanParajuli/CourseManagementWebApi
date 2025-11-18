import { useEffect, useState } from "react";
import { GetAllCourses , AddCourse , DeleteCourseById} from "../api/CourseApi";

export default function Courses() {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [courseName, setCourseName] = useState("");
  const [description, setDescription] = useState("");
  const [courses, setCourses] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    async function fetchCourses() {
      try {
        const response = await GetAllCourses();
        console.log("API Response:", response);
        setCourses(response || []);
      } catch (err) {
        console.error(err);
        setError("Failed to load courses.");
        setCourses([]);
      } finally {
        setLoading(false);
      }
    }


  

    fetchCourses();
  }, []);

  async function handleAddCourse(e) {
    e.preventDefault();

    const payload = { name: courseName, description };
    console.log("Course Created:", payload);

    await AddCourse(payload)
   
    setCourseName("");
    setDescription("");

    setIsModalOpen(false);


    setCourses((prev) => [...prev, { id: prev.length + 1, ...payload }]);
  }


  async function HandleDeleteCourse(courseid) {
    
    await DeleteCourseById(courseid);
  }

  return (
    <div className="p-8">
   
      <div className="flex items-center justify-between mb-6">
        <h1 className="text-3xl font-bold">Courses</h1>
        <button
          onClick={() => setIsModalOpen(true)}
          className="bg-blue-600 hover:bg-blue-700 text-white px-4 py-2 rounded-lg"
        >
          + Add Course
        </button>
      </div>

      {/* Course List */}
      <div className="bg-white shadow-md rounded-xl p-6">
        <h2 className="text-xl font-semibold mb-4">Course List</h2>
        <table className="w-full border-collapse">
          <thead>
            <tr className="bg-gray-100 text-left">
              <th className="p-3 font-semibold border-b">ID</th>
              <th className="p-3 font-semibold border-b">Name</th>
              <th className="p-3 font-semibold border-b">Enrolled students</th>
              <th className="p-3 font-semibold border-b">Actions</th>
            </tr>
          </thead>
          <tbody>
            {loading && (
              <tr>
                <td colSpan="4" className="text-center py-4">
                  Loading courses...
                </td>
              </tr>
            )}
            {error && (
              <tr>
                <td colSpan="4" className="text-center py-4 text-red-500">
                  {error}
                </td>
              </tr>
            )}
            {!loading &&
              !error &&
              courses?.map((course) => (
                <tr key={course.id} className="border-b">
                  <td className="p-3">{course.id}</td>
                  <td className="p-3">{course.name}</td>
                 <td className="p-3">
  {course.students && course.students.length > 0 ? (
    course.students.map((student) => (
      <span key={student.id} className="inline-block mr-2">
        {student.fullName}
      </span>
    ))
  ) : (
    <span className="text-gray-400">No students</span>
  )}
</td>
                 
                  <td className="p-3 space-x-3">
                    <button className="px-3 py-1 bg-yellow-500 text-white rounded-lg hover:bg-yellow-600">
                      Edit
                    </button>
                    <button className="px-3 py-1 bg-red-600 text-white rounded-lg hover:bg-red-700" onClick={()=>HandleDeleteCourse(course.id)}>
                      Delete
                    </button>
                  </td>
                </tr>
              ))}
          </tbody>
        </table>
      </div>

      {/* Add Course Modal */}
      {isModalOpen && (
        <div className="fixed inset-0 bg-black bg-opacity-40 flex items-center justify-center">
          <div className="bg-white p-6 rounded-lg w-96 shadow-lg">
            <h2 className="text-xl font-semibold mb-4">Add New Course</h2>
            <form onSubmit={handleAddCourse}>
              <label className="block mb-3">
                <span className="text-gray-700">Course Name</span>
                <input
                  type="text"
                  className="w-full mt-1 px-3 py-2 border rounded"
                  value={courseName}
                  onChange={(e) => setCourseName(e.target.value)}
                  required
                />
              </label>
              <label className="block mb-3">
                <span className="text-gray-700">Description</span>
                <textarea
                  className="w-full mt-1 px-3 py-2 border rounded"
                  rows="3"
                  value={description}
                  onChange={(e) => setDescription(e.target.value)}
                  required
                ></textarea>
              </label>
              <div className="flex justify-end mt-4 gap-2">
                <button
                  type="button"
                  className="px-3 py-1 bg-gray-300 rounded hover:bg-gray-400"
                  onClick={() => setIsModalOpen(false)}
                >
                  Cancel
                </button>
                <button
                  type="submit"
                  className="px-4 py-1 bg-green-600 text-white rounded hover:bg-green-700"
                >
                  Create
                </button>
              </div>
            </form>
          </div>
        </div>
      )}
    </div>
  );
}
