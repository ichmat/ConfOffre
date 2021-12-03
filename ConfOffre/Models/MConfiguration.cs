using ConfOffre.Tools;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfOffre.Models
{
    internal class MConfiguration : MObject
    {
        internal int Id_conf { get; private set; }

        internal int Id_produit { get; private set; }
        internal int Id_optAch { get; private set; }
        internal string Nom_conf { get; private set; }
        internal double Diff_prix_conf { get; private set; }
        internal bool Visible_conf { get; private set; }


        public MConfiguration(long id_obj) : base(id_obj)
        {

        }

        internal async static Task<List<MObject>> SelectAll(PostgreSQL con)
        {
            List<MObject> mProduits = new List<MObject>();
            NpgsqlDataReader reader = await con.ExecuteSql("Select * From configuration;");
            while (reader.Read())
            {
                MConfiguration obj = new MConfiguration(DataCommunication.Index);
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
            Nom_conf = reader.GetString(3);
            Diff_prix_conf = reader.GetDouble(4);
            Visible_conf = reader.GetBoolean(5);
        }

        protected override string ColName()
        {
            return "configuration";
        }

        protected override string[][] GetIdStr()
        {
            string[][] vs = new string[2][];
            vs[0] = new string[] { "id_conf", "id_optAch", "id_produit" };
            vs[1] = new string[] { Id_conf.ToString(), Id_optAch.ToString(), Id_produit.ToString() };
            return vs;
        }

        protected override string[] GetValName()
        {
            return new string[] { "nom_conf", "diff_prix_conf", "visible_conf" };
        }

        protected override void InsertData(params string[] prms)
        {
            Id_produit = Convert.ToInt32(prms[0]);
            Id_optAch = Convert.ToInt32(prms[1]);
            Nom_conf = prms[2];
            Diff_prix_conf = double.Parse(prms[3], CultureInfo.InvariantCulture);
            Visible_conf = Convert.ToBoolean(prms[4]);
        }

        protected override void InsertIdGen(int id)
        {
            Id_conf = id;
        }

        protected override bool[] ValIsStr()
        {
            return new bool[] { true, false, false };
        }
    }
}
