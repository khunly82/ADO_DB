using ADO_Console.Domain;
using ADO_Console.Repositories;
using System.Data.SqlClient;

string connectionString = @"server=(localdb)\MSSQLLocalDB;database=TFDemoADO;integrated security=true";

// Créer une connection
SqlConnection connection = new(connectionString);
StudentRepository studentRepo = new(connection);



while (true)
{
    Console.Clear();
    Console.WriteLine("Choisir une option");
    Console.WriteLine("1. Afficher les étudiants");
    Console.WriteLine("2. Ajouter un étudiant");
    Console.WriteLine("3. Supprimer un étudiant");

    string choix = Console.ReadLine();

    switch (choix)
    {
        case "1":
            DisplayStudent();
            break;
        default:
            AddStudent();
            break;
    }

}

void AddStudent()
{
    Student student = new Student();
    student.LastName = Question("Entrer le nom de l'etudiant");
    student.FirstName = Question("Entrer le prenom de l'etudiant");
    student.BirthDate = DateTime.Parse(Question("Entrer la date de naissance de l'etudiant"));
    string value = Question("Entrer le resultat de l'etudiant (ou laisser vide)");
    if(value != "")
    {
        student.YearResult = int.Parse(value);
    }
    student.SectionId = int.Parse(Question("Entrez la section de l'etudiant"));

    // inserer dans la databse
    studentRepo.Add(student);
}

string Question(string m)
{
    Console.WriteLine(m);
    return Console.ReadLine();
}

void DisplayStudent()
{
    // recupérer dans la database
    List<Student> students = studentRepo.Get();
    Console.WriteLine($"id\tnom\tprenom\tsection\t");
    Console.WriteLine($"________________________________________________________");
    foreach (Student student in students)
    {
        Console.WriteLine($"{student.Id}\t{student.LastName}\t{student.FirstName}\t{student.SectionId}");
    }
    Console.WriteLine("Continuer ...");
    Console.ReadKey();
}