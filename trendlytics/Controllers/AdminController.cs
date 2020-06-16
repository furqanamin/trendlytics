using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using trendlytics.Models;

namespace trendlytics.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            if (Session["Admin"] == null)
            {

                return RedirectToAction("Login");
            }

            return RedirectToAction("Awareness");
        }
        public Dictionary<string, object> getjson(string Folder, string filename)
        {
            string file = Server.MapPath("~/App_Data/"+Folder+"/"+filename);
            string currentJson = System.IO.File.ReadAllText(file);
            var obj = JsonConvert.DeserializeObject<Dictionary<string, object>>(currentJson);
           
            return obj;
        }

        public JsonResult likes()
        {
            var obj = getjson("Awareness", "likes.json");

            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public JsonResult searchs()
        {
            var obj = getjson("Awareness", "organic_search.json");
            Dictionary<string, object> organic_search ;
            object or; 
            obj.TryGetValue("Organic Search", out or);
            var jsonor= JsonConvert.SerializeObject(or);
            organic_search = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonor);
            List<object> organisearchcounts = new List<object>();
            foreach(var item in organic_search)
            {
                organisearchcounts.Add(item.Value);
            }
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["OrganicSearch"] = organisearchcounts;


            obj = getjson("Awareness", "paid_search.json");

            Dictionary<string, object> paid_search;
            object ps;
            obj.TryGetValue("Paid Search", out ps);
            var jsonps = JsonConvert.SerializeObject(ps);
            paid_search = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonps);
            List<object> paidsearchcounts = new List<object>();
            foreach (var item in paid_search) {
                paidsearchcounts.Add(item.Value);
                
            }
            data["PaidSearch"] = paidsearchcounts;

            obj = getjson("Awareness", "referral.json");
            Dictionary<string, object> referral;
            object rf;
            obj.TryGetValue("Referral", out rf);
            var jsonref = JsonConvert.SerializeObject(rf);
            referral = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonref);
            List<object> referalCounts = new List<object>();
            foreach (var item in referral)
            {
                referalCounts.Add(item.Value);

            }
            data["referral"] = referalCounts;
            obj = getjson("Awareness", "social.json");
            Dictionary<string, object> social;
            object so;
            obj.TryGetValue("Social", out so);
            var jsonso = JsonConvert.SerializeObject(so);
            social = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonso);
            List<object> socialCounts = new List<object>();
            foreach (var item in social)
            {
                socialCounts.Add(item.Value);

            }

            data["social"] = socialCounts;
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Awareness()
        {
            if (Session["Admin"] == null)
            {

                return RedirectToAction("Login");
            }

            Dictionary<string, object> aggregates = getjson("Awareness", "aggregates.json");
            object likes;
            aggregates.TryGetValue("Likes", out likes);
            ViewBag.likes = likes;
            object Hits;
            aggregates.TryGetValue("Hits", out Hits);
            ViewBag.hits = Hits;
            object reach;
            aggregates.TryGetValue("Total Reach", out reach);
            ViewBag.reach = reach;

            Dictionary<string, object> visits = getjson("Awareness", "geographical_traffic_Visits.json");
            var keys = visits.Keys;
            List<string> countrynames = new List<string>();
            int i = 0;
            foreach (var key in keys)
            {
                countrynames.Add(key);

            }
            ViewBag.countrynames = countrynames;
            List<string> visitslist = new List<string>();
            foreach (var item in visits)
            {
                visitslist.Add(item.Value.ToString());
            }
            ViewBag.visits = visitslist;

            Dictionary<string, object> percents = getjson("Awareness", "geographical_traffic_Percentage.json");
            List<string> percentslist = new List<string>();
            foreach (var item in percents)
            {
                percentslist.Add(item.Value.ToString());
            }
            ViewBag.percents = percentslist;

            return View();

        }

        //public JsonResult ()
        //{

        //}
        public ActionResult Engagement()
        {
            if (Session["Admin"] == null)
            {

                return RedirectToAction("Login");
            }

            Dictionary<string, object> aggregates = getjson("Awareness", "engagement_aggregate.json");
            object posts;
            aggregates.TryGetValue("Posts", out posts);
            ViewBag.posts = posts;

            object post_likes;
            aggregates.TryGetValue("Post Likes", out post_likes);
            ViewBag.likes =post_likes ;
            object comments;
            aggregates.TryGetValue("Comments", out comments);
            ViewBag.comments = comments;
            object TotalImpressions;
            aggregates.TryGetValue("Total Impressions", out TotalImpressions);
            ViewBag.TotalImpressions = TotalImpressions;
            object TotalEngagements;
            aggregates.TryGetValue("Total Engagement", out TotalEngagements);
            ViewBag.TotalEngagement = TotalEngagements;
            object loyalImpressions;
            aggregates.TryGetValue("Loyal Impressions", out loyalImpressions);
            ViewBag.loyalImpressions = loyalImpressions;
            object loyalEngagements;
            aggregates.TryGetValue("Loyal Engagement", out loyalEngagements);
            ViewBag.loyalEngagements = loyalEngagements;
            object TotalInteractions;
            aggregates.TryGetValue("Total Interactions", out TotalInteractions);
            ViewBag.TotalInteractions = TotalInteractions;

            object BounceRate;
            aggregates.TryGetValue("Bounce Rate", out BounceRate);
            ViewBag.BounceRate = BounceRate;


            Dictionary<string, object> obj = getjson("Engagement", "comments.json");
            object comm;
            obj.TryGetValue("Comments", out comm);
            var jsonim = JsonConvert.SerializeObject(comm);
            List<object> comment= JsonConvert.DeserializeObject<List<object>>(jsonim);
            //List<string> comment;
            //foreach(var item in comm)
            //{

            //}
            ViewBag.Com = comment;
            object reviews;
            obj.TryGetValue("Label", out reviews) ;
            jsonim = JsonConvert.SerializeObject(reviews);
            List<object> reviewslist = JsonConvert.DeserializeObject<List<object>>(jsonim);

            ViewBag.reviews = reviewslist;
            return View();
        }

        public JsonResult Impressions()
        {
            var obj = getjson("Awareness", "Impression.json");
            Dictionary<string, object> impression;
            object im;
            obj.TryGetValue("Reach", out im);
            var jsonim = JsonConvert.SerializeObject(im);
            impression = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonim);
            List<object> impressioncounts = new List<object>();
            foreach (var item in impression)
            {
                impressioncounts.Add(item.Value);
            }
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["Impressions"] = impressioncounts;
            Dictionary<string, object> engagement;
            object em;
            obj.TryGetValue("Engagement", out em);
            var jsonem = JsonConvert.SerializeObject(em);
            engagement = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonem);
            List<object> engagementcounts = new List<object>();
            foreach (var item in engagement)
            {
                engagementcounts.Add(item.Value);
            }
            data["Engagements"] = engagementcounts;


            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UserByDay()
        {
            var obj = getjson("Awareness", "UserByDay.json");
            object user;
            obj.TryGetValue("Users By Day", out user);
            Dictionary<string, object> data;
            var jsonim = JsonConvert.SerializeObject(user);
            data = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonim);
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        public ActionResult WOM()
        {
            if (Session["Admin"] == null)
            {

                return RedirectToAction("Login");
            }

            Dictionary<string, object> aggregates = getjson("WOM", "aggregates.json");
            object Shares;
            aggregates.TryGetValue("Shares", out Shares);
            ViewBag.Shares = Shares;

            object reach;
            aggregates.TryGetValue("New Audience Reach", out reach);
            ViewBag.reach = reach;
            object Engagement;
            aggregates.TryGetValue("New Audience Engagement", out Engagement);
            ViewBag.Engagement = Engagement;
            object Impressions;
            aggregates.TryGetValue("New Audience Impressions", out Impressions);
            ViewBag.Impressions = Impressions;
           





            return View();
        }

        public JsonResult NonAud()
        {

            var obj = getjson("WOM", "reach_impressions_engagement.json");
            Dictionary<string, object> reach;
            object re;
            obj.TryGetValue("Non Audience Reach", out re);
            var jsonim = JsonConvert.SerializeObject(re);
            reach = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonim);
            List<object> reachcounts = new List<object>();
            foreach (var item in reach)
            {
                reachcounts.Add(item.Value);
            }
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["Reach"] = reachcounts;
            Dictionary<string, object> engagement;
            object em;
            obj.TryGetValue("Non Audience Engagement", out em);
            var jsonem = JsonConvert.SerializeObject(em);
            engagement = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonem);
            List<object> engagementcounts = new List<object>();
            foreach (var item in engagement)
            {
                engagementcounts.Add(item.Value);
            }
            data["Engagements"] = engagementcounts;

            Dictionary<string, object> Impressions;
            object imp;
            obj.TryGetValue("Non Audience Impressions", out imp);
            var jsonimp = JsonConvert.SerializeObject(imp);
            Impressions = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonimp);
            List<object> impressioncounts = new List<object>();
            foreach (var item in Impressions)
            {
                impressioncounts.Add(item.Value);
            }
            data["Impressions"] = impressioncounts;

            return Json(data, JsonRequestBehavior.AllowGet);


        }
        public JsonResult DeviceBrowser()
        {
            var obj = getjson("Advanced", "Device_browser.json");
            Dictionary<string, object> data = new Dictionary<string, object>();
            Dictionary<string, object> reach;
            object count_dict;
            obj.TryGetValue("count", out count_dict);

            var jsonim = JsonConvert.SerializeObject(count_dict);
            reach = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonim);
           // List<object> reachcounts = new List<object>();
            foreach (var item in reach)
            {
                data.Add(item.Key, item.Value);
            }
            Dictionary<string, object> non_zero;
            //object coun;
            obj.TryGetValue("count of non-zero revenue", out count_dict);

            jsonim = JsonConvert.SerializeObject(count_dict);
            non_zero = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonim);
            foreach (var item in non_zero)
            {
                data.Add(item.Key, item.Value);
            }
            Dictionary<string, object> mean;
            //object coun;
            obj.TryGetValue("mean", out count_dict);

            jsonim = JsonConvert.SerializeObject(count_dict);
            mean = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonim);
            foreach (var item in mean)
            {
                data.Add(item.Key, item.Value);
            }

            // data["Reach"] = reachcounts;
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeviceOS()
        {
            var obj = getjson("Advanced", "device_operatingSystem.json");
            Dictionary<string, object> data = new Dictionary<string, object>();
            Dictionary<string, object> reach;
            object count_dict;
            obj.TryGetValue("count", out count_dict);

            var jsonim = JsonConvert.SerializeObject(count_dict);
            reach = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonim);
            // List<object> reachcounts = new List<object>();
            foreach (var item in reach)
            {
                data.Add(item.Key, item.Value);
            }
            Dictionary<string, object> non_zero;
            //object coun;
            obj.TryGetValue("count of non-zero revenue", out count_dict);

            jsonim = JsonConvert.SerializeObject(count_dict);
            non_zero = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonim);
            foreach (var item in non_zero)
            {
                data.Add(item.Key, item.Value);
            }
            Dictionary<string, object> mean;
            //object coun;
            obj.TryGetValue("mean", out count_dict);

            jsonim = JsonConvert.SerializeObject(count_dict);
            mean = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonim);
            foreach (var item in mean)
            {
                data.Add(item.Key, item.Value);
            }

            // data["Reach"] = reachcounts;
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeviceCategory()
        {
            var obj = getjson("Advanced", "deviceCategory.json");
            Dictionary<string, object> data = new Dictionary<string, object>();
            Dictionary<string, object> reach;
            object count_dict;
            obj.TryGetValue("count", out count_dict);

            var jsonim = JsonConvert.SerializeObject(count_dict);
            reach = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonim);
            // List<object> reachcounts = new List<object>();
            foreach (var item in reach)
            {
                data.Add(item.Key, item.Value);
            }
            Dictionary<string, object> non_zero;
            //object coun;
            obj.TryGetValue("count of non-zero revenue", out count_dict);

            jsonim = JsonConvert.SerializeObject(count_dict);
            non_zero = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonim);
            foreach (var item in non_zero)
            {
                data.Add(item.Key, item.Value);
            }
            Dictionary<string, object> mean;
            //object coun;
            obj.TryGetValue("mean", out count_dict);

            jsonim = JsonConvert.SerializeObject(count_dict);
            mean = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonim);
            foreach (var item in mean)
            {
                data.Add(item.Key, item.Value);
            }

            // data["Reach"] = reachcounts;
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CountChannel()
        {
            var obj = getjson("Advanced", "Counts_by_Channel.json");
            Dictionary<string, object> data = new Dictionary<string, object>();
            Dictionary<string, object> reach;
            object count_dict;
            obj.TryGetValue("Counts_Channel", out count_dict);

            var jsonim = JsonConvert.SerializeObject(count_dict);
            reach = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonim);
            // List<object> reachcounts = new List<object>();
            foreach (var item in reach)
            {
                data.Add(item.Key, item.Value);
            }
            
            // data["Reach"] = reachcounts;
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult RevenueChannel()
        {
            var obj = getjson("Advanced", "Revenue_by_Channel.json");
            Dictionary<string, object> data = new Dictionary<string, object>();
            Dictionary<string, object> reach;
            object count_dict;
            obj.TryGetValue("Revenue_Channel", out count_dict);

            var jsonim = JsonConvert.SerializeObject(count_dict);
            reach = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonim);
            // List<object> reachcounts = new List<object>();
            foreach (var item in reach)
            {
                data.Add(item.Key, item.Value);
            }

            // data["Reach"] = reachcounts;
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Sentiment()
        {
            var obj = getjson("Engagement", "sentiment.json");
            Dictionary<string, object> data = new Dictionary<string, object>();
          
            object count;
            obj.TryGetValue("Positive", out count);

            data["Positive"] = count;
            obj.TryGetValue("Negative", out count);
            data["Negative"] = count;
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Insights()
        {
            if (Session["Admin"] == null)
            {

                return RedirectToAction("Login");
            }

            return View();
        }
        public User GetSessionAdmin()
        {
            if (Session["Admin"] != null)
            {
                return Session["Admin"] as User;
            }

            return null;
        }

        public void SetSessionAdmin(User obj)
        {
            Session["Admin"] = obj;
        }

        public ActionResult SignOut()
        {
            Session["Admin"]= null;
            return RedirectToAction("Login");
        }
        public ActionResult Login()

        {
           
            if (Session["Admin"] != null)
            {

                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpPost]
        public string Login(string username, string password)
        {
            string ret = "Error";
            string whereclause = "EMAIL='" + username + "'" + "or user_name='" + username + "'";
            List<User> lstAdmin = UserManager.GetUser(whereclause, null);
            if (lstAdmin.Count > 0)
            {
                if (lstAdmin.First().PASSWORD.Equals(password))
                {
                    SetSessionAdmin(lstAdmin.First());
                    ret = "success";

                }
                else
                {
                    ret = "Invalid Credentials";
                }
            }
            return ret;

        }

        public ActionResult SignUp()

        {
            if (Session["Admin"] != null)
            {

                return RedirectToAction("Index");
            }

          
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(User model)
        {


           List<User> lstUser = UserManager.GetUser(" EMAIL = '" + model.EMAIL + "'", null);
            if (lstUser.Count > 0)
            {
                return Content("An account already exists with the given Email");
            }
            lstUser = UserManager.GetUser("USER_NAME='" + model.USERNAME + "'", null);
            if (lstUser.Count > 0)
            {
                return Content("Username already exists");
            }

            MySqlConnection conn = Shared.BaseManager.PrimaryConnection();


            conn.Open();
            var transaction = conn.BeginTransaction();
            string ret =UserManager.SaveNGOmember(model, conn, transaction);
            if (ret.Equals("OK"))
            {
                transaction.Commit();
                conn.Close();
                conn.Dispose();

                string token = "success";
                return Content(token);
            }
            else
            {
                transaction.Rollback();
                conn.Close();
                conn.Dispose();
                return Content("Error");
            }
        }


    }
}