using ConfOffre.Tools;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfOffre.Models
{
    internal abstract class MObject
    {
        internal long id_object { get; private set; }

        internal MObject(long id_obj)
        {
            id_object = id_obj;
        }

        internal async static Task<MObject> Insert(MObject obj, DataCommunication dc, string[] prms, params string[] id_rel)
        {
            bool[] isStr = obj.ValIsStr();
            if (prms.Length != isStr.Length) throw new Exception("err params");
            List<string> datainsert = new List<string>();
            datainsert.AddRange(id_rel);
            datainsert.AddRange(prms);
            obj.InsertData(datainsert.ToArray());
            string cmd = "INSERT INTO " + obj.ColName() + " VALUES (";
            for (int i = 0; i < id_rel.Length; i++) cmd += id_rel[i] + ',';
            cmd += "default";
            for (int i = 0; i < prms.Length; ++i)
            {
                cmd += ',';
                if (isStr[i])
                {
                    cmd += '\'' + prms[i] + '\'';
                }
                else
                {
                    cmd += prms[i];
                }
            }
            cmd += ") returning " + obj.GetIdStr()[0][0] + ';';
            NpgsqlDataReader r = await dc.sql.ExecuteSql(cmd);
            await r.ReadAsync();
            obj.InsertIdGen(r.GetInt32(0));
            await dc.sql.Close();
            return obj;
        }

        internal string Update(params string[] prms)
        {
            string cmd = "Update " + ColName() + " Set ";
            string[] valnames = GetValName();
            bool[] valstr = ValIsStr();
            if (prms.Length != valnames.Length) throw new Exception("not same data");

            for (int i = 0; i < valnames.Length; ++i)
            {
                if (i != 0) cmd += ",";
                cmd += valnames[i] + '=';
                if (valstr[i])
                {
                    cmd += '\'' + prms[i] + '\'';
                }
                else
                {
                    cmd += prms[i];
                }
            }
            List<string> datainsert = new List<string>();
            string[] dataSupp = GetIdStr()[1];
            for(int i = 1;i < dataSupp.Length; ++i)
            {
                datainsert.Add(dataSupp[i]);
            }
            datainsert.AddRange(prms);
            this.InsertData(datainsert.ToArray());
            return cmd + ' ' + GetWhere() + ';';
        }

        internal string Delete()
        {
            return "Delete From " + ColName() + " " + GetWhere() + ';';
        }

        private string GetWhere()
        {
            string where = "Where ";
            string[][] ids = GetIdStr();
            for (int i = 0; i < ids[0].Length; ++i)
            {
                if (i != 0) where += " AND ";
                where += ids[0][i] + '=' + ids[1][i];
            }
            return where;
        }

        protected abstract void InsertData(params string[] prms);

        protected abstract void InsertIdGen(int id);

        protected abstract string ColName();

        protected abstract string[][] GetIdStr();

        protected abstract bool[] ValIsStr();

        protected abstract string[] GetValName();

        protected abstract void BindData(NpgsqlDataReader reader);
    }
}
