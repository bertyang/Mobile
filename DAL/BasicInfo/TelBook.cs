using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

using Anchor.FA.Model;
using System.Data.Linq;
using System.Linq.Expressions;
using Anchor.FA.Utility;
using System.Data;

namespace Anchor.FA.DAL.BasicInfo
{
    public class TelBook
    {
        /// <summary>
        /// 获取所有电话信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public static object LoadAllTelBookByPage(int? page, int? rows, string order, string sort, int? OwnerID, string Name, string Type)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                string sql = "";
                if (string.IsNullOrEmpty(Type) || Type == "--请选择--")
                {
                    sql = @"select ID=b.编码,Name=b.名称,TypeName = t.名称,Tel1=b.联系电话一,Exten1=b.分机一,Tel2=b.联系电话二,
                                    Exten2=b.分机二,OwnerID=isnull(c.归属人ID,0),Remark=b.备注,OrderNo=b.顺序号,
                                    IsEffect=case when b.是否有效='1' then '有效' else '无效' end
                                    from TTelBook b
                                    join TZTelType t on b.电话分类编码 = t.编码
                                    left join B_TELTYPE_CUSTOM c on t.编码 = c.编码";
                }
                else
                {
                    sql = @"with ty as(
                                select t1.编码,t1.名称,t1.上级编码
                                from TZTelType t1
                                where t1.编码 in (" + Type + @")
                                union all 
                                 select t2.编码,t2.名称,t2.上级编码
                                 from TZTelType t2
                                 join ty t on t2.上级编码 = t.编码)
                                 select distinct ID=b.编码,Name=b.名称,TypeName = ty.名称,Tel1=b.联系电话一,Exten1=b.分机一,Tel2=b.联系电话二,
                                 Exten2=b.分机二,OwnerID=isnull(c.归属人ID,0),Remark=b.备注,OrderNo=b.顺序号,
                                 IsEffect=case when b.是否有效='1' then '有效' else '无效' end
                                 from ty
                                 join TTelBook b on ty.编码 = b.电话分类编码
                                 left join B_TELTYPE_CUSTOM c on ty.编码 = c.编码";
                }

                

                var list1 = dbContext.ExecuteQuery<C_TelBook>(sql);
                var list2 = dbContext.ExecuteQuery<C_TelBook>(sql);

                if (!string.IsNullOrEmpty(Name))
                {
                    list1 = list1.Where(o => o.Name.Contains(Name));
                    list2 = list2.Where(o => o.Name.Contains(Name));
                }

                if (OwnerID != null)
                {
                    list1 = list1.Where(t => t.OwnerID == OwnerID);
                    list2 = list2.Where(t => t.OwnerID == OwnerID);
                }

