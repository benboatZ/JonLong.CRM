﻿using JonLong.CRM.Web.Common;
using JonLong.CRM.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JonLong.CRM.BLL;
using JonLong.CRM.Models;
using JonLong.CRM.Utilities;

namespace JonLong.CRM.Web.Controllers
{
    public class OrderVarianceController : Controller
    {

        [RoleAuthorize]
        public ActionResult Index()
        {
            try
            {
                var model = new VarianceListViewModel();
                var user = AccountHelper.GetLoginUserInfo(HttpContext.User.Identity);
                model.Variances = OrderVarianceManager.Instance.LoadOrderVariance(AccountHelper.IsSuperAdmin(user), user.CustomerCode);
                return View(model);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.ToString();
                return View("Error");

            }
        }

        [RoleAuthorize]
        public ActionResult VarianceDetail(
              string customerCode
            , string etd
            , string bundleNo
            , string containerType
            , string containerNo)
        {
            try
            {
                var model = new VarianceDetailViewModel();
                DateTime sendDate;
                DateTime.TryParse(etd, out sendDate);
                model.VarianceDetails = OrderVarianceManager.Instance.LoadDetail(
                    customerCode,
                    sendDate,
                    bundleNo,
                    containerType,
                    containerNo);

                model.VarianceCount = model.VarianceDetails.Count;

                foreach (var item in model.VarianceDetails)
                {
                    model.Total += item.Total;
                    model.Size1Total += item.Size1;
                    model.Size2Total += item.Size2;
                    model.Size3Total += item.Size3;
                    model.Size4Total += item.Size4;
                    model.Size5Total += item.Size5;
                    model.Size6Total += item.Size6;
                    model.Size7Total += item.Size7;
                    model.Size8Total += item.Size8;
                    model.Size9Total += item.Size9;
                    model.Size10Total += item.Size10;
                    model.Size11Total += item.Size11;
                    model.Size12Total += item.Size12;
                    model.Size13Total += item.Size13;
                    model.Size14Total += item.Size14;
                    model.Size15Total += item.Size15;
                    model.Size16Total += item.Size16;
                    model.Size17Total += item.Size17;
                    model.Size18Total += item.Size18;
                }

                var user = AccountHelper.GetLoginUserInfo(HttpContext.User.Identity);
                if (AccountHelper.IsSuperAdmin(user))
                {
                    model.ShoeSizes = ShoeManager.Instance.LoadShoeSize(Constants.SuperAdminDefaultCustomerCode);
                }
                else
                {
                    model.ShoeSizes = ShoeManager.Instance.LoadShoeSize(user.CustomerCode);
                }

                return PartialView(model);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.ToString();
                return View("Error");
            }

        }

    }
}