
using System;
using System.Data;
using System.Collections.Generic;
using MDD_Model;
using MDD_DAL;

namespace MDD_BLL
{
    /// <summary>
    /// MDD_accounts
    /// </summary>
    public partial class MDD_accountsBll
    {
        MDD_accountsdal dal = new MDD_accountsdal();

        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string Acc_Id)
        {
            return dal.Exists(Acc_Id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(MDD_accounts model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(MDD_accounts model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string Acc_Id)
        {

            return dal.Delete(Acc_Id);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string Acc_Idlist)
        {
            return dal.DeleteList(Acc_Idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public MDD_accounts GetModel(string Acc_Id)
        {

            return dal.GetModel(Acc_Id);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<MDD_accounts> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<MDD_accounts> DataTableToList(DataTable dt)
        {
            List<MDD_accounts> modelList = new List<MDD_accounts>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                MDD_accounts model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = dal.DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}

