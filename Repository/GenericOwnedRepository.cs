using AutoMapper;
using BlazorInvoiceApp.Data;
using BlazorInvoiceApp.Dtos;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.Claims;

namespace BlazorInvoiceApp.Repository;

public class GenericOwnedRepository<TEntity, TDto> : IGenericOwnedRepository<TEntity, TDto>
    where TEntity : class, IEntity, IOwnedEntity
    where TDto : class, IDto, IOwnedDto
{
    public readonly ApplicationDbContext _context;
    public readonly IMapper _mapper;

    public GenericOwnedRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public virtual async Task<string> AddMine(ClaimsPrincipal user, TDto dto)
    {
        var userId = GetMysUserId(user);

        if (userId is not null)
        {
            dto.UserId = userId;
            dto.Id = Guid.NewGuid().ToString();

            var entity = _mapper.Map<TEntity>(dto);

            await _context.Set<TEntity>().AddAsync(entity);

            return entity.Id;
        }

        return null!;
    }

    public virtual async Task<bool> DeleteMine(ClaimsPrincipal user, string id)
    {
        var userId = GetMysUserId(user);

        if (userId is not null)
        {
            var entity = await _context.Set<TEntity>()
                .Where(e => e.UserId == userId
                    && e.Id == id)
                .FirstOrDefaultAsync();

            if (entity is not null)
            {
                _context.Remove(entity);

                return true;
            }
        }

        return false;
    }

    public virtual async Task<List<TDto>> GetAllMine(ClaimsPrincipal user)
    {
        var userId = GetMysUserId(user);

        if (userId is not null)
        {
            var entities = await _context.Set<TEntity>()
                .Where(e => e.UserId == userId)
                .ToListAsync();

            var result = _mapper.Map<List<TDto>>(entities);

            return result;
        }

        return new List<TDto>();
    }

    public virtual async Task<TDto> GetMineById(ClaimsPrincipal user, string id)
    {
        var userId = GetMysUserId(user);

        if (userId is not null)
        {
            var entity = await _context.Set<TEntity>()
                .Where(e => e.UserId == userId
                    && e.Id == id)
                .FirstOrDefaultAsync();

            var result = _mapper.Map<TDto>(entity);

            return result;
        }

        return null!;
    }

    public virtual async Task<TDto> UpdateMine(ClaimsPrincipal user, TDto dto)
    {
        var userId = GetMysUserId(user);

        if (userId is not null)
        {
            var entity = await _context.Set<TEntity>()
                .Where(e => e.UserId == userId
                    && e.Id == dto.Id)
                .FirstOrDefaultAsync();

            if (entity is not null)
            {
                _mapper.Map(dto, entity);
                _context.Entry(entity).State = EntityState.Modified;

                var result = _mapper.Map<TDto>(entity);

                return result;
            }
        }

        return null!;
    }

    protected string? GetMysUserId(ClaimsPrincipal user)
    {
        var uid = user.FindFirst(ClaimTypes.NameIdentifier);

        if (uid is not null)
        {
            return uid.Value;
        }

        return null;
    }

    protected async Task<List<TDto>> GenericQuery(ClaimsPrincipal user, Expression<Func<TEntity, bool>>? expression, List<Expression<Func<TEntity, bool>>>? includes)
    {
        var userId = GetMysUserId(user);

        if (userId is not null)
        {
            var query = _context.Set<TEntity>()
                .Where(e => e.UserId == userId);

            if (expression is not null)
            {
                query = query.Where(expression);
            }

            if (includes is not null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            var entities = await query.ToListAsync();
            var result = _mapper.Map<List<TDto>>(entities);

            return result;
        }

        return new List<TDto>();
    }
}
