using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
public partial class _Default : System.Web.UI.Page
{
protected SqlConnection objConn = new
SqlConnection(ConfigurationManager.ConnectionStrings["test"].ToString());
protected string sql = "";
protected void Page_Load(object sender, EventArgs e)
{
if((Request.QueryString["login"] != null) &&
(Request.QueryString["password"] != null))
{
Response.Write(Request.QueryString["login"].ToString() + "<BR><BR><BR>" +
Request.QueryString["password"].ToString());
sql = "SELECT first_name + ' ' + last_name + ' ' + middle_name FROM users WHERE
username = '" + Request.QueryString["login"] + "' " +
"AND password = '" + Request.QueryString["password"] + "'";
Login();
}
}
public void btnSubmit_Clicked(object o, EventArgs e)
{
lblErrorMessage.Text = "";
lblErrorMessage.Visible = false;
if (txtLogin.Text == "")
{
lblErrorMessage.Text = "Missing login name!<br />";
lblErrorMessage.Visible = true;
}
else
{
if (txtPassword.Text == "")
{
lblErrorMessage.Text = "Missing password!<br />";
lblErrorMessage.Visible = true;
}
else
{
sql = "SELECT first_name + ' ' + last_name + ' ' + middle_name FROM users WHERE
username = '" + txtLogin.Text + "' " +
"AND password = '" + txtPassword.Text + "'";
Login();
}
}
}
private void Login()
{
//correct sql string using sql parameters.
//string sql = "SELECT first_name + ' ' + last_name FROM users WHERE username =
@txtLogin " +
// "AND password = @txtPassword";
SqlCommand cmd = new SqlCommand(sql, objConn);
//each parameter needs added for each user inputted value...
//to take the input literally and not break out with malicious input....
//cmd.Parameters.AddWithValue("@txtLogin", txtLogin.Text);
//cmd.Parameters.AddWithValue("@txtPassword", txtPassword.Text);
objConn.Open();
if (cmd.ExecuteScalar() != DBNull.Value)
{
if (Convert.ToString(cmd.ExecuteScalar()) != "")
{
lblErrorMessage.Text = "Sucessfully logged in!";
lblErrorMessage.Visible = true;
pnlLogin.Visible = false;
pnlChatterBox.Visible = true;
}
else
{
lblErrorMessage.Text = "Invalid Login!";
lblErrorMessage.Visible = true;
}
}
else
{
lblErrorMessage.Text = "Invalid Username/";
lblErrorMessage.Visible = true;
}
objConn.Close();
}
//<style type="text/css">TABLE {display: none !important;}</style> //remove tables
totally.
//<style type="text/css">body{background-color: #ffffff;}</style> //change
background color
//<style type="text/css">div {display: none !important;}</style> //remove all
divs, blank out page
//<script>alert("hello");</script>
//<meta http-equiv="refresh" content="0; url=http://www.google.com" />
}
Por último, crear un archivo que contenga lo siguiente y guardarlo como:
"C:\Inetpub\wwwroot\Web.config".
<?xml version="1.0"?>
<configuration>
<connectionStrings>
<add name="test"
connectionString="server=localhost;database=WebApp;uid=sa;password=password1;"
providerName="System.Data.SqlClient"/>
</connectionStrings>
<system.web>
<!-- DYNAMIC DEBUG COMPILATION
Set compilation debug="true" to enable ASPX debugging. Otherwise, setting this
value to
false will improve runtime performance of this application.
Set compilation debug="true" to insert debugging symbols(.pdb information)
into the compiled page. Because this creates a larger file that executes
more slowly, you should set this value to true only when debugging and to
false at all other times. For more information, refer to the documentation about
debugging ASP.NET files.
-->
<compilation defaultLanguage="c#" debug="true">
<assemblies>
<add assembly="System.Design, Version=2.0.0.0, Culture=neutral,
PublicKeyToken=B03F5F7F11D50A3A"/>
<add assembly="System.Windows.Forms, Version=2.0.0.0, Culture=neutral,
PublicKeyToken=B77A5C561934E089"/></assemblies></compilation>
<!-- CUSTOM ERROR MESSAGES
Set customErrors mode="On" or "RemoteOnly" to enable custom error messages, "Off"
to disable.
Add <error> tags for each of the errors you want to handle.
"On" Always display custom (friendly) messages.
"Off" Always display detailed ASP.NET error information.
"RemoteOnly" Display custom (friendly) messages only to users not running
on the local Web server. This setting is recommended for security purposes, so
that you do not display application detail information to remote clients.
-->
<customErrors mode="Off"/>
<!-- AUTHENTICATION
This section sets the authentication policies of the application. Possible modes
are "Windows",
"Forms", "Passport" and "None"
"None" No authentication is performed.
"Windows" IIS performs authentication (Basic, Digest, or Integrated Windows)
according to
its settings for the application. Anonymous access must be disabled in IIS.
"Forms" You provide a custom form (Web page) for users to enter their credentials,
and then
you authenticate them in your application. A user credential token is stored in a
cookie.
"Passport" Authentication is performed via a centralized authentication service
provided
by Microsoft that offers a single logon and core profile services for member
sites.
-->
<authentication mode="Windows"/>
<!-- AUTHORIZATION
This section sets the authorization policies of the application. You can allow or
deny access
to application resources by user or role. Wildcards: "*" mean everyone, "?" means
anonymous
(unauthenticated) users.
-->
<authorization>
<allow users="*"/>
<!-- Allow all users -->
<!-- <allow users="[comma separated list of users]"
roles="[comma separated list of roles]"/>
<deny users="[comma separated list of users]"
roles="[comma separated list of roles]"/>
-->
</authorization>
<!-- APPLICATION-LEVEL TRACE LOGGING
Application-level tracing enables trace log output for every page within an
application.
Set trace enabled="true" to enable application trace logging. If
pageOutput="true", the
trace information will be displayed at the bottom of each page. Otherwise, you can
view the
application trace log by browsing the "trace.axd" page from your web application
root.
-->
<trace enabled="false" requestLimit="10" pageOutput="false" traceMode="SortByTime"
localOnly="true"/>
<!-- SESSION STATE SETTINGS
By default ASP.NET uses cookies to identify which requests belong to a particular
session.
If cookies are not available, a session can be tracked by adding a session
identifier to the URL.
To disable cookies, set sessionState cookieless="true".
-->
<sessionState mode="InProc" stateConnectionString="tcpip=127.0.0.1:42424"
sqlConnectionString="data source=127.0.0.1;Trusted_Connection=yes"
cookieless="false" timeout="20"/>
<!-- GLOBALIZATION
This section sets the globalization settings of the application.
-->
<globalization requestEncoding="utf-8" responseEncoding="utf-8"/>
</system.web>
</configuration>