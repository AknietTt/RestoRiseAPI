﻿using AutoMapper;
using RestoRise.BuisnessLogic.ICrudRepository;
using RestoRise.Domain.Entities;

namespace RestoRise.Storage.Repositories;

public class MenuRepository:BaseRepository<Menu>, IMenuRepositry
{
    public MenuRepository(AppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }
}