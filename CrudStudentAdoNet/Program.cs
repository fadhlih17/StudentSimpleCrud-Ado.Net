using System.Configuration;
using System.Data.SqlClient;
using CrudStudentAdoNet;

class Program
{
    private static readonly string constring =
        ConfigurationManager.ConnectionStrings["MyConfigurationString"].ConnectionString;

    private static SqlConnection _connection = new SqlConnection(constring);

    private static ManageStudent _manageStudent = new ManageStudent(_connection);
    
    public static void Main(string[] args)
    {
        _connection.Open();
        Display();
        
    }

    public static void Display()
    {
        while (true)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("1. Get All Students");
                Console.WriteLine("2. Get Student By Id");
                Console.WriteLine("3. Insert Student");
                Console.WriteLine("4. Update Student");
                Console.WriteLine("5. Delete Student");
                Console.WriteLine("6. exit");

                Console.Write("Insert your Choice: ");
                int choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1 :
                        GetAllStudents();
                        break;
                    case 2 :
                        GetStudentById();
                        break;
                    case 3 :
                        InsertStudent();
                        break;
                    case 4 :
                        UpdateStudent();
                        break;
                    case 5 :
                        DeleteStudent();
                        break;
                    case 6 :
                        return;
                    default:
                        Console.WriteLine("Invalid input");
                        Console.ReadKey();
                        break;
                }
            }
            catch (FormatException e)
            {
                Console.WriteLine("Invalid format input");
                Console.ReadKey();
            }
        }
    }

    public static void GetAllStudents()
    {
        List<Student> students = _manageStudent.GetAllStudents();
        Console.WriteLine($"Id\tName\t\tAddress\t\tAge");
        foreach (var student in students)
        {
            Console.WriteLine($"{student.Id}\t{student.Name}\t\t{student.Address}\t\t{student.Age}");
        }

        Console.WriteLine("Press enter for back to menu");
        Console.ReadKey();
    }

    public static void GetStudentById()
    {
        Console.WriteLine("********** Get Student By Id **********");
        Console.Write("Input Id to Search: ");
        int id = int.Parse(Console.ReadLine());

        Student student = _manageStudent.GetStudentById(id);

        if (student.Id != 0)
        {
            Console.WriteLine($"Name: {student.Name}");
            Console.WriteLine($"Address: {student.Address}");
            Console.WriteLine($"Age: {student.Age}");
        }
        else
        {
            Console.WriteLine("Data Cant be Found");
        }

        Console.WriteLine("Press enter for back to menu");
        Console.ReadKey();
    }

    public static void InsertStudent()
    {
        Console.WriteLine("********* Insert Student *********");
        Console.Write("Input Name: ");
        string name = Console.ReadLine();
        Console.Write("Input Address: ");
        string address = Console.ReadLine();
        Console.Write("Input Age: ");
        int age = int.Parse(Console.ReadLine());

        Student student = new Student
        {
            Name = name,
            Address = address,
            Age = age
        };
        _manageStudent.InsertStudent(student);

        Console.WriteLine("Insert Data Successfully");
        Console.WriteLine();
        Console.WriteLine("Press enter for back to menu");
        Console.ReadKey();
    }

    public static void UpdateStudent()
    {
        Console.WriteLine("********* Update Student Data *********");
        Console.Write("Insert Id to Update: ");
        int id = int.Parse(Console.ReadLine());

        Student student = _manageStudent.GetStudentById(id);

        if (student.Id != 0)
        {
            Console.Write($"Are you sure want Update Data with Name {student.Name} (y/n): ");
            string choice = Console.ReadLine();

            if (choice.ToLower() == "y")
            {
                Console.Write("Insert New Name: ");
                string name = Console.ReadLine();
                Console.Write("Insert New Address: ");
                string address = Console.ReadLine();
                Console.Write("Insert New Age: ");
                int age = int.Parse(Console.ReadLine());

                student.Name = name;
                student.Address = address;
                student.Age = age;
            
                _manageStudent.UpdateStudent(student);
                Console.WriteLine("Success Update Data");
            }
            else if (choice.ToLower() == "n")
            {
                Console.WriteLine("Data Cancel to Update, Press Enter for back to menu");
                Console.ReadKey();
                return;
            }
            else
            {
                Console.WriteLine("Invalid choice");
                Console.ReadKey();
                return;
            }
            
        }
        else
        {
            Console.WriteLine("Cant be found the data");
        }

        Console.WriteLine();
        Console.WriteLine("Press enter for back to menu");
        Console.ReadKey();
    }

    public static void DeleteStudent()
    {
        Console.WriteLine("********* Delete Student *********");
        Console.Write("Input Id to Delete: ");
        int id = int.Parse(Console.ReadLine());

        Student student = _manageStudent.GetStudentById(id);

        if (student.Id != 0)
        {
            _manageStudent.DeleteStudent(student.Id);
            Console.WriteLine("Delete Success");
        }
        else
        {
            Console.WriteLine("Data Can't be Found");
        }

        Console.WriteLine();
        Console.WriteLine("Press enter for back to menu");
        Console.ReadKey();
    }
}