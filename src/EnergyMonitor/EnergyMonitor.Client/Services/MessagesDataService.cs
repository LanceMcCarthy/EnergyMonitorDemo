using EnergyMonitor.Client.Models;
using Microsoft.EntityFrameworkCore;

namespace EnergyMonitor.Client.Services
{
    public class MessagesDataService(MeasurementsDbContext dbContext)
    {
        private DateTime lastSaved = DateTime.Now;

        public async Task<List<MqttDataItem>> GetMeasurementsAsync()
        {
            return await dbContext.Measurements.ToListAsync();
        }

        public async Task<MqttDataItem> AddMeasurementAsync(MqttDataItem dataItem)
        {
            try
            {
                dbContext.Measurements.Add(dataItem);

                await this.SaveRecentChangesAsync();
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

                    await this.SaveRecentChangesAsync();
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

                await this.SaveRecentChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task SaveRecentChangesAsync()
        {
            if (DateTime.Now - lastSaved > TimeSpan.FromSeconds(20))
            {
                await dbContext.SaveChangesAsync();
                lastSaved = DateTime.Now;
            }
        }
    }
}
