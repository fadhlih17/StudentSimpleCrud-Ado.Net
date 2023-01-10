namespace CrudStudentAdoNet;

public interface IDataAccess
{
    List<Student> GetAllStudents();
    public Student GetStudentById(int id);
    public void InsertStudent(Student student);
    public void UpdateStudent(Student student);
    public void DeleteStudent(int id);
}