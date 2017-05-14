using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OA.WebApp.Controllers
{
    public class LoginController : Controller
    {


        // GET: Login
        //调用服务类
        IBLL.IUserInfoService UserInfoService = new BLL.UserInfoService();

        public ActionResult Index()
        {
            CheckCookieInfo();
            return View();
        }

        #region 校验验证码是否正确
        public ActionResult CheckLogin()
        {
            //判断验证码是否正确
            string validateCode = Session["validateCode"] == null ? string.Empty : Session["validateCode"].ToString();
            if(string.IsNullOrEmpty(validateCode))
            {
                return Content("no:验证码错误！");
            }
            else
            {
                Session["validateCode"] = null;
                string txtCode = Request["vCode"];
                //忽略大小写
                if (!validateCode.Equals(txtCode, StringComparison.InvariantCultureIgnoreCase))
                {
                    return Content("no:验证码错误！！");
                }

                //判断用户名与密码
                string userName = Request["LoginCode"];
                string userPwd = Request["LoginPwd"];

                UserInfo userInfo = UserInfoService.LoadEntities(u => u.UName == userName && u.UPwd == userPwd).FirstOrDefault();

                if(userInfo != null)
                {
                    Session["userInfo"] = userInfo;

                    //记住用户名和密码实现
                    if (!string.IsNullOrEmpty(Request["checkMe"]))
                    {
                        HttpCookie cookie1 = new HttpCookie("cp1", userInfo.UName);
                        HttpCookie cookie2 = new HttpCookie("cp2", 
                            Common.webCommon.GetMd5String(Common.webCommon.GetMd5String(userInfo.UPwd)));

                        //设置cookie保存的时间
                        cookie1.Expires = DateTime.Now.AddDays(3);
                        cookie2.Expires = DateTime.Now.AddDays(3);

                        Response.Cookies.Add(cookie1);
                        Response.Cookies.Add(cookie2);
                    }
                    return Content("ok:登录成功！！");
                }
                else
                {
                    return Content("no:用户名或密码错误");
                }
            }
        }
        #endregion

        #region 展示验证码
        public ActionResult ShowValidateCode()
        {
            Common.ValidateCode validateCode = new Common.ValidateCode();
            string code = validateCode.CreateValidateCode(4);
            Session["validateCode"] = code;
            byte[] buffer = validateCode.CreateValidateGraphic(code);
            return File(buffer, "image/jpeg");
        }
        #endregion

        #region 记住我实现
        private void CheckCookieInfo()
        {
            if(Request.Cookies["cp1"] != null && Request.Cookies["cp2"] != null)
            {
                string userName = Request.Cookies["cp1"].Value;
                string userPwd = Request.Cookies["cp2"].Value;

                //判断Cookie中存储的用户密码和用户名是否正确
                var userInfo  = UserInfoService.LoadEntities(u => u.UName == userName).FirstOrDefault();
                if(userInfo  != null)
                {
                    //注意：将用户的密码保存到数据库时一定要加密
                    //由于数据库中存储的数据面是明文，所以这里需要将数据库中存储的密码两次MD5运行以后再进行比较
                    if (Common.webCommon.GetMd5String(Common.webCommon.GetMd5String(userInfo.UPwd)) == userPwd)
                    {
                        Session["userInfo"] = userInfo;
                        Response.Redirect("/home/Index");
                    }
                }
                Response.Cookies["cp1"].Expires = DateTime.Now.AddDays(-1);
                Response.Cookies["cp2"].Expires = DateTime.Now.AddDays(-1);
            }
        }
        #endregion

        #region 退出登录
        public ActionResult Logout()
        {
            if(Request.Cookies["cp1"] != null || Request.Cookies["cp2"] != null)
            {
                //用户退出清除cookie中的登录信息
                HttpCookie cookie1 = Request.Cookies["cp1"];
                HttpCookie cookie2 = Request.Cookies["cp2"];

                cookie1.Expires = DateTime.Now.AddDays(-1);
                cookie2.Expires = DateTime.Now.AddDays(-1);
            }
            return Redirect("/Login/Index");
        }
        #endregion


    }
}