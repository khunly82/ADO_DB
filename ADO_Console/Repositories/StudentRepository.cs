using ADO_Console.Domain;
using System;
using System.Collections.Generic;
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
                // on crée une instance de Student
                Student student = new Student();
                // on remplit l'instance avec les données de la db
                student.LastName = (string)reader["LastName"];
                student.FirstName = (string)reader["FirstName"];
                student.BirthDate = (DateTime)reader["BirthDate"];
                student.YearResult = (int)reader["YearResult"];
                student.SectionId = (int)reader["SectionId"];

                // on ajoute l'etudiant dans la liste
                result.Add(student);
            }

            _connection.Close();
            return result;
        }

        public Student Get(int id)
        {
            throw new NotImplementedException();
        }

        public int Add(Student student) 
        {
            throw new NotImplementedException();
        }

        public void Remove(Student student)
        {
            throw new NotImplementedException();
        }

        public void Update(Student student)
        {
            throw new NotImplementedException();
        }
    }
}
