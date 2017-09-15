using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;
using System.Web.Services;
using Dapper;

namespace EMRWEBAPI.Controllers
{
    //注意事項  
    // 1. Controller 需要在 WebApiConfig.cs 註冊或MAP過 
    // 2. Controoler Class name 必須加入 "Controoler "字眼 , exp: EMRAPI001Controller
    // 3. 



    public class EMRAPI001Controller  : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
    
    public class TestJsonController : ApiController
    {

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
        //public string GetEmrRequestByDateJson(string Date1 = "", string Time1 = "", string Date2 = "", string Time2 = "", int emrdbno = 1)
        //{
        //    string rtns = "";
        //    DataTable dt = new DataTable("GetEmrRequestByDate");

        //    if (Date1 == "" || Time1 == null || Date2 == "" || Time2 == null) return null;// "呼叫 Web Services 條件不符合!";		

        //    string sql = " select sRequestNo, sRequestUser, sRequestDate, sRequestTime, sRequestVNum, sRequestServerFileName,  sRequestDOCType, sRequestDocParent,  "
        //               + "  sRequestPatientID, sRequestIDNO, Medicaldate, Medicaltime , sRequesttimestampDate, sRequesttimestampTime ,null as 'sSigningDate',null as 'sSigningTime' "
        //               + " from va_request WITH (NOLOCK ) "
        //               + " where sSystem != 'ALL' and sRequestType = 's'  "
        //               + "  and (sRequestDate  between ? and ? )  "
        //               + "  and (sRequestDate + sRequestTime between ? and ? ) "
        //               + " union all "
        //               + " select a.sRequestNo, sRequestUser, sRequestDate, sRequestTime, sRequestVNum, sRequestServerFileName, a.sRequestDOCType, sRequestDocParent,  "
        //               + "  sRequestPatientID, sRequestIDNO, Medicaldate, Medicaltime , sRequesttimestampDate, sRequesttimestampTime  ,sSigningDate,sSigningTime "
        //               + " from va_request  a  WITH (NOLOCK ) left outer join va_transaction b  WITH (NOLOCK ) on "
        //               + "  a.sRequestNo=b.sRequestNo and a.srequestdate=b.sSignedDate and "
        //               + "  a.srequesttime=b.sSignedTime and a.srequestuser=b.sSignedUserID "
        //               + " where a.sSystem != 'ALL' and a.sRequestType = 's'  and a.sStatus in ('S','Y') "
        //               + "  and  (a.sRequestDate not between ? and ? ) "
        //               + "  and  (b.sSigningDate between ? and ?) "
        //               + "  and  (b.sSigningDate+b.sSigningTime between ? and ?) ";

        //    OleDbConnection conn = new OleDbConnection();
        //    try
        //    {
        //        conn.ConnectionString = getmssqlconnstr(emrdbno);
        //        DataSet dataset = new DataSet();

        //        if (dataset.Tables["GetEmrRequestByDate"] != null)
        //        {
        //            dataset.Tables["GetEmrRequestByDate"].Clear();
        //        }

        //        OleDbCommand cmd = new OleDbCommand(sql, conn);
        //        cmd.CommandType = CommandType.Text;
        //        cmd.CommandTimeout = 600;
        //        //cmd.Parameters.Clear();
        //        cmd.Parameters.Add("@1", OleDbType.VarChar);
        //        cmd.Parameters.Add("@2", OleDbType.VarChar);
        //        cmd.Parameters.Add("@3", OleDbType.VarChar);
        //        cmd.Parameters.Add("@4", OleDbType.VarChar);
        //        cmd.Parameters.Add("@5", OleDbType.VarChar);
        //        cmd.Parameters.Add("@6", OleDbType.VarChar);
        //        cmd.Parameters.Add("@7", OleDbType.VarChar);
        //        cmd.Parameters.Add("@8", OleDbType.VarChar);
        //        cmd.Parameters.Add("@9", OleDbType.VarChar);
        //        cmd.Parameters.Add("@10", OleDbType.VarChar);
        //        cmd.Parameters["@1"].Value = Date1;
        //        cmd.Parameters["@2"].Value = Date2;
        //        cmd.Parameters["@3"].Value = Date1 + Time1;
        //        cmd.Parameters["@4"].Value = Date2 + Time2;
        //        cmd.Parameters["@5"].Value = Date1;
        //        cmd.Parameters["@6"].Value = Date2;
        //        cmd.Parameters["@7"].Value = Date1;
        //        cmd.Parameters["@8"].Value = Date2;
        //        cmd.Parameters["@9"].Value = Date1 + Time1;
        //        cmd.Parameters["@10"].Value = Date2 + Time2;

