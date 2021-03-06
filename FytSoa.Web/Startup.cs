using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Service.Implements;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog.Web;

namespace FytSoa.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IErpAppSettingService, ErpAppSettingService>();
            services.AddTransient<IErpSaleOrderService, ErpSaleOrderService>();
            services.AddTransient<IErpSaleOrderGoodsService, ErpSaleOrderGoodsService>();
            services.AddTransient<IErpSupplierService, ErpSupplierService>();
            services.AddTransient<IErpPurchaseService, ErpPurchaseService>();
            services.AddTransient<IErpPurchaseGoodsService, ErpPurchaseGoodsService>();

            services.AddTransient<IInventoryService, InventoryService>();

            services.AddTransient<IErpTransferService, ErpTransferService>();
            services.AddTransient<IErpTransferGoodsService, ErpTransferGoodsService>();
            services.AddTransient<IErpInOutLogService, ErpInOutLogService>();
            services.AddTransient<IErpPackLogService, ErpPackLogService>();
            services.AddTransient<IErpReturnOrderService, ErpReturnOrderService>();
            services.AddTransient<IErpReturnGoodsService, ErpReturnGoodsService>();
            services.AddTransient<IErpBackGoodsService, ErpBackGoodsService>();
            services.AddTransient<IErpGoodsService, ErpGoodsService>();
            services.AddTransient<IErpGoodsSkuService, ErpGoodsSkuService>();
            services.AddTransient<IErpPushService, ErpPushService>();
            services.AddTransient<IErpShopSkuService, ErpShopSkuService>();
            services.AddTransient<IErpShopsService, ErpShopsService>();
            services.AddTransient<IErpShopActivityService, ErpShopActivityService>();
            services.AddTransient<IErpShopUserService, ErpShopUserService>();
            services.AddTransient<IErpStaffService, ErpStaffService>();

            services.AddTransient<ISysBtnFunService, SysBtnFunService>();
            services.AddTransient<ISysPermissionsService, SysPermissionsService>();
            services.AddTransient<ISysLogService, SysLogService>();
            services.AddTransient<ISysAdminService, SysAdminService>();
            services.AddTransient<ISysCodeService, SysCodeService>();
            services.AddTransient<ISysCodeTypeService, SysCodeTypeService>();
            services.AddTransient<ISysOrganizeService, SysOrganizeService>();
            services.AddTransient<ISysMenuService, SysMenuService>();
            services.AddTransient<ISysRoleService, SysRoleService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
            {
                o.LoginPath = new PathString("/fytadmin/login");
                o.AccessDeniedPath = new PathString("/error");
            });

            //��������
            services.AddCors();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials());
            });
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                //app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseAuthentication();

            loggerFactory.AddNLog();//����NLog  
            env.ConfigureNLog("nlog.config");//��ȡNlog�����ļ� 

            app.UseStaticFiles();
            app.UseCors("AllowAll");
            app.UseMvc();            
        }
    }
}
