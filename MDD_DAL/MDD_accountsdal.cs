
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using MDD_Model;

namespace MDD_DAL
{
	/// <summary>
	/// 数据访问类:MDD_accounts
	/// </summary>
	public partial class MDD_accountsdal
	{

		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string Acc_Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from MDD_accounts");
			strSql.Append(" where Acc_Id=@Acc_Id ");
			SqlParameter[] parameters = {
					new SqlParameter("@Acc_Id", SqlDbType.VarChar,50)			};
			parameters[0].Value = Acc_Id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(MDD_accountsM model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into MDD_accounts(");
			strSql.Append("Acc_Id,Context,money,Type,turnover,remark,CreateTime)");
			strSql.Append(" values (");
			strSql.Append("@Acc_Id,@Context,@money,@Type,@turnover,@remark,@CreateTime)");
			SqlParameter[] parameters = {
					new SqlParameter("@Acc_Id", SqlDbType.VarChar,50),
					new SqlParameter("@Context", SqlDbType.VarChar,200),
					new SqlParameter("@money", SqlDbType.Decimal,5),
					new SqlParameter("@Type", SqlDbType.VarChar,50),
					new SqlParameter("@turnover", SqlDbType.VarChar,50),
					new SqlParameter("@remark", SqlDbType.VarChar,500),
					new SqlParameter("@CreateTime", SqlDbType.DateTime)};
			parameters[0].Value = model.Acc_Id;
			parameters[1].Value = model.Context;
			parameters[2].Value = model.money;
			parameters[3].Value = model.Type;
			parameters[4].Value = model.turnover;
			parameters[5].Value = model.remark;
			parameters[6].Value = model.CreateTime;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(MDD_accountsM model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update MDD_accounts set ");
			strSql.Append("Context=@Context,");
			strSql.Append("money=@money,");
			strSql.Append("Type=@Type,");
			strSql.Append("turnover=@turnover,");
			strSql.Append("remark=@remark,");
			strSql.Append("CreateTime=@CreateTime");
			strSql.Append(" where Acc_Id=@Acc_Id ");
			SqlParameter[] parameters = {
					new SqlParameter("@Context", SqlDbType.VarChar,200),
					new SqlParameter("@money", SqlDbType.Decimal,5),
					new SqlParameter("@Type", SqlDbType.VarChar,50),
					new SqlParameter("@turnover", SqlDbType.VarChar,50),
					new SqlParameter("@remark", SqlDbType.VarChar,500),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@Acc_Id", SqlDbType.VarChar,50)};
			parameters[0].Value = model.Context;
			parameters[1].Value = model.money;
			parameters[2].Value = model.Type;
			parameters[3].Value = model.turnover;
			parameters[4].Value = model.remark;
			parameters[5].Value = model.CreateTime;
			parameters[6].Value = model.Acc_Id;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string Acc_Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from MDD_accounts ");
			strSql.Append(" where Acc_Id=@Acc_Id ");
			SqlParameter[] parameters = {
					new SqlParameter("@Acc_Id", SqlDbType.VarChar,50)			};
			parameters[0].Value = Acc_Id;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string Acc_Idlist)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from MDD_accounts ");
			strSql.Append(" where Acc_Id in ("+Acc_Idlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MDD_accountsM GetModel(string Acc_Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Acc_Id,Context,money,Type,turnover,remark,CreateTime from MDD_accounts ");
			strSql.Append(" where Acc_Id=@Acc_Id ");
			SqlParameter[] parameters = {
					new SqlParameter("@Acc_Id", SqlDbType.VarChar,50)			};
			parameters[0].Value = Acc_Id;

			MDD_accountsM model=new MDD_accountsM();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				return DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				return null;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MDD_accountsM DataRowToModel(DataRow row)
		{
			MDD_accountsM model=new MDD_accountsM();
			if (row != null)
			{
				if(row["Acc_Id"]!=null)
				{
					model.Acc_Id=row["Acc_Id"].ToString();
				}
				if(row["Context"]!=null)
				{
					model.Context=row["Context"].ToString();
				}
				if(row["money"]!=null && row["money"].ToString()!="")
				{
					model.money=decimal.Parse(row["money"].ToString());
				}
				if(row["Type"]!=null)
				{
					model.Type=row["Type"].ToString();
				}
				if(row["turnover"]!=null)
				{
					model.turnover=row["turnover"].ToString();
				}
				if(row["remark"]!=null)
				{
					model.remark=row["remark"].ToString();
				}
				if(row["CreateTime"]!=null && row["CreateTime"].ToString()!="")
				{
					model.CreateTime=DateTime.Parse(row["CreateTime"].ToString());
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select Acc_Id,Context,money,Type,turnover,remark,CreateTime ");
			strSql.Append(" FROM MDD_accounts ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
            DataSet set = DbHelperSQL.Query(strSql.ToString());

            return DbHelperSQL.Query(strSql.ToString());
        }

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" Acc_Id,Context,money,Type,turnover,remark,CreateTime ");
			strSql.Append(" FROM MDD_accounts ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM MDD_accounts ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			object obj = DbHelperSQL.GetSingle(strSql.ToString());
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.Acc_Id desc");
			}
			strSql.Append(")AS Row, T.*  from MDD_accounts T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@tblName", SqlDbType.VarChar, 255),
					new SqlParameter("@fldName", SqlDbType.VarChar, 255),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@IsReCount", SqlDbType.Bit),
					new SqlParameter("@OrderType", SqlDbType.Bit),
					new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
					};
			parameters[0].Value = "MDD_accounts";
			parameters[1].Value = "Acc_Id";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

