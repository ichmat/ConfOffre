using ConfOffre.Tools;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfOffre.Models
{
    internal class MStock : MObject
    {
        internal int Id_stock { get; private set; }

        internal int Id_produit { get; private set; }
        internal int Id_optAch { get; private set; }
        internal int Id_conf { get; private set; }
        internal int Quantite_stock { get; private set; }

        public MStock(long id_obj) : base(id_obj)
        {
        }

        internal async static Task<List<MObject>> SelectAll(PostgreSQL con)
        {
            List<MObject> mProduits = new List<MObject>();
            NpgsqlDataReader reader = await con.ExecuteSql("Select * From stock;");
            while (reader.Read())
            {
                MStock obj = new MStock(DataCommunication.Index);
                obj.BindData(reader);
                mProduits.Add(obj);
            }
            await con.Close();
            return mProduits;
        }

        protected override void BindData(NpgsqlDataReader reader)
        {
            Id_produit = reader.GetInt32(0);
            Id_optAch = reader.GetInt32(1);
            Id_conf = reader.GetInt32(2);
            Id_stock = reader.GetInt32(3);
            Quantite_stock = reader.GetInt32(4);
        }

        protected override string ColName()
        {
            return "stock";
        }

        protected override string[][] GetIdStr()
        {
            string[][] vs = new string[2][];
            vs[0] = new string[] { "id_stock", "id_conf", "id_optAch", "id_produit" };
            vs[1] = new string[] { Id_stock.ToString(), Id_conf.ToString(), Id_optAch.ToString(), Id_produit.ToString() };
            return vs;
        }

        protected override string[] GetValName()
        {
            return new string[] { "quantite_stock" };
        }

        protected override void InsertData(params string[] prms)
        {
            Id_produit = Convert.ToInt32(prms[0]);
            Id_optAch = Convert.ToInt32(prms[1]);
            Id_conf = Convert.ToInt32(prms[2]);
            Quantite_stock = Convert.ToInt32(prms[3]);
        }

        protected override void InsertIdGen(int id)
        {
            Id_stock = id;
        }

        protected override bool[] ValIsStr()
        {
            return new bool[] { false };
        }
    }
}
