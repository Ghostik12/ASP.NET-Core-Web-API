using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text;
using WebApiVS.Configuration;
using HomeApi.Contracts.Models.Home;

namespace WebApiVS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private IOptions<HomeOptions> _options;
        private IMapper _mapper;

        public HomeController(IOptions<HomeOptions> options, IMapper mapper)
        {
            _options = options;
            _mapper = mapper;
        }

        [HttpGet] // Для обслуживания Get-запросов
        [Route("info")] // Настройка маршрута с помощью атрибутов
        public IActionResult Info()
        {
            // Объект Stringbuilder, в который будем "собирать" результат из конфигурации
            var pageResult = new StringBuilder();

            // Проставляем все значения из конфигурации для последующего вывода на страницу
            //pageResult.Append($"Добро пожаловать в API вашего дома!{Environment.NewLine}");
            //pageResult.Append($"Здесь вы можете посмотреть основную информацию.{Environment.NewLine}");
            //pageResult.Append($"{Environment.NewLine}"); pageResult.Append($"Количество этажей: {_options.Value.FloorAmount}{Environment.NewLine}");
            //pageResult.Append($"Стационарный телефон: {_options.Value.Telephone}{Environment.NewLine}");
            //pageResult.Append($"Тип отопления: {_options.Value.Heating}{Environment.NewLine}");
            //pageResult.Append($"Напряжение электросети: {_options.Value.CurrentVolts}{Environment.NewLine}");
            //pageResult.Append($"Подключен к газовой сети:  {_options.Value.GasConnected}{Environment.NewLine}");
            //pageResult.Append($"Жилая площадь: {_options.Value.Area} м2{Environment.NewLine}");
            //pageResult.Append($"Материал: {_options.Value.Material}{Environment.NewLine}"); pageResult.Append($"{Environment.NewLine}");
            //pageResult.Append($"Адрес: {_options.Value.Address.Street} {_options.Value.Address.House}/{_options.Value.Address.Building}{Environment.NewLine}");

            var infoResponse = _mapper.Map<HomeOptions, InfoResponse>(_options.Value);

            // Преобразуем результат в строку и выводим, как обычную веб-страницу
            return StatusCode(200, infoResponse);
        }
    }
}
