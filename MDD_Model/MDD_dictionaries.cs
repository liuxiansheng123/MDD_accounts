
using System;
namespace MDD_Model
{
	/// <summary>
	/// MDD_dictionaries:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class MDD_dictionaries
	{
		public MDD_dictionaries()
		{}
		#region Model
		private string _id;
		private string _type;
		private string _name;
		private string _context;
		private string _parentid;
		private string _isdelete;
		private DateTime _createtime;
		/// <summary>
		/// 
		/// </summary>
		public string ID
		{
			set{ _id=value;}
			get{return _id;}
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
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
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
		public string ParentId
		{
			set{ _parentid=value;}
			get{return _parentid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string IsDelete
		{
			set{ _isdelete=value;}
			get{return _isdelete;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime CreateTime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		#endregion Model

	}
}

