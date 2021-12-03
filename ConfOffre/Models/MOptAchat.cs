using ConfOffre.Tools;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfOffre.Models
{
    internal class MOptAchat : MObject
    {
        internal int Id_optAch { get; private set; }

        internal int Id_produit { get; private set; }
        internal string Nom_optAch { get; private set; }
        internal string Desc_optAch { get; private set; }

        internal MOptAchat(long id_obj) : base(id_obj)
        {

        }

        internal async static Task<List<MObject>> SelectAll(PostgreSQL con)
        {
            List<MObject> mProduits = new List<MObject>();
            NpgsqlDataReader reader = await con.ExecuteSql("Select * From optionachat;");
            while (reader.Read())
            {
                MOptAchat obj = new MOptAchat(DataCommunication.Index);
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
            Nom_optAch = reader.GetString(2);
            Desc_optAch = reader.GetString(3);
        }

        protected override string ColName()
        {
            return "optionachat";
        }

        protected override string[][] GetIdStr()
        {
            string[][] vs = new string[2][];
            vs[0] = new string[] { "id_optAch", "id_produit" };
            vs[1] = new string[] { Id_optAch.ToString(), Id_produit.ToString() };
            return vs;
        }

        protected override string[] GetValName()
        {
            return new string[] { "nom_optAch", "desc_optAch" };
        }

        protected override void InsertData(params string[] prms)
        {
            Id_produit = Convert.ToInt32(prms[0]);
            Nom_optAch = prms[1];
            Desc_optAch = prms[2];
        }

        protected override void InsertIdGen(int id)
        {
            Id_optAch = id;
        }

        protected override bool[] ValIsStr()
        {
            return new bool[] { true, true };
        }
    }
}
