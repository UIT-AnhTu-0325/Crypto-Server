﻿using API.Infrastructure;
using API.Infrastructure.Entities;
using API.Infrastructure.Entities.Common;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Blogs.Queries
{
    public class Get
    {
        public class Handler : IRequestHandler<Query, Response?>
        {
            private readonly MasterContext _context;
            private readonly IMapper _mapper;

            public Handler(MasterContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Response?> Handle(Query request, CancellationToken cancellationToken)
            {
                var item = await _context.Blogs
                    .Include(i => i.FollowUsers)
                    .Include(i => i.Author)
                    .Include(i => i.Tags)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

                var result = _mapper.Map<Response>(item);

                return result;
            }
        }

        public class Query : IRequest<Response?>
        {
            [FromRoute]
            public int Id { get; set; }
        }

        public class Response : BaseEntity
        {
            public int Id { get; set; }

            public string Header { get; set; } = string.Empty;

            public string Content { get; set; } = string.Empty;

            public string AuthorName { get; set; } = string.Empty;

            public Guid AuthorId { get; set; }

            public string? AuthorImageUrl { get; set; }

            public int TotalFollower { get; set; }

            public List<Tag> Tags { get; set; } = new();

            public bool Deleted { get; set; }

            public List<FollowUser> FollowUsers { get; set; } = new();

            public string? SubContent { get; set; }

            public string? ImageUrl { get; set; }

            public int Status { get; set; }
        }

        public class Tag
        {
            public int Id { get; set; }

            public string Title { get; set; } = string.Empty;
        }

        [AutoMap(typeof(UserEntity))]
        public class FollowUser
        {
            public Guid Id { get; set; }
        }
    }
}