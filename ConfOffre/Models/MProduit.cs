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
    internal class MProduit : MObject
    {
        internal int Id_produit { get; private set; }

        internal string Nom_produit { get; private set; }
        internal string Desc_produit { get; private set; }
        internal double Prix_produit { get; private set; }

        internal MProduit(long id_obj) : base(id_obj)
        {

        }

        internal async static Task<List<MObject>> SelectAll(PostgreSQL con)
        {
            List<MObject> mProduits = new List<MObject>();
            NpgsqlDataReader reader = await con.ExecuteSql("Select * From produit;");
            while (reader.Read())
            {
                MProduit obj = new MProduit(DataCommunication.Index);
                obj.BindData(reader);
                mProduits.Add(obj);
            }
            await con.Close();
            return mProduits;
        }

        protected override void BindData(NpgsqlDataReader reader)
        {
            Id_produit = reader.GetInt32(0);
            Nom_produit = reader.GetString(1);
            Desc_produit = reader.GetString(2);
            Prix_produit = reader.GetDouble(3);
        }

        protected override string ColName()
        {
            return "produit";
        }

        protected override string[][] GetIdStr()
        {
            string[][] vs = new string[2][];
            vs[0] = new string[] { "id_produit" };
            vs[1] = new string[] { Id_produit.ToString() };
            return vs;
        }

        protected override string[] GetValName()
        {
            return new string[] { "nom_produit", "desc_produit", "prix_produit" };
        }

        protected override void InsertData(params string[] prms)
        {
            Nom_produit = prms[0];
            Desc_produit = prms[1];
            Prix_produit = double.Parse(prms[2], CultureInfo.InvariantCulture);
        }

        protected override bool[] ValIsStr()
        {
            return new bool[]{ true, true, false};
        }

        protected override void InsertIdGen(int id)
        {
            Id_produit = id;
        }
    }
}
