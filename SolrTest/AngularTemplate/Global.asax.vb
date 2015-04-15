Imports System.Web.Optimization
Imports System.Net.WebRequestMethods.Http
Imports System.Web.Http


Public Class Global_asax
    Inherits HttpApplication

    Sub Application_Start(sender As Object, e As EventArgs)
        ' Fires when the application is started
        RouteConfig.RegisterRoutes(RouteTable.Routes)
        BundleConfig.RegisterBundles(BundleTable.Bundles)
        GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
        GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter)
        'RouteDebug.RouteDebugger.RewriteRoutesForTesting(RouteTable.Routes)
        RouteTable.Routes.MapHttpRoute(name:="DefaultApi", routeTemplate:="api/{controller}/{action}/{id}", defaults:=New With { _
    Key .id = RouteParameter.[Optional]})

        'RouteTable.Routes.MapHttpRoute(name:="GetLogs", routeTemplate:="api/{controller}", defaults:=New With {Key .id = RouteParameter.[Optional]})
    End Sub
End Class