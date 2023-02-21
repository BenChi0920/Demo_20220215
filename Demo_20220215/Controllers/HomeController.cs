using Demo_20220215.Models;
using Demo_20220215.Severices;
using Microsoft.Ajax.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Web.Mvc;
using System.Web.UI;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Demo_20220215.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            EMPMethods eMPMethods = new EMPMethods();
            List<EM_Model> emplist = eMPMethods.GetEMPData();
            return View(emplist);
        }

        public PartialViewResult empdata(string EmpNO = "")
        {
            List<EM_Model> emplist = new List<EM_Model>();
            if (Session["empdata"] == null)
            {
                EMPMethods eMPMethods = new EMPMethods();
                emplist = eMPMethods.GetEMPData();
                Session["empdata"] = emplist;
            }
            else
            {
                emplist = Session["empdata"] as List<EM_Model>;
                //empList = (List<EmployeeModel>)Session["EmpData"
            }
            if (string.IsNullOrEmpty(EmpNO))
            {
                return PartialView(emplist);
            }
            var result = new List<EM_Model>();
            //功能一樣
            //foreach (EM_Model emp in emplist)
            //{
            //    if (item.EmpNO == EmpNO)
            //    {
            //        result.Add(item);
            //    }
            //}

            return PartialView(emplist.Where(x => x.EmpNo == EmpNO).ToList());

            //功能一樣
            //result=(from x in emplist
            //        where x.EmpNo==EmpNO
            //        select x).ToList();

            return PartialView(result);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            Session["D1"] = "hELLO SESSION";
            ViewBag.D1 = "hELLO viewbag";
            ViewData["D1"] = "hELLO viewdata";
            TempData["D1"] = "hELLO tempdata";

            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult edit(string EmpNo)
        {
            MessageInfo message = new MessageInfo() { IsSuccess = true, Msg = "" };

            if (string.IsNullOrEmpty(EmpNo))
            {
                return View(new EM_Model());
            }
            if (Session["empdata"] == null)
            {
                return View(new EM_Model());
            }
            EM_Model result = ((List<EM_Model>)Session["empdata"]).Where(x => x.EmpNo == EmpNo).FirstOrDefault();

            return View(result);

        }

        [HttpPost]
        public JsonResult EditPost(EM_Model empdata)
        {
            MessageInfo messageInfo = new MessageInfo() { IsSuccess = true, Msg = "" };
            //todo 檢核資料
            if (Session["empdata"] == null)
            {
                messageInfo.IsSuccess = false;
                messageInfo.Msg = "No data";
                return Json(messageInfo);
            }
            List<EM_Model> result = (List<EM_Model>)Session["empdata"];

            if (result.Where(x => x.EmpNo == empdata.EmpNo).Count() == 0)
            {
                messageInfo.IsSuccess = false;
                messageInfo.Msg = "NO DATA";
                return Json(messageInfo);
            }
            var emp = result.Where(x => x.EmpNo == empdata.EmpNo).FirstOrDefault();
            emp.Name = empdata.Name;
            emp.Ext = empdata.Ext;
            Session["EmpData"] = result;
            return Json(messageInfo);
        }


        [HttpPost]
        public JsonResult DeletePost(string EmpNo)
        {
            MessageInfo messageInfo = new MessageInfo() { IsSuccess = true, Msg = "" };
            if (Session["EmpData"] == null)
            {
                messageInfo.IsSuccess = false;
                messageInfo.Msg = "查無資料 ";
                return Json(messageInfo);

            }
            List<EM_Model> empDatas = Session["EmpData"] as List<EM_Model>;


            if (empDatas.Where(x => x.EmpNo == EmpNo).Count() == 0)
            {
                messageInfo.IsSuccess = false;
                messageInfo.Msg = "查無資料 ";
                return Json(messageInfo);
            }

            empDatas.Remove(empDatas.Where(x => x.EmpNo == EmpNo).FirstOrDefault());
            Session["EmpData"] = empDatas;
            return Json(messageInfo);
        }

        [HttpPost]
        public JsonResult AddPost(EM_Model employee)
        {
            MessageInfo messageInfo = new MessageInfo() { IsSuccess = true, Msg = "" };
            try
            {
                //EM_Model employee = new EM_Model();
                List<EM_Model> result = (List<EM_Model>)Session["EmpData"];
                if (Session["EmpData"] == null)
                {
                    //messageInfo.IsSuccess = false;
                    //messageInfo.Msg = "查無資料";
                    return Json(messageInfo);
                }

                result.Add(employee);

                return Json(messageInfo);
            }
            catch (System.Exception)
            {

                throw;
            }
           
        }
        public ActionResult addPost()
        {
            //MessageInfo messageInfo = new MessageInfo();
            //if (Session["EmpData"] == null)
            //{
            //    return View(new EmployeeModels());
            //}
            //EM_Model employee = new EM_Model();
            //employee.Name = Name;
            //employee.EmpNo = EmpNo;
            //employee.Ext = Ext;
            //((List<EM_Model>)Session["EmpData"]).Add(employee);
            //return View(employee);
            return View();
        }

    }
}