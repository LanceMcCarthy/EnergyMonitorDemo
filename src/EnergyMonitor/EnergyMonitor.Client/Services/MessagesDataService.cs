using EnergyMonitor.Client.Models;
using Microsoft.EntityFrameworkCore;

namespace EnergyMonitor.Client.Services
{
    public class MessagesDataService(MeasurementsDbContext dbContext)
    {
        public async Task<List<MqttDataItem>> GetMeasurementsAsync()
        {
            return await dbContext.Measurements.ToListAsync();
        }

        public async Task<MqttDataItem> AddMeasurementAsync(MqttDataItem dataItem)
        {
            dbContext.Measurements.Add(dataItem);

            await dbContext.SaveChangesAsync();

            return dataItem;
        }

        public async Task<MqttDataItem> UpdateMeasurementAsync(MqttDataItem item)
        {
            var productExist = dbContext.Measurements.FirstOrDefault(p => p.Id == item.Id);

            if (productExist != null)
            {
                dbContext.Update(item);

                await dbContext.SaveChangesAsync();
            }

            return item;
        }

        public async Task DeleteMeasurementAsync(MqttDataItem item)
        {
            dbContext.Measurements.Remove(item);

            await dbContext.SaveChangesAsync();
        }
    }
}
