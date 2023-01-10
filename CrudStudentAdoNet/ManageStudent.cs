using System.Data.SqlClient;

namespace CrudStudentAdoNet;

public class ManageStudent : ADataAccess
{
    private readonly SqlConnection _sqlConnection;

    public ManageStudent(SqlConnection sqlConnection)
    {
        _sqlConnection = sqlConnection;
    }
    
    public override List<Student> GetAllStudents()
    {
        using (SqlCommand command = _sqlConnection.CreateCommand())
        {
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "GetAllStudents";

            using (SqlDataReader reader = command.ExecuteReader())
            {
                List<Student> students = new List<Student>();
                
                while (reader.Read())
                {
                    students.Add(new Student
                    {
                        Id = (int)reader["Id"],
                        Name = (string)reader["Name"],
                        Address = (string)reader["Address"],
                        Age = (int)reader["Age"]
                    });
                }
                return students;
            }
        }
    }

    public override Student GetStudentById(int id)
    {
        using (SqlCommand command = _sqlConnection.CreateCommand())
        {
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "GetStudentById";
            command.Parameters.AddWithValue("@id", id);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new Student
                    {
                        Id = (int)reader["Id"],
                        Name = (string)reader["Name"],
                        Address = (string)reader["Address"],
                        Age = (int)reader["Age"]
                    };
                }
            }
        }
        return new Student();
    }

    public override void InsertStudent(Student student)
    {
        using (SqlCommand command = _sqlConnection.CreateCommand())
        {
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "InsertStudent";
            command.Parameters.AddWithValue("@name", student.Name);
            command.Parameters.AddWithValue("@address", student.Address);
            command.Parameters.AddWithValue("@age", student.Age);

            command.ExecuteNonQuery();
        }
    }

    public override void UpdateStudent(Student student)
    {
        using (SqlCommand command = _sqlConnection.CreateCommand())
        {
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "UpdateStudent";
            command.Parameters.AddWithValue("@id", student.Id);
            command.Parameters.AddWithValue("name", student.Name);
            command.Parameters.AddWithValue("@address", student.Address);
            command.Parameters.AddWithValue("@age", student.Age);
            command.ExecuteNonQuery();
        }
    }

    public override void DeleteStudent(int id)
    {
        using (SqlCommand command = _sqlConnection.CreateCommand())
        {
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "DeleteData";
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }
    }
}