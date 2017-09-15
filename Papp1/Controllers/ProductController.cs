using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Papp1.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Web.Http.Controllers;
using System.Security.Cryptography;
using System.Text;

namespace Papp1.Controllers
{
    public class ProductController : ApiController
    {
        Product[] products = new Product[]
                            {
                                new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 },
                                new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M },
                                new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M }
                            };

        public IEnumerable<Product> GetAllProducts()
        {
            return products;
        }

        public IHttpActionResult GetProduct(int id)
        {
            var product = products.FirstOrDefault((p) => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        //public IHttpActionResult GetCategory(string  ids)
        //{
        //    var product = products.FirstOrDefault((p) => p.Category == ids);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(product);
        //}

    }

    public class TestJsonController : ApiController
    {
        //http://huan-lin.blogspot.com/2013/01/aspnet-web-api-and-json.html

        //public Dictionary<string, string> Get()
        //{
        //    var result = new Dictionary<string, string>()
        //    {
        //        {"001", "Banana"},
        //        {"002", "Apple"},
        //        {"003", "Orange"}
        //    };
        //    return result;
        //}

        //public Dictionary<string, string> HandMadeJson()
        //{
        //    var result = new Dictionary<string, string>()
        //    {
        //        {"001", "Banana"},
        //        {"002", "Apple"},
        //        {"003", "Orange"}
        //    };
        //    return JsonConvert.SerializeObject(result);  //??? error
        //}

        [HttpGet, HttpPost] // 讓此方法可同時接受 HTTP GET 和 POST 請求.
        public HttpResponseMessage HandMadeJson()
        {
            var data = new Dictionary<string, string>()
                {
                    {"001", "Banana"},
                    {"002", "Apple"},
                    {"003", "Orange"}
                };

            string json = JsonConvert.SerializeObject(data);
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StringContent(json);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return result;
        }

    }
    
    public class TestWebApiController : ApiController
    {
        [ActionName("yyy")]
        [HttpGet, HttpPost]
        // GET /api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }


        // GET /api/values/5
        //改一下這邊！
        //public string Get(int id)
        //{
        //    var data = new Dictionary<string, string>()
        //            {
        //                {"value", id.ToString()}
        //            };
        //    string json = JsonConvert.SerializeObject(data);

        //    return json;
        //}

        //public HttpResponseMessage Getbyfunc(int id)
        //{
        //    var data = new Dictionary<string, string>()
        //            {
        //                {"value", id.ToString()}
        //            }; 
        //    string json = JsonConvert.SerializeObject(data);
        //    var result = new HttpResponseMessage(HttpStatusCode.OK);
        //    result.Content = new StringContent(json);
        //    result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        //    return result;
        //}

        [ActionName("yyy")]
        [HttpGet, HttpPost]
        public HttpResponseMessage Getbyfunc(string id) // 以GET開頭的function 有參數只能有一個
        {
            var data = new Dictionary<string, string>()
                    {
                        {"value", id }
                    };
            string json = JsonConvert.SerializeObject(data);
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StringContent(json);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return result;
        }

        //public HttpResponseMessage Getbyid(string id)
        //{
        //    var data = new Dictionary<string, string>()
        //        {
        //            {"value", id}
        //        };

        //    string json = JsonConvert.SerializeObject(data);
        //    var result = new HttpResponseMessage(HttpStatusCode.OK);
        //    result.Content = new StringContent(json);
        //    result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        //    return result;
        //}

        // POST /api/values
        public void Post(string value)
        {
        }

        // PUT /api/values/5
        public void Put(int id, string value)
        {
        }

        // DELETE /api/values/5
        public void Delete(int id)
        {
        }


    }

    //透過config.Routes.MapHttpRoute 定義 route map , 直接指定 actinname & 多變數 
    public class TestWebApi2PController : ApiController
    { 
        [HttpGet, HttpPost, ActionName("yyy")]
        // GET /api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
         
        [HttpGet, HttpPost, ActionName("yyy")]
        public HttpResponseMessage Getbyfunc(string id1, string id2) // 以GET開頭的function 有參數只能有一個
        {
            var data = new Dictionary<string, string>()
                    {
                        {"value1", id1 },  {"value2", id2 }
                    };
            string json = JsonConvert.SerializeObject(data);
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StringContent(json);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return result;
        }

        [HttpGet, HttpPost, ActionName("xxx")]
        public HttpResponseMessage Getbyfunc_x(string id1, string id2) // 以GET開頭的function 有參數只能有一個
        {
            var data = new Dictionary<string, string>()
                    {
                        {"xxx_value1", id1 },  {"xxx_value2", id2 }
                    };
            string json = JsonConvert.SerializeObject(data);
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StringContent(json);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return result;
        }
    }

    //自己定義 route map ,不需要定義 config.Routes.MapHttpRoute
    public class TestWebApi3PController : ApiController
    { 
        [HttpGet, HttpPost, Route("myroutemap/{id}")]
        public HttpResponseMessage Getbyfunc(string id) // 以GET開頭的function 有參數只能有一個
        {
            var data = new Dictionary<string, string>()
                    {
                        {"value", id }
                    };
            string json = JsonConvert.SerializeObject(data);
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StringContent(json);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return result;
        }
    }


    public class TestWebApi4PController : ApiController
    {
        //[HttpGet, HttpPost, Route("myroutemap/{id}")]
        //public HttpResponseMessage Getbyfunc(string id) // 以GET開頭的function 有參數只能有一個
        //{
        //    var data = new Dictionary<string, string>()
        //            {
        //                {"value", id }
        //            };
        //    string json = JsonConvert.SerializeObject(data);
        //    var result = new HttpResponseMessage(HttpStatusCode.OK);
        //    result.Content = new StringContent(json);
        //    result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        //    return result;
        //}
        [HttpGet, HttpPost,Route("myDemo1/{id}")]
        public string Demo1(int id)//Demo 1：簡單參數
        {
            return id.ToString();
        }

        [HttpGet, Route("myDemo2/{id}")]
        public string Demo2(int? id = null) //Demo2：可有可無的參數
        {
            if (id == null)
            {
                return "id, please!";
            }
            return id.ToString();
        }

        //[HttpGet, HttpPost, Route("myDemo3/{id1}")]
        //public string Demo3(string id1,[FromBody]string id2)//Demo 3：同時接受 GET 和 POST
        //{
        //    return "ID1:" + id1   + "/ "+"ID2:" + id2   ;
        //}

        [HttpGet, HttpPost, Route("myDemo4/{id}")]
        public string Demo4(string id) // ,FromBody]  string cmp
        {
            HttpContent requestContent = Request.Content;
            string jsonContent = requestContent.ReadAsStringAsync().Result;

            //System.Collections.Specialized.NameValueCollection a1 = requestContent.ReadAsFormDataAsync().Result;
            
            return String.Format("ID: {0}, Company: {1}", id,jsonContent); 
        }

        [HttpPost, Route("myDemo5")]
        // POST: api/MemberData
        public Dictionary<string, string> Post([FromBody]string value)//
        {
            //if (value == null)
            //{
            //    throw new ArgumentNullException();
            //}
            HttpContent requestContent = Request.Content;
            string jsonContent = requestContent.ReadAsStringAsync().Result;
            System.Collections.Specialized.NameValueCollection a1 = requestContent.ReadAsFormDataAsync().Result;

            Dictionary<string, string> message = new Dictionary<string, string>()
                                {
                                    {"test", "test11"},
                                    {"test2", "MESSAGE"}
                                };         
            return message;

            //    var data = new Dictionary<string, string>()
            //        {
            //            {"001", "Banana"},
            //            {"002", "Apple"},
            //            {"003", "Orange"}
            //        };
            //    string json = JsonConvert.SerializeObject(data);
            //    var result = new HttpResponseMessage(HttpStatusCode.OK);
            //    result.Content = new StringContent(json);
            //    result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            //    return result;

            //return JsonConvert.SerializeObject(message);
        }

        //[ActionName("exist")]
        //[System.Web.Mvc.HttpPost]
        [HttpPost, Route("myDemo5/FromBody")]
        public Dictionary<string, string> CheckIfUserExist([FromBody] string va) //[FromBody]
        {
            bool errors = false;
            string error_msg = "";
            HttpContent requestContent = Request.Content;
            string jsonContent = requestContent.ReadAsStringAsync().Result;
            System.Collections.Specialized.NameValueCollection a1 = requestContent.ReadAsFormDataAsync().Result;
            string aa1 = Security.EncryptStringAES(va, "ChanghuaHospital");
            string aa2 = Security.EncryptStringAES(DateTime.Now.ToString());
            string bb1 = Security.DecryptStringAES(aa1, "ChanghuaHospital1");
            string bb2 = Security.DecryptStringAES(aa2);
            string cc1 = Security.EncryptRSAt(va,ref errors,ref error_msg);
            string cc2 = Security.DecryptRSAt(cc1, ref errors, ref error_msg);


            Dictionary<string, string> message = new Dictionary<string, string>()
                                {
                                    {"name", aa1},
                                    {"DateTime",aa2  }
                                };
            
            return message;

            //bool result = true;// _membershipProvider.CheckIfExist(login);
            //return result;
        }

        [HttpPost, Route("myDemo5/FromUri")] //呼叫不到
        public Dictionary<string, string> CheckIfUserExist2([FromUri] string va) //[FromBody]
        {
            HttpContent requestContent = Request.Content;
            string jsonContent = requestContent.ReadAsStringAsync().Result;
            System.Collections.Specialized.NameValueCollection a1 = requestContent.ReadAsFormDataAsync().Result;

            Dictionary<string, string> message = new Dictionary<string, string>()
                                {
                                    {"name", va},
                                    {"DateTime", DateTime.Now.ToString()}
                                };
            return message;

            //bool result = true;// _membershipProvider.CheckIfExist(login);
            //return result;
        }

        ////http://blog.wahahajk.com/2009/04/c-byte.html
        ////C# byte 轉 文字
        //byte轉char或 byte轉string
        //Convert.ToChar是把hex轉成相對應ascii code
        //像a的ascii code是0x61
        //byte[] b = new byte[2] { 0x61, 0x62 };
        //string s = Convert.ToChar(b[0]); => s="a";
        //string s = Convert.ToChar(b[1]); => s="b";
        //如果你要把byte code轉成"字面上"的數值 應該這樣寫
        //byte[] b = new byte[2] { 0x61, 0x62 };
        //string s = b[0].ToString("X2"); => s="61";
        //string s = b[1].ToString("X2"); => s="62";
    }
    public class Customer
    {
        public string company_name { get; set; }
        public string contact_name { get; set; }
    }
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
    }
}