                if (page != null && rows != null)
                {
                    long total = list1.LongCount();
                    list2 = list2.OrderBy(p => p.ID);
                    list2 = list2.Skip(((int)page - 1) * (int)rows).Take((int)rows);
                    var result = new { total = total, rows = list2.ToList() };
                    return result;

                }
                else
                {
                    return list1.ToList();
                }
            }

        }

        #region 修改/编辑

        public static object Edit(int? id)
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                TTelBook entity = null;
                if (id != null)
                {
                    entity = dbContext.TTelBook.FirstOrDefault(t => t.编码 == id);
                }
                entity = entity ?? new TTelBook
                {
                    编码 = 0,
                    名称 = "",
                    电话分类编码 = -1,
                    联系电话一="",
                    联系电话二="",
                    分机一="",
                    分机二="",
                    备注="",
                    顺序号=1,
                    是否有效=true
                };
                return entity;
            }
        }
        #endregion

        #region 保存

        public static bool Save(TTelBook entity)
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                if (entity.编码 == 0)  //添加
                {
                    var list = from p in dbContext.TTelBook select p.编码;
                    long total = list.LongCount();
                    if (total == 0)
                    {
                        entity.编码 = 1;
                        entity.顺序号 = 1;
                    }
                    else
                    {
                        entity.编码 = dbContext.TTelBook.Max(t => t.编码) + 1;
                        entity.顺序号 = dbContext.TTelBook.Max(t => t.顺序号) + 1;
                    }

                    dbContext.TTelBook.InsertOnSubmit(entity);
                    dbContext.SubmitChanges();
                    return true;

                }
                else  //修改
                {
                    var model = dbContext.TTelBook.FirstOrDefault(t => t.编码 == entity.编码);
                    model.编码 = entity.编码;
                    model.名称 = entity.名称;
                    model.电话分类编码 = entity.电话分类编码;
                    model.是否有效 = entity.是否有效;
                    model.分机一 = entity.分机一;
                    model.联系电话一 = entity.联系电话一;
                    model.联系电话二 = entity.联系电话二;
                    model.分机二 = entity.分机二;
                    model.备注 = entity.备注;
                    model.顺序号 = entity.顺序号;
                    dbContext.SubmitChanges();
                    return true;
                }
            }
        }

        #endregion

        //删除
        public static bool Delete(IList<int> idList)
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                foreach (int g in idList)
                {
                    var model = dbContext.TTelBook.Single(t => t.编码 == g);
                    //dbContext.TTelBook.Load();
                    dbContext.TTelBook.DeleteOnSubmit(model);
                }
                dbContext.SubmitChanges();
                return true;
            }

        }

        /// <summary>
        /// 获取所有电话类型
        /// </summary>
        /// <returns></returns>
        public static List<TZTelType> GetAllType()
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                return dbContext.TZTelType.ToList();
            }
        }
        /// <summary>
        /// 根据归属人ID获取电话类型
        /// </summary>
        /// <returns></returns>
        public static List<TZTelType> GetType(int? OwnerID)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                if (OwnerID.GetValueOrDefault() == 0)
                {
                    return (from t in dbContext.TZTelType
                            join c in dbContext.B_TELTYPE_CUSTOM on t.编码 equals c.编码 into left_c
                            from c in left_c.DefaultIfEmpty()
                            where c.归属人ID == null
                            select t).ToList();
                }
                else
                {
                    return (from t in dbContext.TZTelType
                            join tt in dbContext.B_TELTYPE_CUSTOM on t.编码 equals tt.编码
                            where tt.归属人ID == OwnerID
                            select t).ToList();
                }
            }
        }

        /// <summary>
        /// 编辑/新增时获取电话类型树
        /// </summary>
        /// <param name="superiorID"></param>
        /// <returns></returns>
        //public static List<TZTelType> GetTree(string superiorID)
        //{
        //    using (MainDataContext dbContext = new MainDataContext())
        //    {
        //        List<TZTelType> list = (from p in dbContext.TZTelType select p).ToList();
        //        return list;
        //    }
        //}

        #region 分类管理
        public static object LoadAllTelTypeByPage(int page, int rows, string order, string sort, int OwnerID)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                var list = from t in dbContext.TZTelType
                           join tt in dbContext.TZTelType on t.上级编码 equals tt.编码 into left_tt
                           from tt in left_tt.DefaultIfEmpty()
                           join c in dbContext.B_TELTYPE_CUSTOM on t.编码 equals c.编码 into left_c
                           from c in left_c.DefaultIfEmpty()
                           //where c.归属人ID == OwnerID
                           select new
                           {
                               编码 = t.编码,
                               名称 = t.名称,
                               上级ID = t.上级编码,
                               上级分类 = tt.名称,
                               顺序 = t.顺序号,
                               是否有效 = t.是否有效 == true ? "是" : "否",
                               是否包含联系人 = (dbContext.TTelBook.Where(b => b.电话分类编码 == t.编码)).Count() == 0 ? "否" : "是",
                               归属人ID = c.归属人ID == null ? 0 : c.归属人ID,
                           };

                list = list.Where(t => t.归属人ID == OwnerID);

                long total = list.LongCount();
                list = list.OrderBy(t => t.编码);
                list = list.Skip((page - 1) * rows).Take(rows);

                var result = new { total = total, rows = list.ToList() };

                return result;
            }
        }

        public static bool SaveTelType(C_TelType entity) 
        {
            //using (TransactionScope scope = new TransactionScope())
            //{
                using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
                {
                    if (entity.编码 == 0)  //添加
                    {
                        var list = dbContext.TZTelType.Select(t => t.编码);
                        long total = list.LongCount();
                        if (total == 0)
                        {
                            entity.编码 = 1;
                            //entity.顺序号 = 1;
                        }
                        else
                        {
                            entity.编码 = dbContext.TZTelType.Max(t => t.编码) + 1;
                            //entity.顺序号 = dbContext.TZTelType.Max(t => t.顺序号) + 1;
                        }

                        TZTelType type = new TZTelType();

                        type.编码 = entity.编码;
                        type.名称 = entity.名称;
                        type.上级编码 = entity.上级编码;
                        type.是否有效 = entity.是否有效;
                        type.顺序号 = entity.顺序号;
                        dbContext.TZTelType.InsertOnSubmit(type);

                        using (MainDataContext Context = new MainDataContext())
                        {
                            //自定义通讯录类别
                            if (entity.归属人ID != 0)
                            {
                                B_TELTYPE_CUSTOM custom = new B_TELTYPE_CUSTOM();
                                custom.编码 = entity.编码;
                                custom.归属人ID = entity.归属人ID;

                                Context.B_TELTYPE_CUSTOM.InsertOnSubmit(custom);
                                Context.SubmitChanges();
                            }
                        }

                        dbContext.SubmitChanges();
                        //scope.Complete();
                        return true;

                    }
                    else  //修改
                    {
                        var model = dbContext.TZTelType.FirstOrDefault(t => t.编码 == entity.编码);
                        model.编码 = entity.编码;
                        model.名称 = entity.名称;
                        model.上级编码 = entity.上级编码;
                        model.是否有效 = entity.是否有效;
                        model.顺序号 = entity.顺序号;
                        dbContext.SubmitChanges();
                        //scope.Complete();
                        return true;
                    }
                }
            //}
        }

        public static bool DeleteTelType(int id)
        {
            //using (TransactionScope scope = new TransactionScope())
            //{
                using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
                {
                    var model = dbContext.TZTelType.Single(t => t.编码 == id);
                    dbContext.TZTelType.DeleteOnSubmit(model);
                    using (MainDataContext context = new MainDataContext())
                    {
                        var custom = context.B_TELTYPE_CUSTOM.SingleOrDefault(t => t.编码 == id);

                        if (custom != null)
                        {
                            context.B_TELTYPE_CUSTOM.DeleteOnSubmit(custom);
                        }
                        context.SubmitChanges();
                    }
                    dbContext.SubmitChanges();
                    //scope.Complete();
                    return true;
                }
            //}

        }
        #endregion

        /// <summary>
        /// 将DataSet中的数据导入数据库
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static bool InsertUnsale(DataTable dt)
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                int Num = dbContext.TTelBook.Max(t => t.编码);


                try
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TTelBook book = new TTelBook();
                        book.编码 = Num + 1;
                        book.名称 = Excel.FormatString(dt.Rows[i]["名称"].ToString());
                        book.联系电话一 = Excel.FormatString(dt.Rows[i]["联系电话一"].ToString());
                        book.分机一 = Excel.FormatString(dt.Rows[i]["分机一"].ToString());
                        book.联系电话二 = Excel.FormatString(dt.Rows[i]["联系电话二"].ToString());
                        book.分机二 = Excel.FormatString(dt.Rows[i]["分机二"].ToString());
                        book.备注 = Excel.FormatString(dt.Rows[i]["备注"].ToString());
                        book.顺序号 = int.Parse(Excel.FormatString(dt.Rows[i]["顺序号"].ToString()));
                        book.是否有效 = true;

                        string type = Excel.FormatString(dt.Rows[i]["电话分类"].ToString());
                        book.电话分类编码 = dbContext.TZTelType.FirstOrDefault(t => t.名称 == type).编码;

                        dbContext.TTelBook.InsertOnSubmit(book);
                        Num++;
                    }
                    dbContext.SubmitChanges();
                    return true;
                }
                catch (Exception e)
                {
                    Log4Net.LogError("Anchor.FA.DAL.BasicInfo.TelBook/InsertUnsale()", e.Message);
                    return false;
                }
            }

        }
    }
}
