﻿using BlazorInvoiceApp.Data;
using BlazorInvoiceApp.Dtos;
using System.Security.Claims;

namespace BlazorInvoiceApp.Repository;

public interface IGenericOwnedRepository<TEntity, TDto>
    where TEntity : class, IEntity, IOwnedEntity
    where TDto : class, IDto, IOwnedDto
{
    Task<TDto> GetMineById(ClaimsPrincipal user, string id);
    Task<List<TDto>> GetAllMine(ClaimsPrincipal user);
    Task<string> AddMine(ClaimsIdentity user, TDto dto);
    Task<TDto> UpdateMine(ClaimsIdentity user, TDto dto);
    Task<bool> DeleteMine(ClaimsIdentity user, string id);
}
