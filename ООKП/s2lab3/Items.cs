////using System;
////using System.Collections.Generic;
////using System.ComponentModel;
////using System.Linq;
////using System.Text;
////using System.Threading.Tasks;

////namespace ookplab3
////{
////    public abstract class Product
////    {
////        internal int id;
////        internal string name;
////        internal int price;
////        internal int supplyDate;
////        internal int weight;
////        internal int width;
////        internal int length;
////        internal int height;
////        public Product() { }
////        public abstract void Update(object att);
////        public abstract void Display(string filter = "", object val = null);
////        public virtual void Add(Product component)
////        {
////            throw new NotImplementedException();
////        }
////        public virtual void Remove(Product component)
////        {
////            throw new NotImplementedException();
////        }
////    }

////    public class BasicProduct : Product
////    {
////        public BasicProduct(string n, int p, int sD, int wg, int w, int l, int h)
////        {
////            this.name = n;
////            this.price = p;
////            this.supplyDate = sD;
////            this.weight = wg;
////            this.width = w;
////            this.length = l;
////            this.height = h;
////        }
////        public override void Update(object att)
////        {
////            this.name = att.ToString();
////        }
////        public override void Display(string filter = "", object val = null)
////        {
////            Console.WriteLine(this.name);
////        }
////    }

////    public class SpecialProduct : Product
////    {
////        protected Dictionary<string, object> AddParams = new Dictionary<string, object>();
////        public void AddNewAttribute(object Param)
////        {
////            Console.WriteLine("Введіть назву поля");
////            string parName = Console.ReadLine();
////            AddParams.Add(parName, Param);
////        }
////        public SpecialProduct(string n, int p, int sD, int wg, int w, int l, int h, object[] addParams)
////        {
////            this.name = n;
////            this.price = p;
////            this.supplyDate = sD;
////            this.weight = wg;
////            this.width = w;
////            this.length = l;
////            this.height = h;
////            for (int i = 0; i < addParams.Length; i++)
////            {
////                AddNewAttribute(addParams[i]);
////            }
////        }
////        public override void Update(object att)
////        {
////            this.name = att.ToString();
////        }

////        public override void Display(string filter = "", object val = null)
////        {
////            Console.WriteLine(this.name);
////        }
////    }

////    internal class Items : Product
////    {
////        protected List<Product> _children = new List<Product>();

////        public override void Add(Product component)
////        {
////            this._children.Add(component);
////        }

////        public override void Remove(Product component)
////        {
////            this._children.Remove(component);
////        }
////        public override void Update(object att)
////        {
////            foreach(var child in this._children)
////            {
////                child.name = att.ToString();
////            }
////        }
////        public override void Display(string filter, object val)
////        {
////            foreach (var child in this._children)
////            {
////                switch (filter)
////                {
////                    case "name":
////                        if (child.name == val.ToString())
////                            child.Display();
////                        break;
////                    case "price":
////                        if (child.price == (int)val)
////                            child.Display();
////                        break;
////                    case "supplyDate":
////                        if (child.supplyDate == (int)val)
////                            child.Display();
////                        break;
////                    case "weight":
////                        if (child.weight == (int)val)
////                            child.Display();
////                        break;
////                    case "width":
////                        if (child.width == (int)val)
////                            child.Display();
////                        break;
////                    case "length":
////                        if (child.length == (int)val)
////                            child.Display();
////                        break;
////                    case "height":
////                        if (child.height == (int)val)
////                            child.Display();
////                        break;
////                    default:
////                        break;
////                }
////            }
////        }
////        public int Count()
////        {
////            return this._children.Count();
////        }
////    }

////}
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ookplab3
//{
//    public abstract class TableObject
//    {
//        internal string name;
//        internal string value;
//        internal string type = "";
//        public List<TableObject> Params = null;
//        public TableObject() { }
//        public abstract void Update(string val, string att = "");
//        public abstract void Display(string filter = "", string val = "");
//        public virtual void AddObj(TableObject[] component)
//        {
//            throw new NotImplementedException();
//        }
//        public virtual void Remove(TableObject component)
//        {
//            throw new NotImplementedException();
//        }
//    }

//    public class Attribute : TableObject
//    {
//        public Attribute(string name, string val)
//        {
//            this.name = name;
//            this.value = val;
//            this.type = "attribute";
//        }
//        public override void Update(string val, string att = "")
//        {
//            this.value = val;
//        }

//        public override void Display(string filter = "", string val = "")
//        {
//            Console.WriteLine(this.name);
//        }
//    }

//    public class TablePart : TableObject
//    {
//        public List<TableObject> Params = new List<TableObject>();
//        public TablePart(string[] addParams = null, TableObject[] addToTable = null)
//        {
//            if (addParams != null)
//            {
//                if (addParams.Length >= 7)
//                {
//                    this.type = "product";
//                    this.Params.Add(new Attribute("name", addParams[0]));
//                    this.Params.Add(new Attribute("price", addParams[1]));
//                    this.Params.Add(new Attribute("supplyDate", addParams[2]));
//                    this.Params.Add(new Attribute("weight", addParams[3]));
//                    this.Params.Add(new Attribute("width", addParams[4]));
//                    this.Params.Add(new Attribute("length", addParams[5]));
//                    this.Params.Add(new Attribute("height", addParams[6]));
//                }
//            }
//            else if (addToTable != null)
//            {
//                foreach (var el in addToTable)
//                {
//                    this.Params.Add(el);
//                }
//            }    
//        }
//        public override void AddObj(TableObject[] components)
//        {
//            for (int i = 0; i < components.Length; i++)
//                this.Params.Add(components[i]);
//        }
//        public override void Remove(TableObject component)
//        {
//            this.Params.Remove(component);
//        }
//        public override void Update(string val, string att)
//        {
//            TableObject Att = this.Params.Find(x => x.name == att);
//            Att.Update(val);
//        }

//        public override void Display(string filter = "", string val = "")
//        {
//            if (this.type == "product")
//            {
//                foreach (var child in this.Params)
//                {
//                    child.Display();
//                }
//            }
//            else
//            {
//                foreach (var child in this.Params)
//                {
//                    if (child.name == filter && child.value == val)
//                        child.Display();
//                }
//            }
//        }
//    }

//}

//var TestParam1 = new TablePart(new string[] { "Продукт", "520", "10.02.2023", "200", "10", "20", "15" });
//foreach (var Param in TestParam1.Params)
//{
//    Console.WriteLine(Param.name);
//}
//var TestParam2 = new TablePart(new string[] { "Продукт", "120", "30.01.2023", "450", "11", "19", "13" });
//var TestParam3 = new TablePart(new string[] { "НеПродукт", "1020", "28.03.2023", "920", "100", "190", "130" });
//var BasicTable = new TablePart(null, new TablePart[] { TestParam1, TestParam2, TestParam3 });

//DataTable dt = new DataTable();
//dt.Columns.Add("Name", typeof(string));
//dt.Columns.Add("Price", typeof(string));
//dt.Columns.Add("Supply Date", typeof(string));
//dt.Columns.Add("Weight", typeof(string));
//dt.Columns.Add("Width", typeof(string));
//dt.Columns.Add("Length", typeof(string));
//dt.Columns.Add("Height", typeof(string));

//foreach (var item in BasicTable.Params)
//{
//    var newlist = new object[7];
//    foreach (var Param in item.Params)
//    {
//        newlist.Append(Param);
//    }
//    dt.Rows.Add(newlist);
//}

//dataGridView1.DataSource = dt;