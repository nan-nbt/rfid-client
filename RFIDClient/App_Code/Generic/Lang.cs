using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Collections.Generic;
// Ref Json
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace Generic
{
    /// <summary>
    /// 用于翻譯
    /// Author:Hanxing; Created: 2017-3-9
    /// Modifier:           Modified: 
    /// </summary>
    public class Lang
    {
        static string sOrgVal = "";
        static string langPath = AppDomain.CurrentDomain.BaseDirectory + "Lang.json";

        /// <summary>翻譯</summary>
        public static string Dict(string langKey, string orgVal)
        {
            string retMsg = "";
            sOrgVal = orgVal;
            try
            {
                retMsg = Dict(langKey);
            }
            catch (Exception ex)
            {
                retMsg = orgVal;
            }
            return retMsg;
        }

        /// <summary>翻譯</summary>
        public static string Dict(string langKey)
        {
            dynamic objLang = null;
            string retMsg = "", sLang = "", langType = "";
            try
            {
                sLang = AppConfig.LangData;
                if (string.IsNullOrEmpty(AppConfig.LangData) && File.Exists(langPath))
                    sLang = File.ReadAllText(langPath);
                objLang = JsonConvert.DeserializeObject<dynamic>(sLang);
                langType = string.IsNullOrEmpty(AppConfig.LangType) ? "EN" : AppConfig.LangType;
                langType = langType.Trim().ToUpper();
                langKey = langKey.Trim().ToUpper();
                retMsg = (objLang != null && langType != "" && ("CN,EN,TW,VN,ID").Contains(langType) && langKey != "" && objLang[langType][langKey] != null) ? objLang[langType][langKey] : sOrgVal;
            }
            catch (Exception ex)
            {
                retMsg = "ERROR:" + ex.ToString() + "<br/>LangKey:" + langKey;
            }
            return retMsg;
        }
    }
}