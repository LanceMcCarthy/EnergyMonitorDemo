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
            try
            {
                dbContext.Measurements.Add(dataItem);

                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }

            return dataItem;
        }

        public async Task<MqttDataItem> UpdateMeasurementAsync(MqttDataItem item)
        {
            try
            {
                var productExist = dbContext.Measurements.FirstOrDefault(p => p.Id == item.Id);

                if (productExist != null)
                {
                    dbContext.Update(item);

                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return item;
        }

        public async Task DeleteMeasurementAsync(MqttDataItem item)
        {
            try
            {
                dbContext.Measurements.Remove(item);

                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