        //        conn.Open();
        //        OleDbDataAdapter adp = new OleDbDataAdapter(cmd);
        //        adp.Fill(dataset, "GetEmrRequestByDate");
        //        dt = dataset.Tables["GetEmrRequestByDate"];
        //        rtns = DataTableToListJson(dt);// DataTableToList(dt);
        //        conn.Close();
        //    }
        //    catch (Exception e)
        //    {
        //        conn.Close();
        //        dt = null;
        //    }
        //    finally
        //    {
        //    };

        //    return rtns;
        //}


    }

    public class IEMRController : ApiController
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

         
        [HttpGet, HttpPost, ActionName("FUNC")]
        public HttpResponseMessage GetEmrRequestByDateJson(string Date1 = "", string Time1 = "", string Date2 = "", string Time2 = "", int emrdbno = 1)
        {
            string rtns = "";

            string errormessage = "";
            DataTable dt = new DataTable("GetEmrRequestByDate");
            EMRWEBAPI.Models.PublicFunc plib = new Models.PublicFunc();

            if (Date1 == "" || Date1 == null ||
                Time1 == "" || Time1 == null ||
                Date2 == "" || Date2 == null ||
                Time2 == "" || Time2 == null )
            {
                rtns= "呼叫 Web API 條件不符合!";	                 
            }
            else
            {
                #region
                string sql = " select sRequestNo, sRequestUser, sRequestDate, sRequestTime, sRequestVNum, sRequestServerFileName,  sRequestDOCType, sRequestDocParent,  "
                       + "  sRequestPatientID, sRequestIDNO, Medicaldate, Medicaltime , sRequesttimestampDate, sRequesttimestampTime ,null as 'sSigningDate',null as 'sSigningTime' "
                       + " from va_request WITH (NOLOCK ) "
                       + " where sSystem != 'ALL' and sRequestType = 's'  "
                       + "  and (sRequestDate  between ? and ? )  "
                       + "  and (sRequestDate + sRequestTime between ? and ? ) "
                       + " union all "
                       + " select a.sRequestNo, sRequestUser, sRequestDate, sRequestTime, sRequestVNum, sRequestServerFileName, a.sRequestDOCType, sRequestDocParent,  "
                       + "  sRequestPatientID, sRequestIDNO, Medicaldate, Medicaltime , sRequesttimestampDate, sRequesttimestampTime  ,sSigningDate,sSigningTime "
                       + " from va_request  a  WITH (NOLOCK ) left outer join va_transaction b  WITH (NOLOCK ) on "
                       + "  a.sRequestNo=b.sRequestNo and a.srequestdate=b.sSignedDate and "
                       + "  a.srequesttime=b.sSignedTime and a.srequestuser=b.sSignedUserID "
                       + " where a.sSystem != 'ALL' and a.sRequestType = 's'  and a.sStatus in ('S','Y') "
                       + "  and  (a.sRequestDate not between ? and ? ) "
                       + "  and  (b.sSigningDate between ? and ?) "
                       + "  and  (b.sSigningDate+b.sSigningTime between ? and ?) ";

                OleDbConnection conn = new OleDbConnection();
                try
                {

                    errormessage = "";
                    plib.ErrorLog("Get ConnectionString");
                    conn.ConnectionString = plib.getmssqlconnstr(emrdbno, out errormessage);
                    //"Provider=SQLOLEDB.1;Data Source=192.168.19.88;Initial Catalog=emrdb;UID=emr;PWD=emr2010~;";
                    // 
                    // 
                    plib.ErrorLog("finish errormessage:" + errormessage);
                    DataSet dataset = new DataSet();

                    if (dataset.Tables["GetEmrRequestByDate"] != null)
                    {
                        dataset.Tables["GetEmrRequestByDate"].Clear();
                    }

                    OleDbCommand cmd = new OleDbCommand(sql, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = 600;
                    //cmd.Parameters.Clear();
                    cmd.Parameters.Add("@1", OleDbType.VarChar);
                    cmd.Parameters.Add("@2", OleDbType.VarChar);
                    cmd.Parameters.Add("@3", OleDbType.VarChar);
                    cmd.Parameters.Add("@4", OleDbType.VarChar);
                    cmd.Parameters.Add("@5", OleDbType.VarChar);
                    cmd.Parameters.Add("@6", OleDbType.VarChar);
                    cmd.Parameters.Add("@7", OleDbType.VarChar);
                    cmd.Parameters.Add("@8", OleDbType.VarChar);
                    cmd.Parameters.Add("@9", OleDbType.VarChar);
                    cmd.Parameters.Add("@10", OleDbType.VarChar);
                    cmd.Parameters["@1"].Value = Date1;
                    cmd.Parameters["@2"].Value = Date2;
                    cmd.Parameters["@3"].Value = Date1 + Time1;
                    cmd.Parameters["@4"].Value = Date2 + Time2;
                    cmd.Parameters["@5"].Value = Date1;
                    cmd.Parameters["@6"].Value = Date2;
                    cmd.Parameters["@7"].Value = Date1;
                    cmd.Parameters["@8"].Value = Date2;
                    cmd.Parameters["@9"].Value = Date1 + Time1;
                    cmd.Parameters["@10"].Value = Date2 + Time2;

                    conn.Open();
                    OleDbDataAdapter adp = new OleDbDataAdapter(cmd);
                    adp.Fill(dataset, "GetEmrRequestByDate");
                    dt = dataset.Tables["GetEmrRequestByDate"];
                    rtns = DataTableToListJson(dt);// DataTableToList(dt);
                    conn.Close();
                }
                catch (Exception e)
                {
                    conn.Close();
                    dt = null;
                    rtns = errormessage + e.Message;
                    plib.ErrorLog("errormessage:" + errormessage + e.Message);
                }
                finally
                {
                };
                #endregion
            }
                        
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StringContent(rtns);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return result;
        }

        //[HttpGet, HttpPost, ActionName("FUNC"), Route("Emr/{id}")]
        [HttpGet, HttpPost, ActionName("FUNC"), Route("EMR/{Doctype}/{emrdbno}")]
        public HttpResponseMessage Get_reference_xlst(string Doctype= "", int emrdbno = 1)
        {
            string rtns = "";

            string errormessage = "";
            DataTable dt = new DataTable("Get_reference_xlst");
            EMRWEBAPI.Models.PublicFunc plib = new Models.PublicFunc();

            if (Doctype == ""  || Doctype == null)
            {
                rtns = "呼叫 Web API 條件不符合!";
            }
            else
            {
                #region
                string sql = " select a.sCode,a.sContent,b.vs from va_reference_xlst a right join "
                           + " (select sCode, max(Version) vs from va_reference_xlst where EFG = '1' and STATUS = '1' group by  sCode) b " 
                           + " on a.sCode = b.sCode and a.Version = b.vs "
                           + " where a.sCode = ? ";

                           //" select a.sSystem,a.sRequestDOCType,c.sContent,c.vs as version ,count(1) ct  "
                           //+ " from va_request a left "
                           //+ " join "
                           //+ " ( "
                           //+ "     select a.sCode,a.sContent,b.vs from va_reference_xlst a left join "
                           //+ "     (select sCode, max(Version)vs from va_reference_xlst where EFG = '1' and STATUS = '1' group by  sCode ) b "
                           //        + " on a.sCode = b.sCode and a.Version = b.vs "
                           //+ " ) c on a.sRequestDOCType = c.sCode "
                           //+ " where  a.sRequestDOCType = ? "
                           //+ " group by a.sSystem,a.sRequestDOCType,c.sContent,c.vs "
                           //+ " order by sSystem, sRequestDOCType ";

                OleDbConnection conn = new OleDbConnection();
                try
                {

                    errormessage = "";
                    plib.ErrorLog("Get ConnectionString");
                    conn.ConnectionString = plib.getmssqlconnstr(emrdbno, out errormessage);
                    plib.ErrorLog("finish errormessage:" + errormessage);
                    DataSet dataset = new DataSet();

                    if (dataset.Tables["Get_reference_xlst"] != null)
                    {
                        dataset.Tables["Get_reference_xlst"].Clear();
                    }

                    OleDbCommand cmd = new OleDbCommand(sql, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = 600;
                    //cmd.Parameters.Clear();
                    cmd.Parameters.Add("@1", OleDbType.VarChar);
                    cmd.Parameters["@1"].Value = Doctype;

                    conn.Open();
                    OleDbDataAdapter adp = new OleDbDataAdapter(cmd);
                    adp.Fill(dataset, "Get_reference_xlst");
                    dt = dataset.Tables["Get_reference_xlst"];
                    rtns = JsonConvert.SerializeObject(dt, Formatting.Indented);
                    conn.Close();
                }
                catch (Exception e)
                {
                    conn.Close();
                    dt = null;
                    rtns = errormessage + e.Message;
                    plib.ErrorLog("errormessage:" + errormessage + e.Message);
                }
                finally
                {
                };
                #endregion
            }

            var result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StringContent(rtns);
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

        public string DataTableToListJson(DataTable dtInput)
        {
            string rtns = "";
            List<object> objectList = new List<object>();

            foreach (DataRow dr in dtInput.Rows)
            {
                IEmrObj newObj = new IEmrObj();
                newObj.sRequestNo = dr["sRequestNo"].ToString();
                newObj.sRequestUser = dr["sRequestUser"].ToString();
                newObj.sRequestDate = dr["sRequestDate"].ToString();
                newObj.sRequestTime = dr["sRequestTime"].ToString();
                newObj.sRequestVNum = dr["sRequestVNum"].ToString();
                newObj.sRequestServerFileName = dr["sRequestServerFileName"].ToString();
                newObj.sRequestDOCType = dr["sRequestDOCType"].ToString();
                newObj.sRequestDocParent = dr["sRequestDocParent"].ToString();
                newObj.sRequestPatientID = dr["sRequestPatientID"].ToString();
                newObj.sRequestIDNO = dr["sRequestIDNO"].ToString();
                newObj.Medicaldate = dr["Medicaldate"].ToString();
                newObj.Medicaltime = dr["Medicaltime"].ToString();
                newObj.sRequesttimestampDate = dr["sRequesttimestampDate"].ToString();
                newObj.sRequesttimestampTime = dr["sRequesttimestampTime"].ToString();
                objectList.Add(newObj);
            }
            rtns = JsonConvert.SerializeObject(objectList);
            return rtns;
        }

        public class IEmrObj
        {
            private string _sRequestNo = "";
            private string _sRequestUser = "";
            private string _sRequestDate = "";
            private string _sRequestTime = "";
            private string _sRequestVNum = "";
            private string _sRequestServerFileName = "";
            private string _sRequestDOCType = "";
            private string _sRequestDocParent = "";
            private string _sRequestPatientID = "";
            private string _sRequestIDNO = "";
            private string _Medicaldate = "";
            private string _Medicaltime = "";
            private string _sRequesttimestampDate = "";
            private string _sRequesttimestampTime = "";

            public void clear()
            {
                _sRequestNo = "";
                _sRequestUser = "";
                _sRequestDate = "";
                _sRequestTime = "";
                _sRequestVNum = "";
                _sRequestServerFileName = "";
                _sRequestDOCType = "";
                _sRequestDocParent = "";
                _sRequestPatientID = "";
                _sRequestIDNO = "";
                _Medicaldate = "";
                _Medicaltime = "";
                _sRequesttimestampDate = "";
                _sRequesttimestampTime = "";

            }

            public IEmrObj() { }
            ~IEmrObj() { }

            public string sRequestNo
            {
                get
                {
                    return _sRequestNo;
                }
                set
                {
                    _sRequestNo = value;
                }
            }

            public string sRequestUser
            {
                get
                {
                    return _sRequestUser;
                }
                set
                {
                    _sRequestUser = value;
                }
            }

            public string sRequestDate
            {
                get
                {
                    return _sRequestDate;
                }
                set
                {
                    _sRequestDate = value;
                }
            }

            public string sRequestTime
            {
                get
                {
                    return _sRequestTime;
                }
                set
                {
                    _sRequestTime = value;
                }
            }

            public string sRequestVNum
            {
                get
                {
                    return _sRequestVNum;
                }
                set
                {
                    _sRequestVNum = value;
                }
            }

            public string sRequestServerFileName
            {
                get
                {
                    return _sRequestServerFileName;
                }
                set
                {
                    _sRequestServerFileName = value;
                }
            }

            public string sRequestDOCType
            {
                get
                {
                    return _sRequestDOCType;
                }
                set
                {
                    _sRequestDOCType = value;
                }
            }

            public string sRequestDocParent
            {
                get
                {
                    return _sRequestDocParent;
                }
                set
                {
                    _sRequestDocParent = value;
                }
            }

            public string sRequestPatientID
            {
                get
                {
                    return _sRequestPatientID;
                }
                set
                {
                    _sRequestPatientID = value;
                }
            }

            public string sRequestIDNO
            {
                get
                {
                    return _sRequestIDNO;
                }
                set
                {
                    _sRequestIDNO = value;
                }
            }

            public string Medicaldate
            {
                get
                {
                    return _Medicaldate;
                }
                set
                {
                    _Medicaldate = value;
                }
            }

            public string Medicaltime
            {
                get
                {
                    return _Medicaltime;
                }
                set
                {
                    _Medicaltime = value;
                }
            }

            public string sRequesttimestampDate
            {
                get
                {
                    return _sRequesttimestampDate;
                }
                set
                {
                    _sRequesttimestampDate = value;
                }
            }

            public string sRequesttimestampTime
            {
                get
                {
                    return _sRequesttimestampTime;
                }
                set
                {
                    _sRequesttimestampTime = value;
                }
            }
        }



    }

    [RoutePrefix("VVEMR")]
    public class VAEMRController : ApiController
    { 
        [HttpGet, HttpPost, Route("apie/LIST_3")] //http://localhost:58697/VVEMR/apie/LIST_3
        public HttpResponseMessage Get_LIST3()
        { 
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StringContent(function1("3", 88));
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return result;
        }

        [HttpGet, HttpPost, Route("apie/LIST_4_10_3_3", Order = 1)] //http://localhost:58697/VVEMR/apie/LIST_4_10_3_3
        public HttpResponseMessage Get_LIST_4_10_3_3()
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StringContent(function1("4-10-3-3", 88));
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return result;
        }


        [HttpGet, HttpPost, Route("api/{Doctype}/{emrdbno:int=1}")] //http://localhost:58697/VVEMR/api/4-2-1/88
        public HttpResponseMessage Get_reference_xlst(string Doctype = "", int emrdbno = 1)
        {
            string rtns = "";

            string errormessage = "";
            DataTable dt = new DataTable("Get_reference_xlst");
            EMRWEBAPI.Models.PublicFunc plib = new Models.PublicFunc();

            if (Doctype == "" || Doctype == null)
            {
                rtns = "呼叫 Web API 條件不符合!";
            }
            else
            {
                #region
                string sql = " select a.sCode,a.sContent,b.vs from va_reference_xlst a right join "
                           + " (select sCode, max(Version) vs from va_reference_xlst where EFG = '1' and STATUS = '1' group by  sCode) b "
                           + " on a.sCode = b.sCode and a.Version = b.vs "
                           + " where a.sCode = ? "; 

                OleDbConnection conn = new OleDbConnection();
                try
                {

                    errormessage = "";
                    plib.ErrorLog("Get ConnectionString");
                    conn.ConnectionString = plib.getmssqlconnstr(emrdbno, out errormessage);
                    plib.ErrorLog("finish errormessage:" + errormessage);
                    DataSet dataset = new DataSet();

                    if (dataset.Tables["Get_reference_xlst"] != null)
                    {
                        dataset.Tables["Get_reference_xlst"].Clear();
                    }

                    OleDbCommand cmd = new OleDbCommand(sql, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = 600;
                    //cmd.Parameters.Clear();
                    cmd.Parameters.Add("@1", OleDbType.VarChar);
                    cmd.Parameters["@1"].Value = Doctype;

                    conn.Open();
                    OleDbDataAdapter adp = new OleDbDataAdapter(cmd);
                    adp.Fill(dataset, "Get_reference_xlst");
                    dt = dataset.Tables["Get_reference_xlst"];
                    rtns = JsonConvert.SerializeObject(dt, Formatting.Indented);
                    conn.Close();
                }
                catch (Exception e)
                {
                    conn.Close();
                    dt = null;
                    rtns = errormessage + e.Message;
                    plib.ErrorLog("errormessage:" + errormessage + e.Message);
                }
                finally
                {
                };
                #endregion
            }

            var result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StringContent(rtns);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return result;
        }

        #region 測試 dapper ORM
        // http://blog.darkthread.net/post-2014-05-15-dapper.aspx
        // https://dotblogs.com.tw/yc421206/2015/03/11/150703

        public class ReferenceXlst
        {
            public string sCode { get; set; }
            public string sContent { get; set; }
            public string vs { get; set; }
        }

        [HttpGet, HttpPost, Route("api/Query/{Doctype}/{emrdbno:int=1}")] // http://localhost:58698/VVEMR/api/Query/4-13-11-3/1
        public HttpResponseMessage Get_reference_Query(string Doctype = "", int emrdbno = 1)
        {
            string rtns = ""; 
            
            string errormessage = "";
            //DataTable dt = new DataTable("Get_reference_xlst");
            EMRWEBAPI.Models.PublicFunc plib = new Models.PublicFunc();

            if (Doctype == "" || Doctype == null)
            {
                rtns = "呼叫 Web API 條件不符合!";
            }
            else
            {

                string sql = " select a.sCode,a.sContent,b.vs from va_reference_xlst a right join "
            + " (select sCode, max(Version) vs from va_reference_xlst where EFG = '1' and STATUS = '1' group by  sCode) b "
            + " on a.sCode = b.sCode and a.Version = b.vs "
            + " where a.sCode=?  and b.vs=? ";

                try
                {
                    using (var cn = new OleDbConnection(plib.getmssqlconnstr(emrdbno, out errormessage)))
                    {
                        //1) 將SELECT結果轉成指定的型別(屬性與欄位名稱要一致)
                        //2) 直接傳數字陣列作為WHERE IN比對參數
                        //   =>自動轉成WHERE col in (@arg1,@arg2,@arg3)
                        var list = cn.Query<ReferenceXlst>(
                            sql,
                                //new
                                //{
                                //    P1 = Doctype 
                                //}
                                new { P1 = Doctype,P2="1" }
                            );


                        foreach (var item in list)
                        {
                            Console.WriteLine("{0}.{1}.{2}",
                                item.sCode, item.sContent, item.vs);
                        }
                        rtns = JsonConvert.SerializeObject(list, Formatting.Indented);
                    }
                }
                catch (Exception e)
                {
                    rtns = errormessage + e.Message;
                    plib.ErrorLog("errormessage:" + errormessage + e.Message);
                }
                finally
                {
                };
                
                #region
                
                #endregion
            }

            var result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StringContent(rtns);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return result;
        }
        
        [HttpGet, HttpPost, Route("api/Update/{Doctype}/{Effdate}/{emrdbno:int=1}")] // http://localhost:58698/VVEMR/api/Update/4-13-9-6/20170401/1
        public HttpResponseMessage Get_referenceUpdate(string Doctype = "", string Effdate = "", int emrdbno = 1)
        {
            string rtns = "";

            string errormessage = ""; 
            EMRWEBAPI.Models.PublicFunc plib = new Models.PublicFunc();

            if (Doctype == "" || Doctype == null || Effdate == "" || Effdate == null)
            {
                rtns = "呼叫 Web API 條件不符合!";
            }
            else
            {

                string sql = " update va_reference_xlst set Effdate =?,RTT= getdate()  where scode =? ";

                try
                {
                    using (var cn = new OleDbConnection(plib.getmssqlconnstr(emrdbno, out errormessage)))
                    {                        
                        var list = cn.Execute(
                            sql,                                 
                                new { P1 = Effdate, P2 = Doctype }
                            );


                        //foreach (var item in list)
                        //{
                        //    Console.WriteLine("{0}.{1}.{2}",
                        //        item.sCode, item.sContent, item.vs);
                        //}
                        rtns = JsonConvert.SerializeObject(list, Formatting.Indented);
                    }
                }
                catch (Exception e)
                {
                    rtns = errormessage + e.Message;
                    plib.ErrorLog("errormessage:" + errormessage + e.Message);
                }
                finally
                {
                };

                #region

                #endregion
            }

            var result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StringContent(rtns);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return result;
        }
        
        [HttpGet, HttpPost, Route("api/insert/{Doctype}/{emrdbno:int=1}")] // http://localhost:58698/VVEMR/api/Update/4-13-9-6/20170401/1
        public HttpResponseMessage Get_referenceinsert(string Doctype = "", int emrdbno = 1)
        {
            string rtns = "";

            string errormessage = "";
            EMRWEBAPI.Models.PublicFunc plib = new Models.PublicFunc();

            if (Doctype == "" || Doctype == null  )
            {
                rtns = "呼叫 Web API 條件不符合!";
            }
            else
            {

                string sql = " INSERT INTO va_reference ( sSystem,sType,sCode,sContent,sRemark )   VALUES (?,?,?, '鎮靜劑照護記錄單' ,null) ";

                try
                {
                    using (var cn = new OleDbConnection(plib.getmssqlconnstr(emrdbno, out errormessage)))
                    {
                        //1) 可執行SQL資料更新指令，支援參數
                        //2) 以陣列方式提供多組參數，可重複執行同一SQL指
                        //3 會回船處理筆數
                        var list = cn.Execute(
                            sql,
                              new[] {
                                        new { P1 = "EMR1", P2 = "Doctype" , P3 = "4-13-9-6-1" },
                                        new { P1 = "EMR1", P2 = "Doctype" , P3 = "4-13-9-6-2" },
                                        new { P1 = "EMR1", P2 = "Doctype" , P3 = "4-13-9-6-3" }
                                    }
                            );
                                                
                        rtns = JsonConvert.SerializeObject(list, Formatting.Indented);
                    }
                }
                catch (Exception e)
                {
                    rtns = errormessage + e.Message;
                    plib.ErrorLog("errormessage:" + errormessage + e.Message);
                }
                finally
                {
                };

                #region

                #endregion
            }

            var result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StringContent(rtns);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return result;
        }

        [HttpGet, HttpPost, Route("api/QueryMultipl/{Doctype}/{emrdbno:int=1}")] // http://localhost:58698/VVEMR/api//QueryMultipl/4-13-9-6/1
        public HttpResponseMessage Get_reference_QueryMultiple(string Doctype = "", int emrdbno = 1)
        {
            string rtns = "";

            string errormessage = ""; 
            EMRWEBAPI.Models.PublicFunc plib = new Models.PublicFunc();

            if (Doctype == "" || Doctype == null)
            {
                rtns = "呼叫 Web API 條件不符合!";
            }
            else
            {
                //Dapper還可以在命令中一次包含多組SELECT，透過QueryMultiple()後再以Read()或Read<T>分別取出查詢結果。
                string sql = @" select * from va_reference      where sSystem = 'EMR' and sType = 'DocType' and sCode = ? "
                           + "  select * from va_reference_xlst where sSystem = 'EMR' and sType = 'DocType' and scode = ?  ";

                try
                {
                    using (var cn = new OleDbConnection(plib.getmssqlconnstr(emrdbno, out errormessage)))
                    {
                        //1) 將SELECT結果轉成指定的型別(屬性與欄位名稱要一致)
                        //2) 直接傳數字陣列作為WHERE IN比對參數
                        //   =>自動轉成WHERE col in (@arg1,@arg2,@arg3)
                        var multi = cn.QueryMultiple(
                            sql, 
                                new { P1 = Doctype, P2 = Doctype }
                            );

                        var cust = multi.Read<ReferenceXlst>().First();
                        Console.WriteLine("{0} / {1} / {2}", cust.sCode, cust.sContent, cust.vs);

                        var ords = multi.Read(); //取回IEnumerable<dynamic>
                        Console.WriteLine("Orders Count = {0}", ords.Count());

                        foreach (var item in ords)
                        {
                            Console.WriteLine("{0}.{1}",
                                item.sCode, item.sContent);
                        }
                        rtns = JsonConvert.SerializeObject(ords, Formatting.Indented);
                    }
                }
                catch (Exception e)
                {
                    rtns = errormessage + e.Message;
                    plib.ErrorLog("errormessage:" + errormessage + e.Message);
                }
                finally
                {
                };

                #region

                #endregion
            }

            var result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StringContent(rtns);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return result;
        }

        [HttpGet, HttpPost, Route("api/StoredProcedure/{stDate}/{edDate}/{sRequestPatientID}/{emrdbno:int=1}")] // http://localhost:58698/VVEMR/api/StoredProcedure/20170501/20170601/24674226/1
        public HttpResponseMessage Get_reference_StoredProcedure(string stDate = "", string edDate = "", string sRequestPatientID = "", int emrdbno = 1)
        {
            string rtns = "";

            string errormessage = "";
            EMRWEBAPI.Models.PublicFunc plib = new Models.PublicFunc();

            if (stDate == "" || stDate == null ||
                edDate == "" || edDate == null ||
                sRequestPatientID == "" || sRequestPatientID == null
                )
            {
                rtns = "呼叫 Web API 條件不符合!";
            }
            else
            {
                //Dapper還可以在命令中一次包含多組SELECT，透過QueryMultiple()後再以Read()或Read<T>分別取出查詢結果。
                string sql = @"[dbo].[GetEmrPatientListMax] ";

                try
                {
                    using (var cn = new OleDbConnection(plib.getmssqlconnstr(emrdbno, out errormessage)))
                    { 
                        var list = cn.Query(
                            sql,
                                new { P1 = stDate, P2 = edDate, P3 = sRequestPatientID, P4 = "", P5 = "", P6 = "", P7 = "", P8 = "" },
                             commandType: CommandType.StoredProcedure
                            );
                         
                        foreach (var item in list)
                        {
                            Console.WriteLine("{0}.{1}",
                                item.sCode, item.sContent);
                        }
                        rtns = JsonConvert.SerializeObject(list, Formatting.Indented);
                    }
                }
                catch (Exception e)
                {
                    rtns = errormessage + e.Message;
                    plib.ErrorLog("errormessage:" + errormessage + e.Message);
                }
                finally
                {
                };
            }

            var result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StringContent(rtns);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return result;
        }

        [HttpGet, HttpPost, Route("api/StoredProcedure2/{stDate}/{edDate}/{sRequestPatientID}/{emrdbno:int=1}")] // http://localhost:58698/VVEMR/api/StoredProcedure2/20170501/20170601/24674226/1
        public HttpResponseMessage Get_reference_StoredProcedure2(string stDate = "", string edDate = "", string sRequestPatientID = "", int emrdbno = 1)
        {
            string rtns = "";

            string errormessage = "";
            EMRWEBAPI.Models.PublicFunc plib = new Models.PublicFunc();

            if (stDate == "" || stDate == null ||
                edDate == "" || edDate == null ||
                sRequestPatientID == "" || sRequestPatientID == null
                )
            {
                rtns = "呼叫 Web API 條件不符合!";
            }
            else
            {
                //Dapper還可以在命令中一次包含多組SELECT，透過QueryMultiple()後再以Read()或Read<T>分別取出查詢結果。
                string sql = @"[dbo].[GetEmrPatientListMax] ";

                try
                {
                    using (var cn = new OleDbConnection(plib.getmssqlconnstr(emrdbno, out errormessage)))
                    {
                        var dynamicParams = new DynamicParameters();//←動態參數
                        dynamicParams.Add("p1", stDate, dbType: DbType.String, direction: ParameterDirection.Input);
                        dynamicParams.Add("p2", edDate, dbType: DbType.String, direction: ParameterDirection.Input);
                        dynamicParams.Add("p3", sRequestPatientID, dbType: DbType.String, direction: ParameterDirection.Input);
                        dynamicParams.Add("p4", "");
                        dynamicParams.Add("p5", "");
                        dynamicParams.Add("p6", "");
                        dynamicParams.Add("p7", "");
                        dynamicParams.Add("p8", "");

                        var list = cn.Query(
                            sql,
                             dynamicParams,
                             commandType: CommandType.StoredProcedure
                            );

                        foreach (var item in list)
                        {
                            Console.WriteLine("{0}.{1}",
                                item.sCode, item.sContent);
                        }
                        rtns = JsonConvert.SerializeObject(list, Formatting.Indented);
                    }
                }
                catch (Exception e)
                {
                    rtns = errormessage + e.Message;
                    plib.ErrorLog("errormessage:" + errormessage + e.Message);
                }
                finally
                {
                };
            }

            var result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StringContent(rtns);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return result;
        }
        #endregion

        /// <summary>
        ///測試 直接呼叫 method
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, HttpPost, Route("func/MethodFruit/{id}", Name = "GetBookById")]  //http://localhost:58697/VVEMR/func/MethodFruit/GetBookById?id=1
        public string[] MethodFruit(string id)
        {
            if (id == "1")
                return new string[] { "Apple", "Orange", "Banana", "GREEN" }; //http://localhost:58697/VVEMR/func/MethodFruit/1
            else
                return new string[] { "Apple", "Orange", "Banana" }; //http://localhost:58697/VVEMR/func/MethodFruit/2
        }

        [HttpGet, HttpPost, Route("func/MethodFruit")]
        public string[] MethodFruit()
        {
            return new string[] { "Apple", "Orange", "Banana" };  //http://localhost:58697/VVEMR/func/MethodFruit
        }

        public string function1(string Doctype = "", int emrdbno = 1)
        {
            string rtns = "";

            string errormessage = "";
            DataTable dt = new DataTable("Get_reference_xlst");
            EMRWEBAPI.Models.PublicFunc plib = new Models.PublicFunc();

            if (Doctype == "" || Doctype == null)
            {
                rtns = "呼叫 Web API 條件不符合!";
            }
            else
            {
                #region
                string sql = " select a.sCode,a.sContent,b.vs from va_reference_xlst a right join "
                           + " (select sCode, max(Version) vs from va_reference_xlst where EFG = '1' and STATUS = '1' group by  sCode) b "
                           + " on a.sCode = b.sCode and a.Version = b.vs "
                           + " where a.sCode = ? ";

                OleDbConnection conn = new OleDbConnection();
                try
                {

                    errormessage = "";
                    plib.ErrorLog("Get ConnectionString");
                    conn.ConnectionString = plib.getmssqlconnstr(emrdbno, out errormessage);
                    plib.ErrorLog("finish errormessage:" + errormessage);
                    DataSet dataset = new DataSet();

                    if (dataset.Tables["Get_reference_xlst"] != null)
                    {
                        dataset.Tables["Get_reference_xlst"].Clear();
                    }

                    OleDbCommand cmd = new OleDbCommand(sql, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = 600;
                    //cmd.Parameters.Clear();
                    cmd.Parameters.Add("@1", OleDbType.VarChar);
                    cmd.Parameters["@1"].Value = Doctype;

                    conn.Open();
                    OleDbDataAdapter adp = new OleDbDataAdapter(cmd);
                    adp.Fill(dataset, "Get_reference_xlst");
                    dt = dataset.Tables["Get_reference_xlst"];
                    rtns = JsonConvert.SerializeObject(dt, Formatting.Indented);
                    conn.Close();
                }
                catch (Exception e)
                {
                    conn.Close();
                    dt = null;
                    rtns = errormessage + e.Message;
                    plib.ErrorLog("errormessage:" + errormessage + e.Message);
                }
                finally
                {
                };
                #endregion
            }
        return rtns;
        }

    }

}