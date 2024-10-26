using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using Newtonsoft.Json.Linq;
using Microsoft.Win32;

/**
 * Class for handle SSO OAuth 2.0 login method
 * created by. anan 20240902 
 */ 

namespace RFIDClient
{
    public partial class FrmOAuth2Login : Form
    {
        private const string clientId = "rfid-client-gxnudd9j";
        private const string redirectUri = "com.pouchen.dotnet://oauth2redirect";
        private const string authEndpoint = "https://iam.pouchen.com/auth/realms/pcg/protocol/openid-connect/auth";
        private const string tokenEndpoint = "https://iam.pouchen.com/auth/realms/pcg/protocol/openid-connect/token";
        private const string userinfoEndpoint = "https://iam.pouchen.com/auth/realms/pcg/protocol/openid-connect/userinfo";
        private const string logoutEndpoint = "https://iam.pouchen.com/auth/realms/pcg/protocol/openid-connect/logout";

        private string accessToken;
        private string refreshToken;
        public string ssouid;

        // constructor
        public FrmOAuth2Login()
        {
            InitializeComponent();
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072; 
        }

        // load form
        private void FrmOAuth2Login_Load(object sender, EventArgs e)
        {
            SetBrowserEmulationVersion(); // set default browser IE 11

            // Check if user is already logged in
            if (IsUserLoggedIn())
            {
                string userInfo = GetUserInfo(accessToken);
                if (!string.IsNullOrEmpty(userInfo))
                {
                    DisplayUserInfo(userInfo);
                    this.Close();
                }
            }
            else
            {
                StartOAuthLoginFlow();
            }
        }

        // navigate to the login page OAuth 2.0
        private void StartOAuthLoginFlow()
        {
            //webBrowserOAuth.ScriptErrorsSuppressed = true;
            string authorizationUrl = authEndpoint + "?client_id=" + clientId + "&redirect_uri=" + redirectUri + "&response_type=code";
            webBrowserOAuth.Navigate(authorizationUrl);
        }

        // set defult browser using IE 11
        private void SetBrowserEmulationVersion()
        {
            string appName = System.IO.Path.GetFileNameWithoutExtension(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            string registryKeyPath = String.Format(@"HKEY_CURRENT_USER\Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION");

            Registry.SetValue(registryKeyPath, appName + ".exe", 11001, RegistryValueKind.DWord);
        }

        // navigated to switch auth_code to get token
        private void webBrowseOAuth_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            if (e.Url.ToString().StartsWith(redirectUri))
            {
                var queryParams = ParseQueryString(e.Url.Query);
                if (queryParams["code"] != null)
                {
                    GetTokenAndUserInfo(queryParams["code"]);
                }
            }
        }

        // function for parse query string
        private NameValueCollection ParseQueryString(string query)
        {
            var queryParams = new NameValueCollection();
            string[] queryParamsArray = query.TrimStart('?').Split('&');
            foreach (string param in queryParamsArray)
            {
                string[] keyValue = param.Split('=');
                queryParams.Add(keyValue[0], keyValue[1]);
            }
            return queryParams;
        }

        // function for get token and user information from OAuth 2.0
        private void GetTokenAndUserInfo(string authCode)
        {
            string tokenResponse = GetToken(authCode);
            if (!string.IsNullOrEmpty(tokenResponse))
            {
                accessToken = ExtractJsonValue(tokenResponse, "access_token");
                refreshToken = ExtractJsonValue(tokenResponse, "refresh_token");

                SaveAccessToken(accessToken);

                string userInfo = GetUserInfo(accessToken);
                if (!string.IsNullOrEmpty(userInfo))
                {
                    DisplayUserInfo(userInfo);
                    this.Close();
                }
            }
        }

        //function for get token using endpoint OAuth 2.0
        private string GetToken(string authCode)
        {
            using (WebClient client = new WebClient())
            {
                var postData = new NameValueCollection
                {
                    { "grant_type", "authorization_code" },
                    { "code", authCode },
                    { "redirect_uri", redirectUri },
                    { "client_id", clientId }
                };

                try
                {
                    byte[] responseBytes = client.UploadValues(tokenEndpoint, "POST", postData);
                    return Encoding.UTF8.GetString(responseBytes);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error getting token: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
        }

        // function for extract value of json response
        private string ExtractJsonValue(string jsonResponse, string key)
        {
            try
            {
                var jsonObject = JObject.Parse(jsonResponse);
                JToken token;
        
                // Check if the key exists in the JSON object
                if (jsonObject.TryGetValue(key, out token))
                {
                    return token.ToString();
                }
                else
                {
                    return null; // or throw an exception if you want to handle missing keys differently
                }
            }
            catch (Exception ex)
            {
                        MessageBox.Show("Error extracting " + key + ": " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        // function for get user information from OAuth 2.0
        private string GetUserInfo(string accessToken)
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add("Authorization", "Bearer " + accessToken);
                try
                {
                    return client.DownloadString(userinfoEndpoint);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error getting user info: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
        }

        // function for set user information
        private void DisplayUserInfo(string userInfo)
        {
            // Parse and format user info
            var userInfoObject = JObject.Parse(userInfo);
            string pccuid = (string)userInfoObject["pccuid"];
            string uid = (string)userInfoObject["uid"];
            string name = (string)userInfoObject["name"];

            // Format the message
            //string message = "PCCUID: " + pccuid + "\nUID: " + uid + "\nName: " + name;

            // Display the message in a MessageBox
            //MessageBox.Show(message, "User Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ssouid = uid;
        }

        // function for handle login status
        private bool IsUserLoggedIn()
        {
            // Check if the access token exists (this could be more sophisticated, e.g., checking expiry)
            return !string.IsNullOrEmpty(accessToken);
        }

        // function for save token access
        private void SaveAccessToken(string token)
        {
            // You can implement more persistent storage if needed
            accessToken = token;
        }

        // funtion for logout from OAuth 2.0
        public void Logout()
        {
            string logoutUrl = logoutEndpoint + "?client_id=" + clientId + "&post_logout_redirect_uri=" + redirectUri;

            // Use WebClient to call the logout endpoint (if needed), or simply navigate to the logout URL
            try
            {
                webBrowserOAuth.Navigate(logoutUrl);

                // Clear the tokens
                accessToken = null;
                refreshToken = null;

                //MessageBox.Show("Logged out successfully.", "Logout", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during logout: " + ex.Message);
            }
        }
    }
}
