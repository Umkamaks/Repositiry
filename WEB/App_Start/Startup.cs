﻿using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
[assembly: OwinStartup(typeof(WEB.App_Start.Startup))]

namespace WEB.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/Account/Login"),
            });
        }
    }
}