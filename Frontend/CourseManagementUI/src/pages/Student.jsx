import { useEffect, useState } from 'react';
import {
  GetAllStudents,
  GetAllStudentCourseById,
  DeleteStudent,
  AddStudent
} from '../api/studentapi.js';

export default function Students() {
  const [students, setStudents] = useState([]);
  const [studentEmail, setStudentEmail] = useState("");
  const [studentFullName, setStudentFullName] = useState("");
  const [isModalOpen, setIsModalOpen] = useState(false);


  const loadStudents = async () => {
    try {
      const response = await GetAllStudents();
      setStudents(response);
    } catch (err) {
      console.log(err);
    }
  };

  useEffect(() => {
    loadStudents();
  }, []);


  const handleCreateStudent = async (e) => {
    e.preventDefault();
    const payload = {
      fullName: studentFullName,
      email: studentEmail
    };

    try {
      await AddStudent(payload);
      setStudentFullName("");
      setStudentEmail("");
      setIsModalOpen(false);
      loadStudents(); // refresh list
    } catch (err) {
      console.log(err);
    }
  };

  const handleDelete = async (id) => {
    await DeleteStudent(id);
    loadStudents();
  };

  return (
    <>
      <div className="overflow-x-auto">
        <button
          className="bg-green-600 text-white px-4 py-2 rounded mb-4"
          onClick={() => setIsModalOpen(true)}
        >
          + Add Student
        </button>

        <table className="min-w-full bg-white shadow rounded-lg">
          <thead className="bg-blue-500 text-white">
            <tr>
              <th className="py-2 px-4 text-left">ID</th>
              <th className="py-2 px-4 text-left">Name</th>
              <th className="py-2 px-4 text-left">Email</th>
              <th className="py-2 px-4 text-left">Courses</th>
              <th className="py-2 px-4 text-left">Actions</th>
            </tr>
          </thead>
          <tbody>
            {students.map((student) => (
              <tr key={student.id} className="border-b hover:bg-gray-100">
                <td className="py-2 px-4">{student.id}</td>
                <td className="py-2 px-4">{student.fullName}</td>
                <td className="py-2 px-4">{student.email}</td>
                <td className="py-2 px-4">
                  {student.courses.map((course) => (
                    <span
                      key={course.id}
                      className="inline-block bg-gray-200 text-gray-700 px-2 py-1 rounded mr-1 mb-1 text-sm"
                    >
                      {course.name}
                    </span>
                  ))}
                </td>
                <td className="py-2 px-4">
                  <button className="bg-blue-500 text-white px-3 py-1 rounded hover:bg-blue-600 mr-2">
                    Edit
                  </button>
                  <button
                    className="bg-red-500 text-white px-3 py-1 rounded hover:bg-red-600"
                    onClick={() => handleDelete(student.id)}
                  >
                    Delete
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>

      {/* CREATE MODAL */}
      {isModalOpen && (
        <div className="fixed inset-0 bg-black bg-opacity-40 flex items-center justify-center">
          <div className="bg-white p-6 rounded-lg w-96 shadow-lg">
            <h2 className="text-xl font-semibold mb-4">Create New Student</h2>

            <form onSubmit={handleCreateStudent}>
              <label className="block mb-3">
                <span className="text-gray-700">Full Name</span>
                <input
                  type="text"
                  className="w-full mt-1 px-3 py-2 border rounded"
                  value={studentFullName}
                  onChange={(e) => setStudentFullName(e.target.value)}
                  required
                />
              </label>

              <label className="block mb-3">
                <span className="text-gray-700">Email</span>
                <input
                  type="email"
                  className="w-full mt-1 px-3 py-2 border rounded"
                  value={studentEmail}
                  onChange={(e) => setStudentEmail(e.target.value)}
                  required
                />
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
    </>
  );
}
