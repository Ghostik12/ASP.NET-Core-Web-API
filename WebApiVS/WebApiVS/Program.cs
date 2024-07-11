using System.Reflection;
using FluentValidation.AspNetCore;
using WebApiVS.Configuration;
using HomeApi.Contracts.Validation;
using HomeApi.Data.Repos;

namespace WebApiVS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var confi = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Development.json")
                .AddJsonFile("HomeOptions.json")
                .Build();
            // Add services to the container.

            builder.Services.AddControllers();
            // ���������� ���������
            builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AddDeviceRequestValidator>());

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            // ��������� ����� ������
            builder.Services.Configure<HomeOptions>(confi);
            // ���������� �����������
            var assembly = Assembly.GetAssembly(typeof(MappingProfile));
            builder.Services.AddAutoMapper(assembly);
            // ����������� ������� ����������� ��� �������������� � ����� ������
            builder.Services.AddSingleton<IDeviceRepository, DeviceRepository>();
            builder.Services.AddSingleton<IRoomRepository, RoomRepository>();
            // ��������� ������ ������ (��������� Json-������))
            builder.Services.Configure<Address>(Configuration.GetSection("Address"));

            string connection = Configuration.GetConnectionString("DefaultConnection");

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
