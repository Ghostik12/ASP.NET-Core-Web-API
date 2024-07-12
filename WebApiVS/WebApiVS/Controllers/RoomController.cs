using AutoMapper;
using HomeApi.Contracts.Models.Rooms;
using HomeApi.Data.Models;
using HomeApi.Data.Queries;
using HomeApi.Data.Repos;
using Microsoft.AspNetCore.Mvc;

namespace WebApiVS.Controllers
{
    /// <summary>
    /// Контроллер комнат
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class RoomController : ControllerBase
    {
        private IRoomRepository _repository;
        private IMapper _mapper;

        public RoomController(IRoomRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        //TODO: Задание - добавить метод на получение всех существующих комнат

        /// <summary>
        /// Добавление комнаты
        /// </summary>
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Add([FromBody] AddRoomRequest request)
        {
            var existingRoom = await _repository.GetRoomByName(request.Name);
            if (existingRoom == null)
            {
                var newRoom = _mapper.Map<AddRoomRequest, Room>(request);
                await _repository.AddRoom(newRoom);
                return StatusCode(201, $"Комната {request.Name} добавлена!");
            }

            return StatusCode(409, $"Ошибка: Комната {request.Name} уже существует.");
        }

        [HttpPut]
        [Route("{name}")]
        public async Task<IActionResult> EditRoom([FromRoute] string name, [FromBody] EditRoomRequest request)
        {
            var chekId = await _repository.GetRoomByName(name);
            if (chekId == null)
                return StatusCode(400, $"Ошибка: Комната {chekId} не подключена. Сначала подключите комнату!");

            await _repository.UpdateRoom(chekId, new UpdateRoomQuery() { NewGasConnected = request.NewGasConnected, NewVoltage = request.NewVoltage });
            return StatusCode(200, $"Устройство обновлено! Комната подключения  —  {name}");
        }
    }
}
