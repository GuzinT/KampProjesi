using DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        Context c = new Context();

        DbSet<T> _object; //_object aslında bizim T değerine karşılık gelen sınıfı tutuyordu
        //T değerine karşılık olarak gelecek sınıfı nasıl bulacağız;
        //burada constractır denilen olay var yapıcı metot olarak geliyor.objecte değer ataması yapacağız
        public GenericRepository() //ctor yazıp tab tuşuna bastık.Oluşturduğumuz sınıfın ismiyle aynı isimle bir constractır oluşturur.
        {
            _object = c.Set<T>(); //dışarıdan gönderilen T değerini objectin içine aldı
            //artık object isimle filed dışarıdan gönderdiğim entity ne ise o olacak
        }

        public void Delete(T p)
        {
            _object.Remove(p);
            c.SaveChanges();
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            return _object.SingleOrDefault(filter); //SingleOrDefault:Bir dizide veya listede sadece bir tane değer geriye döndürmek için kullanılan entityframework linq metodudur. 
        }

        public void Insert(T p)
        {
            _object.Add(p);
            c.SaveChanges();
        }

        public List<T> List()
        {
            return _object.ToList();
        }

        public List<T> List(Expression<Func<T, bool>> filter)
        {
            return _object.Where(filter).ToList();
        }

        public void Update(T p)
        {
            c.SaveChanges();
        }
    }
}
