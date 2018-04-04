using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;

namespace Anchor.FA.Model
{
    public class DataAccess<T> where T : class
    {
        public static List<T> ToList()
        {
            using (MainDataContext db = new MainDataContext())
            {
                return db.GetTable<T>().ToList();
            }
        }

        public static List<T> ToList(string connectionString)
        {
            using (MainDataContext db = new MainDataContext(connectionString))
            {
                return db.GetTable<T>().ToList();
            }
        }


        public static T SingleOrDefault(Func<T, bool> predicate)
        {
            using (MainDataContext db = new MainDataContext())
            {
                return db.GetTable<T>().SingleOrDefault(predicate);
            }
        }

        public static T SingleOrDefault(string connectionString,Func<T, bool> predicate)
        {
            using (MainDataContext db = new MainDataContext(connectionString))
            {
                return db.GetTable<T>().SingleOrDefault(predicate);
            }
        }

        public static T FirstOrDefault(Func<T, bool> predicate)
        {
            using (MainDataContext db = new MainDataContext())
            {
                return db.GetTable<T>().FirstOrDefault(predicate);
            }
        }

        public static T FirstOrDefault(string connectionString, Func<T, bool> predicate)
        {
            using (MainDataContext db = new MainDataContext(connectionString))
            {
                return db.GetTable<T>().FirstOrDefault(predicate);
            }
        }

        public static List<T> Where(Func<T, bool> predicate)
        {
            using (MainDataContext db = new MainDataContext())
            {
                return db.GetTable<T>().Where(predicate).ToList();
            }
        }

        public static List<T> Where(string connectionString, Func<T, bool> predicate)
        {
            using (MainDataContext db = new MainDataContext(connectionString))
            {
                return db.GetTable<T>().Where(predicate).ToList();
            }
        }

        public static void Insert(List<T> entity)
        {
            using (MainDataContext db = new MainDataContext())
            {
                try
                {
                    foreach (T obj in entity)
                    {
                        db.GetTable<T>().InsertOnSubmit(obj);
                    }

                    db.SubmitChanges();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static void Insert(string connectionString, List<T> entity)
        {
            using (MainDataContext db = new MainDataContext(connectionString))
            {
                try
                {
                    foreach (T obj in entity)
                    {
                        db.GetTable<T>().InsertOnSubmit(obj);
                    }

                    db.SubmitChanges();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public static void Insert(string connectionString, T entity)
        {
            using (MainDataContext db = new MainDataContext(connectionString))
            {
                try
                {
                    db.GetTable<T>().InsertOnSubmit(entity);
                    db.SubmitChanges();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static void Insert(T entity)
        {
            using (MainDataContext db = new MainDataContext())
            {
                try
                {
                    db.GetTable<T>().InsertOnSubmit(entity);
                    db.SubmitChanges();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static void Update(T entity)
        {
            using (MainDataContext db = new MainDataContext())
            {
                try
                {
                    db.GetTable<T>().Attach(entity, true);
                    db.SubmitChanges();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static void Update(string connectionString, T entity)
        {
            using (MainDataContext db = new MainDataContext(connectionString))
            {
                try
                {
                    db.GetTable<T>().Attach(entity, true);
                    db.SubmitChanges();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static void Update(T entity, T original)
        {
            using (MainDataContext db = new MainDataContext())
            {
                try
                {
                    db.GetTable<T>().Attach(entity, original);
                    db.SubmitChanges();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static void Update(string connectionString, T entity, T original)
        {
            using (MainDataContext db = new MainDataContext(connectionString))
            {
                try
                {
                    db.GetTable<T>().Attach(entity, original);
                    db.SubmitChanges();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public static void Delete(T entity)
        {
            using (MainDataContext db = new MainDataContext())
            {
                try
                {
                    db.GetTable<T>().Attach(entity, true);
                    db.GetTable<T>().DeleteOnSubmit(entity);
                    db.SubmitChanges();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static void Delete(string connectionString, T entity)
        {
            using (MainDataContext db = new MainDataContext(connectionString))
            {
                try
                {
                    db.GetTable<T>().Attach(entity, true);
                    db.GetTable<T>().DeleteOnSubmit(entity);
                    db.SubmitChanges();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static void Delete(List<T> entity)
        {
            using (MainDataContext db = new MainDataContext())
            {
                try
                {
                    foreach (T obj in entity)
                    {
                        db.GetTable<T>().Attach(obj, true);
                        db.GetTable<T>().DeleteOnSubmit(obj);
                    }

                    db.SubmitChanges();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static void Delete(string connectionString, List<T> entity)
        {
            using (MainDataContext db = new MainDataContext(connectionString))
            {
                try
                {
                    foreach (T obj in entity)
                    {
                        db.GetTable<T>().Attach(obj, true);
                        db.GetTable<T>().DeleteOnSubmit(obj);
                    }

                    db.SubmitChanges();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


    }

}

