using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TPDB.Auth.Common;
using TPDB.Resource.API.Data;

namespace TPDB.Resource.API
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
            services.AddControllers();

            //����������� ���������
            var connection = Configuration.GetConnectionString("TPDBResContextConnection");
            services.AddDbContext<TPDBResourceContext>(options => options.UseSqlServer(connection));

            //���������� Auth ������������
            var authOptions = Configuration.GetSection("Auth").Get<AuthOptions>();

            //��������� �������������� �� ������ JWT-������
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    //��������� ������������ �����, ��������� �� http
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        //����� �� �������������� �������� ������?
                        ValidateIssuer = true,
                        //�������� ������
                        ValidIssuer = authOptions.Issuer,

                        //����� �� �������������� ����������� ������?
                        ValidateAudience = true,
                        //����������� ������
                        ValidAudience = authOptions.Audience,

                        //������������ �������� ������?
                        ValidateLifetime = true,

                        //��������� ����� ������������
                        IssuerSigningKey = authOptions.GetSymetricSecurityKey(), //HS256
                        //������������ �� ���� ������������?
                        ValidateIssuerSigningKey = true
                    };
                });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
