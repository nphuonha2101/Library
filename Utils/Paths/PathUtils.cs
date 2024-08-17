namespace Library.Utils.Paths;

public class PathUtils
{
    
    public static string GetHost(HttpContext httpContext)
    {
        return httpContext.Request.Scheme + "://" + httpContext.Request.Host;
    }
    
    public static string GetWebRootPath(IWebHostEnvironment env)
    {
        return env.WebRootPath;
    }
}