using ConfOffre.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfOffre.Tools
{
    internal class DataCommunication
    {
        internal readonly PostgreSQL sql;
        private readonly Dictionary<long, MObject> datas;

        private static long act_index = 0;
        internal static long Index { get { return act_index++; } }

        internal DataCommunication()
        {
            sql = new PostgreSQL();
            datas = new Dictionary<long, MObject>();
        }

        internal async Task GetData()
        {
            FillData(await MProduit.SelectAll(sql));
            FillData(await MOptAchat.SelectAll(sql));
            FillData(await MConfiguration.SelectAll(sql));
            FillData(await MStock.SelectAll(sql));
        }

        internal async Task<MObject> CreateObj(MObject obj, params string[] parms)
        {
            obj = await MObject.Insert(obj, this, parms);
            datas.Add(obj.id_object, obj);
            return obj;
        }

        internal async Task UpdateObj(MObject obj, params string[] parms)
        {
            await sql.ExecuteCmd(obj.Update(parms));
        }

        internal async Task DeleteObj(MObject obj)
        {
            await sql.ExecuteCmd(obj.Delete());
            datas.Remove(obj.id_object);
        }

        private void FillData(List<MObject> objs)
        {
            foreach(MObject obj in objs)
            {
                datas.Add(obj.id_object, obj);
            }
        }

        #region GET_DATA

        internal T[] GetData<T>() where T : MObject
        {
            MObject[] objs = datas.Values.ToArray();
            List<T> result = new List<T>();
            foreach(MObject obj in objs)
            {
                if(obj is T)
                    result.Add((T)obj);

            }
            return result.ToArray();
        }

        internal MOptAchat[] GetOptAchatsByIdProduit(int id_produit)
        {
            MOptAchat[] objs = GetData<MOptAchat>();
            List<MOptAchat> selectedObjs = new List<MOptAchat>();

            foreach(MOptAchat obj in objs)
            {
                if(obj.Id_produit == id_produit)
                    selectedObjs.Add(obj);
            }

            return selectedObjs.ToArray();
        }

        internal MConfiguration[] GetConfByIdOptAchat(int id_optAch)
        {
            MConfiguration[] objs = GetData<MConfiguration>();
            List<MConfiguration> selectedObjs = new List<MConfiguration>();

            foreach (MConfiguration obj in objs)
            {
                if (obj.Id_optAch == id_optAch)
                    selectedObjs.Add(obj);
            }

            return selectedObjs.ToArray();
        }

        #endregion
    }
}
