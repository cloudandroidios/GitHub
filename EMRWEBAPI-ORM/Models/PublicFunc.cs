using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace EMRWEBAPI.Models
{
    public class PublicFunc
    {
        public static string Global_EFFDATE = "";      //重新啟動取得連結字串 by 天
        public static Global_connstrObj Gbo = new Global_connstrObj();  //存放各連結字串

        public PublicFunc()
        {
            //如果使用設計的元件，請取消註解下行程式碼 
            //InitializeComponent(); 
#if DEBUG
            /* 
            
            Configuration rootWebConfig = WebConfigurationManager.OpenWebConfiguration("~/");
            for (int i = 0; i < rootWebConfig.ConnectionStrings.ConnectionStrings.Count; i++)
            {
                _connstrings.Add(rootWebConfig.ConnectionStrings.ConnectionStrings[i].Name,
                    rootWebConfig.ConnectionStrings.ConnectionStrings[i].ConnectionString);
            }
            _connstrings.Add("OPD8", return_connstr_OPD8);
            _connstrings.Add("IPD8", return_connstr_IPD8);
            _connstrings.Add("LAB8", return_connstr_LAB8);
            _connstrings.Add("UCTL", return_connstr_UCTL);
            _connstrings.Add("PSON", return_connstr_PSON);

            */

            _connstrings = new System.Collections.Generic.Dictionary<string, string>();
            _connstrings.Add("OPD8", "User Id=hot;Data Source=HDB.WORLD;Password=hot;Persist Security Info=true;Max Pool Size=15;Min Pool Size=5;Incr Pool Size=1;");
            _connstrings.Add("IPD8", "User Id=hot;Data Source=SDB.WORLD;Password=hot;Persist Security Info=true;Max Pool Size=15;Min Pool Size=5;Incr Pool Size=1;");
            _connstrings.Add("LAB8", "User Id=hot;Data Source=LBEXAM.WORLD;Password=hot;Persist Security Info=true;Max Pool Size=15;Min Pool Size=5;Incr Pool Size=1;");
            _connstrings.Add("UCTL", "User Id=hot;Data Source=UCTL.WORLD;Password=hot;Persist Security Info=true;Max Pool Size=15;Min Pool Size=5;Incr Pool Size=1;");
            _connstrings.Add("PSON", "User Id=hot;Data Source=PSON.WORLD;Password=hot;Persist Security Info=true;Max Pool Size=15;Min Pool Size=5;Incr Pool Size=1;");
            _connstrings.Add("BATCH", "User Id=hot;Data Source=BATCH.WORLD;Password=hot;Persist Security Info=true;Max Pool Size=15;Min Pool Size=5;Incr Pool Size=1;");

#else
            
            string return_connstr_OPD8 = WebConfigurationManager.ConnectionStrings["OPD8"].ConnectionString;
            string return_connstr_IPD8 = WebConfigurationManager.ConnectionStrings["IPD8"].ConnectionString;
            string return_connstr_LAB8 = WebConfigurationManager.ConnectionStrings["LAB8"].ConnectionString;
            string return_connstr_UCTL = WebConfigurationManager.ConnectionStrings["UCTL"].ConnectionString;
            string return_connstr_PSON = WebConfigurationManager.ConnectionStrings["PSON"].ConnectionString;
            string return_connstr_BATCH = WebConfigurationManager.ConnectionStrings["BATCH"].ConnectionString;

            _connstrings = new System.Collections.Generic.Dictionary<string,string>();
            
            _connstrings.Add("OPD8", return_connstr_OPD8);
            _connstrings.Add("IPD8", return_connstr_IPD8);
            _connstrings.Add("LAB8", return_connstr_LAB8);
            _connstrings.Add("UCTL", return_connstr_UCTL);
            _connstrings.Add("PSON", return_connstr_PSON);
            _connstrings.Add("BATCH", return_connstr_BATCH); 
#endif
        }

        private System.Collections.Generic.Dictionary<string, string> _connstrings;
        
        public class Global_connstrObj
        {
            private string _Global_ConnStr0 = "";
            private string _Global_ConnStr1 = "";
            private string _Global_ConnStr88 = "";
            private string _Global_ConnStr2010 = "";
            private string _Global_ConnStr2011 = "";
            private string _Global_ConnStr2012 = "";
            private string _Global_ConnStr2013 = "";
            private string _Global_ConnStr2014 = "";
            private string _Global_ConnStr2015 = "";
            private string _Global_ConnStr2016 = "";
            private string _Global_ConnStr2017 = "";
            private string _Global_ConnStr2018 = "";

            public void clear()
            {
                _Global_ConnStr0 = "";
                _Global_ConnStr1 = "";
                _Global_ConnStr88 = "";
                _Global_ConnStr2010 = "";
                _Global_ConnStr2011 = "";
                _Global_ConnStr2012 = "";
                _Global_ConnStr2013 = "";
                _Global_ConnStr2014 = "";
                _Global_ConnStr2015 = "";
                _Global_ConnStr2016 = "";
                _Global_ConnStr2017 = "";
                _Global_ConnStr2018 = "";
            }

            /// <summary>
            /// 取得全域性連結字串
            /// </summary>
            /// <param name="EmrDbNo">資料庫編號</param>
            /// <returns></returns>
            public string GetGlobalNum(int EmrDbNo)
            {
                string rtns = "";

                switch (EmrDbNo)
                {
                    case 0:
                        rtns = _Global_ConnStr0;
                        break;
                    case 1:
                        rtns = _Global_ConnStr1;
                        break;
                    case 88:
                        rtns = _Global_ConnStr88;
                        break;
                    case 2010:
                        rtns = _Global_ConnStr2010;
                        break;
                    case 2011:
                        rtns = _Global_ConnStr2011;
                        break;
                    case 2012:
                        rtns = _Global_ConnStr2012;
                        break;
                    case 2013:
                        rtns = _Global_ConnStr2013;
                        break;
                    case 2014:
                        rtns = _Global_ConnStr2014;
                        break;
                    case 2015:
                        rtns = _Global_ConnStr2015;
                        break;
                    case 2016:
                        rtns = _Global_ConnStr2016;
                        break;
                    case 2017:
                        rtns = _Global_ConnStr2017;
                        break;
                    case 2018:
                        rtns = _Global_ConnStr2018;
                        break;
                    default:
                        break;
                }
                return rtns;
            }

            /// <summary>
            /// 設定全域性連結字串
            /// </summary>
            /// <param name="EmrDbNo">資料庫編號</param>
            /// <param name="inputstr">連結字串</param>
            public void SetGlobalNum(int EmrDbNo, string inputstr)
            {
                switch (EmrDbNo)
                {
                    case 0:
                        _Global_ConnStr0 = inputstr;
                        break;
                    case 1:
                        _Global_ConnStr1 = inputstr;
                        break;
                    case 88:
                        _Global_ConnStr88 = inputstr;
                        break;
                    case 2010:
                        _Global_ConnStr2010 = inputstr;
                        break;
                    case 2011:
                        _Global_ConnStr2011 = inputstr;
                        break;
                    case 2012:
                        _Global_ConnStr2012 = inputstr;
                        break;
                    case 2013:
                        _Global_ConnStr2013 = inputstr;
                        break;
                    case 2014:
                        _Global_ConnStr2014 = inputstr;
                        break;
                    case 2015:
                        _Global_ConnStr2015 = inputstr;
                        break;
                    case 2016:
                        _Global_ConnStr2016 = inputstr;
                        break;
                    case 2017:
                        _Global_ConnStr2017 = inputstr;
                        break;
                    case 2018:
                        _Global_ConnStr2018 = inputstr;
                        break;
                    default:
                        break;
                }
            }

            public Global_connstrObj()
            {
                clear();
            }

            ~Global_connstrObj() { }

            #region 變數陣列

            public string Global_ConnStr0
            {
                get
                {
                    return _Global_ConnStr0;
                }
                set
                {
                    _Global_ConnStr0 = value;
                }
            }

            public string Global_ConnStr1
            {
                get
                {
                    return _Global_ConnStr1;
                }
                set
                {
                    _Global_ConnStr1 = value;
                }
            }

            public string Global_ConnStr88
            {
                get
                {
                    return _Global_ConnStr88;
                }
                set
                {
                    _Global_ConnStr88 = value;
                }
            }

            public string Global_ConnStr2010
            {
                get
                {
                    return _Global_ConnStr2010;
                }
                set
                {
                    _Global_ConnStr2010 = value;
                }
            }

            public string Global_ConnStr2011
            {
                get
                {
                    return _Global_ConnStr2011;
                }
                set
                {
                    _Global_ConnStr2011 = value;
                }
            }

            public string Global_ConnStr2012
            {
                get
                {
                    return _Global_ConnStr2012;
                }
                set
                {
                    _Global_ConnStr2012 = value;
                }
            }

            public string Global_ConnStr2013
            {
                get
                {
                    return _Global_ConnStr2013;
                }
                set
                {
                    _Global_ConnStr2013 = value;
                }
            }

            public string Global_ConnStr2014
            {
                get
                {
                    return _Global_ConnStr2014;
                }
                set
                {
                    _Global_ConnStr2014 = value;
                }
            }

            public string Global_ConnStr2015
            {
                get
                {
                    return _Global_ConnStr2015;
                }
                set
                {
                    _Global_ConnStr2015 = value;
                }
            }

            public string Global_ConnStr2016
            {
                get
                {
                    return _Global_ConnStr2016;
                }
                set
                {
                    _Global_ConnStr2016 = value;
                }
            }

            public string Global_ConnStr2017
            {
                get
                {
                    return _Global_ConnStr2017;
                }
                set
                {
                    _Global_ConnStr2017 = value;
                }
            }

            public string Global_ConnStr2018
            {
                get
                {
                    return _Global_ConnStr2018;
                }
                set
                {
                    _Global_ConnStr2018 = value;
                }
            }

            #endregion

        }


        public string getmssqlconnstr(int EmrDbNo,out string errormsg)
        {
            string _EFFDATE = "";
            string _connstr = "";
            errormsg = "setp:getmssqlconnstr " + Environment.NewLine;
            string NowDate = DateTime.Now.ToString("yyyyMMdd").ToString();

            if (Global_EFFDATE == null)
            {
                Global_EFFDATE = NowDate;
                _EFFDATE = NowDate;
            }
            else if (Global_EFFDATE == "")
            {
                Global_EFFDATE = NowDate;
                _EFFDATE = NowDate;
            }
            else
            {
                _EFFDATE = Global_EFFDATE;
            }            

            if (Gbo.GetGlobalNum(EmrDbNo) == null)
            {
                //就是要抓
                _connstr = getconnstr2017(EmrDbNo, out errormsg);
                Gbo.SetGlobalNum(EmrDbNo, _connstr);
            }
            else if (Gbo.GetGlobalNum(EmrDbNo) == "")
            {
                //就是要抓
                _connstr = getconnstr2017(EmrDbNo, out errormsg);
                Gbo.SetGlobalNum(EmrDbNo, _connstr);
            }
            else
            {
                _connstr = Gbo.GetGlobalNum(EmrDbNo);
            }

            //重新取得連結字串
            if (_EFFDATE == NowDate)
            { //同一天
                _connstr = Gbo.GetGlobalNum(EmrDbNo);
            }
            else
            {
                Global_EFFDATE = NowDate;
                _connstr = getconnstr2017(EmrDbNo, out errormsg);
                Gbo.SetGlobalNum(EmrDbNo, _connstr);
            }

            return _connstr;

            #region 之前版本
            //            BasicHttpBinding binding = new BasicHttpBinding();
            //            binding.MaxReceivedMessageSize = int.MaxValue;
            //            binding.MaxBufferPoolSize = int.MaxValue;
            //            binding.MaxBufferSize = int.MaxValue;
            //            errormsg = "setp:getmssqlconnstr " + Environment.NewLine;
            //            ErrorLog("setp:getmssqlconnstr ");
            //            string return_connstr = "";

            //            try
            //            {
            //                EMRWebSrvCum.WebService1SoapClient ws1 = new EMRWebSrvCum.WebService1SoapClient();

            //                string EMRWebSrvCum_url = GetEMRPARMT("EMRWebSrvCum_url");
            //                System.ServiceModel.EndpointAddress adr_Asp = new System.ServiceModel.EndpointAddress(EMRWebSrvCum_url);
            //                ws1.Endpoint.Address = adr_Asp;
            //                ws1.Endpoint.Binding = binding;


            //                switch (EmrDbNo)
            //                {
            //                    case 0:
            //                        //2016.10.21
            //                        string rtns = getWebConfigstr(out errormsg);
            //                        if (rtns != "")
            //                        {
            //#if DEBUG
            //                            return_connstr = "Provider=SQLOLEDB.1;Data Source=192.168.19.88;Initial Catalog=emrdb;UID=emr;PWD=" + rtns + ";";
            //#else
            //                        return_connstr = "Provider=SQLOLEDB.1;Data Source=localhost;Initial Catalog=emrdb;UID=emr;PWD=" + rtns + ";";
            //#endif

            //                        }
            //                        else
            //                        {
            //#if DEBUG
            //                            return_connstr = "Provider=SQLOLEDB.1;Data Source=192.168.19.88;Initial Catalog=emrdb;UID=emr;PWD=emr2010~;";
            //#else
            //                        return_connstr = "Provider=SQLOLEDB.1;Data Source=localhost;Initial Catalog=emrdb;UID=emr;PWD=emr2010~;";
            //#endif
            //                        }
            //                        //case 0: return_connstr = "Provider=SQLOLEDB.1;Data Source=192.168.19.88;Initial Catalog=emrdb;UID=emr;PWD=emr2010~";
            //                        break;
            //                    case 88:
            //                        return_connstr = "Provider=SQLOLEDB.1;Data Source=192.168.19.88;Initial Catalog=emrdb;UID=emr;PWD=emr2010~;";
            //                        break;
            //                    case 1:
            //                        return_connstr = "Provider=SQLOLEDB.1;" + ws1.GetEMRPARMT("dbConnection_tst");
            //                        break;
            //                    case 2010:
            //                        return_connstr = "Provider=SQLOLEDB.1;" + ws1.GetEMRPARMT("dbConnection_emrdb2010");
            //                        break;
            //                    case 2011:
            //                        return_connstr = "Provider=SQLOLEDB.1;" + ws1.GetEMRPARMT("dbConnection_emrdb2011");
            //                        break;
            //                    case 2012:
            //                        return_connstr = "Provider=SQLOLEDB.1;" + ws1.GetEMRPARMT("dbConnection_emrdb2012");
            //                        break;
            //                    case 2013:
            //                        return_connstr = "Provider=SQLOLEDB.1;" + ws1.GetEMRPARMT("dbConnection_emrdb2013");
            //                        break;
            //                    case 2014:
            //                        return_connstr = "Provider=SQLOLEDB.1;" + ws1.GetEMRPARMT("dbConnection_emrdb2014");
            //                        break;
            //                    case 2015:
            //                        return_connstr = "Provider=SQLOLEDB.1;" + ws1.GetEMRPARMT("dbConnection_emrdb2015");
            //                        break;
            //                    //case 2:
            //                    //    return_connstr = AspEMRWebSvr.Properties.Settings.Default.EMRDb2Connection.ToString();
            //                    //    break;
            //                    default:
            //                        return_connstr = "Provider=SQLOLEDB.1;" + ws1.GetEMRPARMT("dbConnection_tst");
            //                        break;
            //                }
            //            }
            //            catch (Exception ex)
            //            {
            //                errormsg = errormsg + ex.Message + Environment.NewLine;
            //                ErrorLog(ex.Message);
            //            }
            //            ErrorLog("return_connstr:" + return_connstr);
            //            return return_connstr;
            #endregion

        }

        public string getconnstr2017(int EmrDbNo, out string errormsg) //2017.05.17
        {
            BasicHttpBinding binding = new BasicHttpBinding();
            binding.MaxReceivedMessageSize = int.MaxValue;
            binding.MaxBufferPoolSize = int.MaxValue;
            binding.MaxBufferSize = int.MaxValue;
            errormsg =  "setp:getconnstr2017 " + Environment.NewLine;
            ErrorLog("setp:getmssqlconnstr ");
            string return_connstr = "";

            try
            {
                EMRWebSrvCum.WebService1SoapClient ws1 = new EMRWebSrvCum.WebService1SoapClient();

                string EMRWebSrvCum_url = GetEMRPARMT("EMRWebSrvCum_url");
                System.ServiceModel.EndpointAddress adr_Asp = new System.ServiceModel.EndpointAddress(EMRWebSrvCum_url);
                ws1.Endpoint.Address = adr_Asp;
                ws1.Endpoint.Binding = binding;


                switch (EmrDbNo)
                {
                    case 0:
                        //2016.10.21
                        string rtns = getWebConfigstr(out errormsg);
                        if (rtns != "")
                        {
#if DEBUG
                            return_connstr = "Provider=SQLOLEDB.1;Data Source=192.168.19.88;Initial Catalog=emrdb;UID=emr;PWD=" + rtns + ";";
#else
                        return_connstr = "Provider=SQLOLEDB.1;Data Source=localhost;Initial Catalog=emrdb;UID=emr;PWD=" + rtns + ";";
#endif

                        }
                        else
                        {
#if DEBUG
                            return_connstr = "Provider=SQLOLEDB.1;Data Source=192.168.19.88;Initial Catalog=emrdb;UID=emr;PWD=emr2010~;";
#else
                        return_connstr = "Provider=SQLOLEDB.1;Data Source=localhost;Initial Catalog=emrdb;UID=emr;PWD=emr2010~;";
#endif
                        }
                        //case 0: return_connstr = "Provider=SQLOLEDB.1;Data Source=192.168.19.88;Initial Catalog=emrdb;UID=emr;PWD=emr2010~";
                        break;
                    case 1:
                        return_connstr = "Provider=SQLOLEDB.1;" + ws1.GetEMRPARMT("dbConnection_tst");
                        break;
                    case 2010:
                        return_connstr = "Provider=SQLOLEDB.1;" + ws1.GetEMRPARMT("dbConnection_emrdb2010");
                        break;
                    case 2011:
                        return_connstr = "Provider=SQLOLEDB.1;" + ws1.GetEMRPARMT("dbConnection_emrdb2011");
                        break;
                    case 2012:
                        return_connstr = "Provider=SQLOLEDB.1;" + ws1.GetEMRPARMT("dbConnection_emrdb2012");
                        break;
                    case 2013:
                        return_connstr = "Provider=SQLOLEDB.1;" + ws1.GetEMRPARMT("dbConnection_emrdb2013");
                        break;
                    case 2014:
                        return_connstr = "Provider=SQLOLEDB.1;" + ws1.GetEMRPARMT("dbConnection_emrdb2014");
                        break;
                    case 2015:
                        return_connstr = "Provider=SQLOLEDB.1;" + ws1.GetEMRPARMT("dbConnection_emrdb2015");
                        break;
                    case 2016:
                        return_connstr = "Provider=SQLOLEDB.1;" + ws1.GetEMRPARMT("dbConnection_emrdb2016");
                        break;
                    case 2017:
                        return_connstr = "Provider=SQLOLEDB.1;" + ws1.GetEMRPARMT("dbConnection_emrdb2017");
                        break;
                    case 2018:
                        return_connstr = "Provider=SQLOLEDB.1;" + ws1.GetEMRPARMT("dbConnection_emrdb2018");
                        break;
                    case 88:
                        return_connstr = "Provider=SQLOLEDB.1;Data Source=192.168.19.88;Initial Catalog=emrdb;UID=emr;PWD=emr2010~;";
                        break;
                    default:
                        return_connstr = "Provider=SQLOLEDB.1;" + ws1.GetEMRPARMT("dbConnection_tst");
                        break;
                }

            }
            catch (Exception ex)
            {
                errormsg = errormsg + ex.Message + Environment.NewLine;
                ErrorLog(ex.Message);
            }
            ErrorLog("return_connstr:"+ return_connstr);
            return return_connstr;
        }
        //[WebMethod(Description = "取得電子病歷參數檔資料")]
        public string GetEMRPARMT(string PARMTNAM)
        {
            string connstr = "";
            OracleConnection conn = null;
            OracleCommand cmd = null;
            string OrdbName = "";
            string returnstr = "";
            try
            {
                OrdbName = "IPD8";

                if (!_connstrings.TryGetValue(OrdbName, out connstr))
                {
                    return "";
                }

                string sql = "select PARMTNAM,PARMTVALUE,PARMTSETTYP,EFG,RTP,RTT FROM EMRPARMT where PARMTNAM=:PARMTNAM ";
                conn = new OracleConnection(connstr);
                conn.Open();
                cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new Oracle.DataAccess.Client.OracleParameter("PARMTNAM", Oracle.DataAccess.Client.OracleDbType.Varchar2)).Value = PARMTNAM;

                OracleDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    returnstr = reader["PARMTVALUE"].ToString().Trim();
                    break;
                }
                return returnstr;
            }
            catch (Exception e)
            {
                return "";
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            };
        }

        //當地主機密碼設定 非預設值時
        public string getWebConfigstr(out string errormsg)
        {
            string return_connstr = "";
            errormsg =  "setp:getWebConfigstr" +Environment.NewLine;
            ErrorLog("setp:getWebConfigstr");
            try
            {
                return_connstr = WebConfigurationManager.AppSettings["EMRDbConnection"].ToString();

                // 如果本地的連線密碼不一樣 , 需要在 Web.config 中加入下面字串 2016.10.21 輔英上線時修改
                //  <appSettings>
                //    <add key="EMRDbConnection" value="Provider=SQLOLEDB.1;Password=EmrFy@2016;Persist Security Info=True;User ID=emr;Initial Catalog=emrdb;Data Source=172.25.19.93"/>
                //  </appSettings>

            }
            catch (Exception ex)
            { 
                errormsg = errormsg +ex.Message + Environment.NewLine;
                ErrorLog(ex.Message);
            }            
            return return_connstr;
        }

        public bool ErrorLog(string body)
        {
            if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath("log")))
            {
                Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath("log")); //DateTime.Now.ToString("yyyyMMdd-HHmmssffffff")
            }

            string path = System.Web.HttpContext.Current.Server.MapPath("log") + "\\log_" + DateTime.Now.ToString("yyyyMMdd") + "_log.txt";
            
            bool return_f = false;
            try
            {
                string sTime = DateTime.Now.ToString("HH:mm:ss ");
                string sDate = DateTime.Now.ToString("yyyy-MM-dd ");

                // This text is added only once to the file.
                if (!File.Exists(path))
                {// Create a file to write to                    
                    File.WriteAllText(path, sDate + sTime + ">> " + body+Environment.NewLine, Encoding.UTF8);
                }
                else
                {
                    File.AppendAllText(path, sDate + sTime + ">> " + body+Environment.NewLine, Encoding.UTF8);
                }
                //string readText = File.ReadAllText(path);
                //Console.WriteLine(readText);
                return_f = true;
            }
            catch (Exception e)
            {
                //File.WriteAllText(AspEMRWebSvr.Properties.Settings.Default.EMRTempFilePath + "\\AspEMRWebSvr_" + DateTime.Now.ToString("yyyyMMdd") + "_log.txt", e.Message, Encoding.UTF8);
                return_f = false;
            };
            return return_f;

        }

    }


}