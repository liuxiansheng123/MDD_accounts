using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDD_Model
{
    public class MDD_menu
    {
        private string _id;
        private string _name;
        private string _link;
        private string _parentid;
        private int _isdelete;
        private string _remark;

        public string Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public string Link { get => _link; set => _link = value; }
        public string Parentid { get => _parentid; set => _parentid = value; }
        public int Isdelete { get => _isdelete; set => _isdelete = value; }
        public string Remark { get => _remark; set => _remark = value; }
    }
}
