using ADO_Console.Domain;
using System.Data;
using System.Data.SqlClient;

string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TFDemoADO;Integrated Security=True;";

//Utilisation du bloc d'instruction using(){} pour que l'objet soit
//disposé (détruit/nettoyé/viré a coup de pied au cul) après son utilisation

#region Exemple
//using (SqlConnection cnx = new SqlConnection(connectionString))
//{
//    Console.WriteLine(cnx.State);
//    cnx.Open();
//    Console.WriteLine(cnx.State);
//    cnx.Close();
//    Console.WriteLine(cnx.State);

//    //SqlCommand cmd = new SqlCommand();
//    //cmd.Connection = cnx;

//    using (SqlCommand cmd = cnx.CreateCommand())
//    {
//        string sql = "SELECT LastName FROM Student WHERE Id = 1";
//        cmd.CommandText = sql;

//        cnx.Open();
//        string lastName = cmd.ExecuteScalar().ToString();
//        cnx.Close();
//        Console.WriteLine(lastName);

//    }

//    using (SqlCommand cmd = cnx.CreateCommand())
//    {
//        string sql = "SELECT LastName FROM Student";
//        cmd.CommandText = sql;

//        cnx.Open();
//        using (SqlDataReader reader = cmd.ExecuteReader())
//        {
//            while (reader.Read())
//            {
//                Console.WriteLine(reader["LastName"]);
//            }
//        }

//        cnx.Close();

//    }
//} 

//Console.WriteLine("Entrez le nom de l'etudiant que vous souhaitez supprimer");
//string nom = Console.ReadLine();

//using SqlConnection connection = new SqlConnection(connectionString);
//connection.Open();

//using SqlCommand cmd = connection.CreateCommand();
//cmd.CommandText = $"DELETE FROM student WHERE lastName = @toto";
//cmd.Parameters.AddWithValue("toto", nom);
//cmd.ExecuteNonQuery();


//cmd.CommandText = "UPDATE student SET active = 1 WHERE id = @param1";
//cmd.Parameters.AddWithValue("param1", 2);
////SqlParameter p = new SqlParameter();
////p.ParameterName = "param1";
////p.Value = 2;
////p.DbType = DbType.Int32;
////cmd.Parameters.Add(p);
//int row = cmd.ExecuteNonQuery();

//Console.WriteLine(row);

//cmd.CommandText = @"
//    INSERT INTO student(
//        LastName,
//        FirstName,
//        BirthDate,
//        YearResult,
//        SectionId
//    ) 
//    OUTPUT INSERTED.LastName
//    VALUES(@p1, @p2, @p3, @p4, @p5)";
//cmd.Parameters.AddWithValue("p1", "LY");
//cmd.Parameters.AddWithValue("p2", "Khun");
//cmd.Parameters.AddWithValue("p3", new DateTime(1982,5,6));
//cmd.Parameters.AddWithValue("p4", 11);
//cmd.Parameters.AddWithValue("p5", 1010);

//int result = (int)cmd.ExecuteScalar();

//connection.Close();
#endregion

#region Correctifs Exos

#region Exo 1
//using(SqlConnection cnx = new SqlConnection(connectionString))
//{
//    using(SqlCommand cmd = cnx.CreateCommand())
//    {
//        cmd.CommandText = "SELECT Id, FirstName, LastName FROM V_Student";

//        cnx.Open();
//        using(SqlDataReader reader = cmd.ExecuteReader())
//        {
//            while(reader.Read())
//            {
//                Console.WriteLine($"{reader["id"]} | " +
//                    $"{reader["LastName"]} | {reader["FirstName"]}");
//            }
//        }
//        cnx.Close();
//    }
//} 
#endregion

#region Exo2
//DataTable dt = new DataTable();
//using (SqlConnection conn = new SqlConnection(connectionString))
//{
//    using (SqlCommand cmd = conn.CreateCommand())
//    {
//        cmd.CommandText = "SELECT * FROM section";

//        SqlDataAdapter adapter = new SqlDataAdapter();
//        adapter.SelectCommand = cmd;

//        adapter.Fill(dt);

//    }
//}

//if (dt.Rows.Count > 0)
//{
//    foreach (DataRow dr in dt.Rows)
//    {
//        Console.WriteLine($"{dr["id"]} | {dr["SectionName"]}");
//    }
//} 
#endregion

//using(SqlConnection conn = new SqlConnection(connectionString))
//{
//    using(SqlCommand cmd = conn.CreateCommand())
//    {
//        cmd.CommandText = "SELECT AVG(CONVERT(decimal,YearResult)) FROM student";
//        conn.Open();
//        decimal moyenne = (decimal)cmd.ExecuteScalar();
//        conn.Close();
//        Console.WriteLine(moyenne);
//    }
//}

using SqlConnection connection = new SqlConnection(connectionString);

//while (true)
//{
//    Console.WriteLine("Insérer un étudiant");
//    Student student = new Student();

//    Console.WriteLine("Entrer le nom de l'étudiant");
//    student.LastName = Console.ReadLine();

//    Console.WriteLine("Entrer le prénom de l'étudiant");
//    student.FirstName = Console.ReadLine();

//    Console.WriteLine("Entrer la de naissance de l'étudiant");
//    student.BirthDate = DateTime.Parse(Console.ReadLine());

//    Console.WriteLine("Entrer son résulat annuel (ou laisser vide si pas encore de résultat)");
//    string value = Console.ReadLine();
//    if(value == "")
//    {
//        student.YearResult = null;
//    }
//    else
//    {
//        student.YearResult = int.Parse(value);
//    } 

//    Console.WriteLine("Entrer sa section");
//    student.SectionId = int.Parse(Console.ReadLine());

//    int id = Add(student);
//    Console.WriteLine("Vous avez insérer un nouvel étudiant son id est " + id);
//    Console.WriteLine("continuer ...");
//    Console.ReadKey();
//}

int Add(Student student)
{
    connection.Open();

    using SqlCommand cmd = connection.CreateCommand();
    cmd.CommandText = @"
        INSERT INTO student (LastName, FirstName, BirthDate, YearResult, sectionId)
        OUTPUT inserted.Id
        VALUES (@p1, @p2, @p3, @p4, @p5)";
    cmd.Parameters.AddWithValue("p1", student.LastName);
    cmd.Parameters.AddWithValue("p2", student.FirstName);
    cmd.Parameters.AddWithValue("p3", student.BirthDate);
    //if(student.YearResult != null)
    //{
    //    cmd.Parameters.AddWithValue("p4", student.YearResult);
    //} 
    //else
    //{
    //    cmd.Parameters.AddWithValue("p4", DBNull.Value);
    //}
    // cmd.Parameters.AddWithValue("p4", student.YearResult == null ? DBNull.Value : null);
    cmd.Parameters.AddWithValue("p4", student.YearResult ?? (object)DBNull.Value);
    cmd.Parameters.AddWithValue("p5", student.SectionId);

    int id = (int)cmd.ExecuteScalar();

    connection.Close();
    return id;
}

#endregion