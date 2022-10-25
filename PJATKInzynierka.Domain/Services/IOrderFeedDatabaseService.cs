﻿using PJATKInżynierka.DTOs.OrderFeedDTOs;
using PJATKInżynierka.Models;

namespace Application.Services.OrdersFeed
{
    public interface IOrderFeedDatabaseService
    {
        public Task AddOrderFeed(AddOrderFeedDTO orderFeed, int farmId);
        public Task<List<OrderFeed>> GetOrdersFeed(int farmId);
    }
}