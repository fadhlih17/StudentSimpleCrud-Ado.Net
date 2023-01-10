namespace CrudStudentAdoNet;

public abstract class ADataAccess : IDataAccess
{
    public abstract List<Student> GetAllStudents();

    public abstract Student GetStudentById(int id);

    public abstract void InsertStudent(Student student);

    public abstract void UpdateStudent(Student student);

    public abstract void DeleteStudent(int id);
}