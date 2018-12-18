
using System;
namespace MDD_Model
{
	/// <summary>
	/// MDD_accounts:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class MDD_accountsM
	{

		#region Model
		private string _acc_id;
		private string _context;
		private decimal? _money;
		private string _type;
		private string _turnover;
		private string _remark;
		private DateTime? _createtime;
		/// <summary>
		/// 
		/// </summary>
		public string Acc_Id
		{
			set{ _acc_id=value;}
			get{return _acc_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Context
		{
			set{ _context=value;}
			get{return _context;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? money
		{
			set{ _money=value;}
			get{return _money;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string turnover
		{
			set{ _turnover=value;}
			get{return _turnover;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		#endregion Model

	}
}

