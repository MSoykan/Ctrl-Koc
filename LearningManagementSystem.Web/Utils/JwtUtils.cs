namespace LearningManagementSystem.Web.Utils;

public static class JwtUtils
{
    public static bool IsUserAdminBasedOnToken(HttpContext httpContext)
    {
        var isAdmin = false;

        var token = httpContext.Session.GetString("JWTToken");

        if (!string.IsNullOrEmpty(token))
        {
            try
            {
                var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
                var tokenS = handler.ReadJwtToken(token);

                var roleClaim = tokenS.Claims.FirstOrDefault(c => c.Type == "role");

                if (roleClaim != null && roleClaim.Value == "Admin")
                {
                    isAdmin = true;
                }
            }
            catch (Exception ex)
            {

            }
        }

        return isAdmin;
    }
    
    public static bool IsUserInstructorBasedOnToken(HttpContext httpContext)
    {
        var isInstructor = false;

        var token = httpContext.Session.GetString("JWTToken");

        if (!string.IsNullOrEmpty(token))
        {
            try
            {
                var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
                var tokenS = handler.ReadJwtToken(token);

                var roleClaim = tokenS.Claims.FirstOrDefault(c => c.Type == "role");

                if (roleClaim != null && roleClaim.Value == "Instructor")
                {
                    isInstructor = true;
                }
            }
            catch (Exception ex)
            {

            }
        }

        return isInstructor;
    }
}