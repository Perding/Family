using DapperData;
using Entity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace Repository.IReposotiry
{
    public class StudentRepository
    {
        private static readonly string SELECT_SQL_STRING = @"select * from student";
        private static readonly string INSERT_SQL_STRING = @"insert into student(Name, Age) values(@Name, @Age)";
        private static readonly string UPDATE_SQL_STRING = @"update student set Name=@Name, Age=@Age where Id = @Id";
        private static readonly string DELETE_SQL_STRING = @"delete student where Id = @Id";
        private DapperFactory _factory;
        private IDapper _dapper;

        public StudentRepository(IConfiguration configuration)
        {
            _factory = DapperFactory.GetInstance(configuration);
            _dapper = _factory.GetDapper();
          
        }

        //public async Task<IEnumerable<Student>> GetList()
        //{
        //    var list=  _dapper.GetList<Student>(SELECT_SQL_STRING, null);
        //   // IEnumerable<Student> students =  list;
        //    return await list;
        //}

        //public async Task<Student> GetStudent(Student model)
        //{
          
        //    return await  _dapper.GetSinger("select top 1 * from student where Id=@id", model);
        //}

        //public Task<bool> Insert(List<Student> list)
        //{
        //    return _dapper.Insert(INSERT_SQL_STRING, list);
        //}

        //public Task<bool> Update(Student student)
        //{
        //    return _dapper.Update(UPDATE_SQL_STRING, student);
        //}

        //public Task<bool> Delete(int id)
        //{
        //    return _dapper.Delete(DELETE_SQL_STRING, id);
        //}
    }
}
