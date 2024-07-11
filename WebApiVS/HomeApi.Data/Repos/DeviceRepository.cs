using Microsoft.EntityFrameworkCore;
using HomeApi.Data.Models;
using HomeApi.Data.Queries;

namespace HomeApi.Data.Repos
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly HomeApiContext _context;

        public DeviceRepository(HomeApiContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Выгрузить все устройства
        /// </summary>
        public async Task<Device[]> GetDevices()
        {
            return await _context.Devices
                .Include(d => d.Room)
                .ToArrayAsync();
        }

        /// <summary>
        /// Найти устройство по имени
        /// </summary>
        public async Task<Device> GetDeviceByName(string name)
        {
            return await _context.Devices
                .Include(d => d.Room)
                .Where(d => d.Name == name).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Найти устройство по идентификатору
        /// </summary>
        public async Task<Device> GetDeviceById(Guid id)
        {
            return await _context.Devices
                .Include(d => d.Room)
                .Where(d => d.Id == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Добавить новое устройство
        /// </summary>
        public async Task SaveDevice(Device device, Room room)
        {
            // Привязываем новое устройство к соответствующей комнате перед сохранением
            device.RoomId = room.Id;
            device.Room = room;

            // Добавляем в базу 
            var entry = _context.Entry(device);
            if (entry.State == EntityState.Detached)
                await _context.Devices.AddAsync(device);

            // Сохраняем изменения в базе 
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Обновить существующее устройство
        /// </summary>
        public async Task UpdateDevice(Device device, Room room, UpdateDeviceQuery query)
        {
            // Привязываем новое устройство к соответствующей комнате перед сохранением
            device.RoomId = room.Id;
            device.Room = room;

            // Если в запрос переданы параметры для обновления - проверяем их на null
            // И если нужно - обновляем устройство
            if (!string.IsNullOrEmpty(query.NewName))
                device.Name = query.NewName;
            if (!string.IsNullOrEmpty(query.NewSerial))
                device.SerialNumber = query.NewSerial;

            // Добавляем в базу 
            var entry = _context.Entry(device);
            if (entry.State == EntityState.Detached)
                _context.Devices.Update(device);

            // Сохраняем изменения в базе 
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Удалить устройство
        /// </summary>
        public async Task DeleteDevice(Device device)
        {
            _context.Devices.Remove(device);
            await _context.SaveChangesAsync();
        }
    }
}
