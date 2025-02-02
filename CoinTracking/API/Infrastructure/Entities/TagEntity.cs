﻿using API.Infrastructure.Entities.Common;

namespace API.Infrastructure.Entities
{
    public class TagEntity : ISoftEntity
    {
        public TagEntity()
        {
            Blogs = new List<BlogEntity>();
        }

        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public virtual ICollection<BlogEntity> Blogs { get; set; }

        public bool Deleted { get; set; }
    }
}
