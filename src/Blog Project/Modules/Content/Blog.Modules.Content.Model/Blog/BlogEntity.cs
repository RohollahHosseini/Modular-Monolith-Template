using Blog.BuildingBlocks.Model;
using Blog.BuildingBlocks.Model.BusinessRule.PublicRules;
using Blog.BuildingBlocks.Model.Result;
using Blog.Modules.Content.Model.Blog.Events;
using Blog.Modules.Content.Model.Blog.Rules;
using Blog.Modules.Content.Model.Category;
using Blog.Modules.Content.Model.ValueObjectes.Blog;
using System;
using System.Text;

namespace Blog.Modules.Content.Model.Blog
{
    public sealed class BlogEntity:BaseEntity<Guid>
    {

        private readonly List<LogValueObject> _changeLog;

        public string BlogTitle { get; private set; }
        public string BlogContent { get; private set; }
        public BlogState CurrentState { get; private set; }
        public string Slug { get; set; }
        public IReadOnlyList<LogValueObject> ChangeLogs => _changeLog.AsReadOnly();
        public Guid CategoryId { get; set; }
        public CategoryEntity Category { get; set; }
        public DomainResult ChangeStatus(BlogState state, string? additionalMessage = null)
        {

            CurrentState = state;

            this._changeLog.Add(new LogValueObject() {NetryDate=DateTime.UtcNow,Message= "Entity State is Changed",AdditionsDescription= $"{additionalMessage} BlogId:{Id}"});

            return DomainResult.None;
        }

        //private BlogEntity()
        //{

        //}
        public BlogEntity Create(string blogTitle, string blogContent, Guid? categoryId)
        {
            //ArgumentNullException.ThrowIfNull(blogTitle);
            //ArgumentNullException.ThrowIfNull(blogContent);
            //ArgumentNullException.ThrowIfNull(categoryId);

            CheckRule(new StrignIsNullOrEmptyRule(blogTitle));
            CheckRule(new StrignIsNullOrEmptyRule(blogContent));
            CheckRule(new GuidParameterValidationRule(categoryId));


            //Guard.Against.NullOrEmpty(userId, message: "Invalida UserId");

            //if (string.IsNullOrEmpty(userId.ToString()))    
            //     new ArgumentException($"Required input {userId} was empty.");
            //CheckRule(new BlogMustHaveValidUserIdRule(userId));



            var @blogEntity = new BlogEntity()
            {
                Id = Guid.NewGuid(),
                BlogTitle = blogTitle,
                BlogContent = blogContent,
                Slug = GenerateSlug(blogTitle),
                //UserId = userId.Value,
                CategoryId = categoryId.Value,
                CurrentState = BlogState.PendingReview,

            };

            @blogEntity._changeLog.Add(new LogValueObject() { NetryDate = DateTime.UtcNow, Message = $"Blog Created.",AdditionsDescription=$"Blog Id :{@blogEntity.Id}"});


            @blogEntity.Raise(new BlogCreatedDomainEvent(Id,BlogTitle));

            return @blogEntity;

        }
        public void Edit(string? blogTitle, string? blogContent, Guid? categoryId)
        {
            if (!string.IsNullOrEmpty(blogTitle))
                BlogTitle = blogTitle;

            if (!string.IsNullOrEmpty(blogContent))
                BlogContent = blogContent;

            //if (userId.HasValue && userId != Guid.Empty)
            //    UserId = userId.Value;

            if (categoryId.HasValue && categoryId != Guid.Empty)
                CategoryId = categoryId.Value;

            _changeLog.Add(new LogValueObject(){NetryDate=DateTime.UtcNow, Message="Blog Edited",AdditionsDescription= $"Blog Id :{Id}" });

            CurrentState = BlogState.PendingReview;
        }

        protected string GenerateSlug(string title)
        {
            //if (string.IsNullOrWhiteSpace(title))
            //    throw new ArgumentException("Title cannot be null or empty.");
            CheckRule(new StrignIsNullOrEmptyRule(title));

            string normalizedTitle = title.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var character in normalizedTitle)
            {
                if (char.IsLetterOrDigit(character) || character == ' ')
                {
                    stringBuilder.Append(character);
                }
            }

            return stringBuilder
                .ToString()
                .ToLower()
                .Trim()
                .Replace(" ", "-")
                .Replace(".", "")
            .Replace(",", "")
            .Replace("?", "")
            .Replace("!", "")
            .Replace(":", "")
            .Replace(";", "");
        }

    }
}
