using Demo_20220215.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo_20220215.Severices
{
    public class EMPMethods
    {
        /// <summary>
        /// 取得Default Emp data
        /// </summary>
        /// <returns>list的員工物件 ///</returns>
       public List<EM_Model> GetEMPData()
        {
        List<EM_Model>eM_Models= new List<EM_Model>();
            eM_Models.Add(new EM_Model() { EmpNo = "CT3040", Name = "紀昱呈0",Ext = 1600 });
            eM_Models.Add(new EM_Model() { EmpNo = "CT1233", Name = "紀昱呈1",Ext = 1231 });
            eM_Models.Add(new EM_Model() { EmpNo = "CT1234", Name = "紀昱呈2",Ext = 1623 });
           
            return eM_Models;
        }
    }
}