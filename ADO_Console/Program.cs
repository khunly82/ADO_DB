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
    Console.WriteLine("4. Modifier la section et le résultat d'1 étudiant");

    string choix = Console.ReadLine();

    switch (choix)
    {
        case "1":
            DisplayStudent();
            break;
        case "2":
            AddStudent();
            break;
        case "3":
            RemoveStudent();
            break;
        case "4":
            UpdateStudent();
            break;
    }

}

void UpdateStudent()
{
    int id = int.Parse(Question("Quel etudiant voulez-vous modifiez"));
    Student? student = studentRepo.Get(id);
    if(student is null)
    {
        Console.WriteLine("Impossible de modifier cet étudiant");
        Console.WriteLine("Continuer...");
        Console.ReadKey();
    }
    else
    {
        Console.WriteLine($"{student.LastName} {student.FirstName}");
        string year_result = Question($"Resultat actuel ({student.YearResult}) Laisser vide pour ne pas modifier");
        string section = Question($"Section Actuelle ({student.SectionId}) Laisser vide pour ne pas modifier");

        if(year_result != "")
        {
            student.YearResult = int.Parse(year_result);
        }

        if (section != "")
        {
            student.SectionId = int.Parse(section);
        }
        studentRepo.Update(student);
    }
}

void RemoveStudent()
{
    int id = int.Parse(Question("Entrez l'id de l'etudiant à suppimer"));

    // studentRepo.Remove(new Student { Id = id });

    Student? s = studentRepo.Get(id);

    if (s is null)
    {
        Console.WriteLine("L'étudiant que vous souhaitez supprimer n'existe pas ou a déjà été supprimé");
        Console.WriteLine("Continuer ...");
        Console.ReadKey();
    }
    else
    {
        studentRepo.Remove(s);
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

    // inserer dans la database
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
    Console.WriteLine($"id\tnom\tprenom\tsection\tresultat");
    Console.WriteLine($"________________________________________________________");
    foreach (Student student in students)
    {
        Console.WriteLine($"{student.Id}\t{student.LastName}\t{student.FirstName}\t{student.SectionId}\t{student.YearResult}");
    }
    Console.WriteLine("Continuer ...");
    Console.ReadKey();
}