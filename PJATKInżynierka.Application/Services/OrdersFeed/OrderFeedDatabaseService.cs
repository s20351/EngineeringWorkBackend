using Microsoft.EntityFrameworkCore;
using Domain.DTOs.OrderFeedDTOs;
using Domain.Models;
using PJATKInżynierka.DTOs.OrderFeedDTOs;
using Infrastructure.Database;
using System.Globalization;
using Domain.DTOs.FarmsDTOs;

namespace Application.Services.OrdersFeed
{
    public class OrderFeedDatabaseService : IOrderFeedDatabaseService
    {
        private readonly pjatkContext _pjatkContext;

        public OrderFeedDatabaseService(pjatkContext pjatkContext)
        {
            _pjatkContext = pjatkContext;
        }

        public async Task AddOrderFeed(AddOrderFeedDTO orderFeed, int farmId)
        {
            await _pjatkContext.OrderFeeds.AddAsync(new OrderFeed
            {
                FeedhouseFeedhouseId = orderFeed.FeedHouseID,
                DateOfArrival = orderFeed.DateOfArrival,
                DateOfOrder = DateTime.Now,
                Weight = orderFeed.Weight,
                FarmFarmId = farmId
            });

            await _pjatkContext.SaveChangesAsync();
        }

        public async Task<List<OrderFeed>> GetOrdersFeed(int farmId)
        {
            var ordersFeed = await _pjatkContext.OrderFeeds.Where(x => x.FarmFarmId == farmId).ToListAsync();
            return ordersFeed;
        }

        public async Task<List<DateTime>> GetDeliveriesDates(int farmId)
        {
            var deliveryDates = new List<DateTime>();
            var ordersFeed = await _pjatkContext.OrderFeeds.Where(x => x.FarmFarmId == farmId).ToListAsync();

            foreach (var orderHatchery in ordersFeed)
            {
                deliveryDates.Add(orderHatchery.DateOfArrival);
            }

            return deliveryDates;
        }

        public async Task<List<GetOrdersScheduleDTO>> GetOrdersSchedule(int farmerId)
        {
            List<GetOrdersScheduleDTO> ordersSchedule = new List<GetOrdersScheduleDTO>();

            var farms = await _pjatkContext.Farms.Where(x => x.FarmerFarmerId == farmerId).ToListAsync();
            await _pjatkContext.Feedhouses.LoadAsync();

            foreach(var farm in farms)
            {
               var deliveries = await _pjatkContext.OrderFeeds.Where(x => x.FarmFarmId == farm.FarmId).ToListAsync();
                
                foreach(var delivery in deliveries)
                {
                    ordersSchedule.Add(new GetOrdersScheduleDTO
                    {
                        ObjectID = farm.FarmId,
                        FarmName = farm.Name,
                        ArrivalDate = delivery.DateOfArrival.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                        Weight = delivery.Weight
                    });
                }
            }

            return ordersSchedule;
        }

        public async Task<List<FarmEventDTO>> GetFeedFarmerEvents(int farmerId)
        {
            List<FarmEventDTO> list = new List<FarmEventDTO>();

            var farms = await _pjatkContext.Farms.Where(x => x.FarmerFarmerId == farmerId).ToListAsync();

            foreach(var farm in farms)
            {
                var orderFeeds = await _pjatkContext.OrderFeeds.Where(x => x.FarmFarmId == farm.FarmId).ToListAsync();

                foreach (var orderFeed in orderFeeds)
                {
                    list.Add(new FarmEventDTO
                    {
                        Title = "Pasza",
                        AllDay = true,
                        Start = orderFeed.DateOfArrival.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                        End = orderFeed.DateOfArrival.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
                    });
                }
            }

            return list;
        }
    }
}
