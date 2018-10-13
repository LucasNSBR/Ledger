using Ledger.Shared.Entities;
using System;
using System.Collections.Generic;

namespace Ledger.Blog.Domain.Aggregates.ArticleAggregate
{
    public class Article : Entity<Article>
    {
        public bool Active { get; private set; }
        public string Slug { get; private set; }
        public string Title { get; private set; }
        public string Body { get; private set; }
        public Guid CategoryId { get; private set; }
        
        //TenantId
        public Guid AuthorId { get; private set; }
        public DateTime DatePublished { get; private set; }

        private readonly List<Comment> _comments;
        public IReadOnlyList<Comment> Comments
        {
            get
            {
                return _comments;
            }
        }

        public Article(string slug, string title, string body, Guid categoryId, Guid authorId)
        {
            Active = true;
            DatePublished = DateTime.Now;

            Slug = slug;
            Title = title;
            Body = body;
            CategoryId = categoryId;
            AuthorId = authorId;

            _comments = new List<Comment>();
        }

        public Article(Guid id, string slug, string title, string body, Guid categoryId, Guid authorId)
        {
            Active = true;
            DatePublished = DateTime.Now;

            Id = id;
            Slug = slug;
            Title = title;
            Body = body;
            CategoryId = categoryId;
            AuthorId = authorId;

            _comments = new List<Comment>();
        }

        public void SetActive()
        {
            if (Active)
                AddNotification("Já ativo", "Não é possível completar a operação pois o artigo já está ativo");
            else
                Active = true;
        }

        public void SetInactive()
        {
            if (!Active)
                AddNotification("Já está inativo", "Não é possível completar a operação pois o artigo já está inativo");
            else
                Active = false;
        }

        public void AddComment(Comment comment)
        {
            _comments.Add(comment);
        }

        public void RemoveComment(Comment comment)
        {
            _comments.Remove(comment);
        }
    }
}
