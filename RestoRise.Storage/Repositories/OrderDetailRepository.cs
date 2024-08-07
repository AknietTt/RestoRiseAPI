﻿using AutoMapper;
using RestoRise.Application.Interfaces.Repositories;
using RestoRise.Domain.Entities;

namespace RestoRise.Storage.Repositories;

public class OrderDetailRepository:BaseRepository<OrderDetail>, IOrderDetailRepository
{
    public OrderDetailRepository(AppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }
}