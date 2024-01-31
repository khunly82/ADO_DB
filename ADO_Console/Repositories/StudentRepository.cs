using ADO_Console.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO_Console.Repositories
{
    public class StudentRepository
    {
        private SqlConnection _connection;

        public StudentRepository(
            SqlConnection connection
        ) {
            _connection = connection;
        }

        // CRUD
        public List<Student> Get()
        {
            _connection.Open();

            using SqlCommand command = _connection.CreateCommand();
            command.CommandText = "SELECT * FROM V_Student";
            using SqlDataReader reader = command.ExecuteReader();

            // on crée une liste vide de Student
            List<Student> result = new List<Student>();

            // pour chaque ligne de la requète
            while (reader.Read())
            {
                Student student = ReaderToStudent(reader);
                // on ajoute l'etudiant dans la liste
                result.Add(student);
            }

            _connection.Close();
            return result;
        }

        public Student? Get(int id)
        {
            _connection.Open();

            using SqlCommand command = _connection.CreateCommand();
            command.CommandText = "SELECT * FROM V_Student WHERE Id = @id";
            command.Parameters.AddWithValue("id", id);
            using SqlDataReader reader = command.ExecuteReader();

            // pour chaque ligne de la requète
            if (reader.Read())
            {
                Student student = ReaderToStudent(reader);
                // on ajoute l'etudiant dans la liste
                _connection.Close();
                return student;
            } 
            else
            {
                _connection.Close();
                return null;
            }
        }

        public int Add(Student student) 
        {
            _connection.Open();

            using SqlCommand command = _connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO Student (LastName,FirstName,BirthDate,YearResult,sectionId) 
                OUTPUT inserted.Id 
                VALUES(@lastName, @firstName, @birthDate, @yearResult, @sectionId)
            ";
            command.Parameters.AddWithValue("lastName", student.LastName);
            command.Parameters.AddWithValue("firstName", student.FirstName);
            command.Parameters.AddWithValue("birthDate", student.BirthDate);
            command.Parameters.AddWithValue("yearResult", student.YearResult ?? (object) DBNull.Value);
            command.Parameters.AddWithValue("sectionId", student.SectionId);
            int id = (int)command.ExecuteScalar();

            _connection.Close();
            return id;
        }

        public void Remove(Student student)
        {
            _connection.Open();

            using SqlCommand cmd = _connection.CreateCommand();
            cmd.CommandText = "DELETE FROM student WHERE Id = @id";
            cmd.Parameters.AddWithValue("id", student.Id);
            cmd.ExecuteNonQuery();

            _connection.Close();
        }

        public void Update(Student student)
        {
            _connection.Open();

            using SqlCommand cmd = _connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "UpdateStudent";
            cmd.Parameters.AddWithValue("SectionID", student.SectionId);
            cmd.Parameters.AddWithValue("YearResult", student.YearResult);
            cmd.Parameters.AddWithValue("Id", student.Id);
            cmd.ExecuteNonQuery();

            _connection.Close();
        }

        private Student ReaderToStudent(SqlDataReader reader)
        {
            // on crée une instance de Student
            Student student = new Student();
            // on remplit l'instance avec les données de la db
            student.Id = (int)reader["Id"];
            student.LastName = (string)reader["LastName"];
            student.FirstName = (string)reader["FirstName"];
            student.BirthDate = (DateTime)reader["BirthDate"];
            student.YearResult = reader["YearResult"] as int?;
            student.SectionId = (int)reader["SectionId"];
            return student;
        }
    }
}
