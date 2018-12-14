namespace StackOverFlowAuth.Migrations.QuestionAnswerContext
{
    using StackOverFlowAuth.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<StackOverFlowAuth.Models.QuestionAnswerContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\QuestionAnswerContext";
        }

        protected override void Seed(StackOverFlowAuth.Models.QuestionAnswerContext context)
        {
            context.Categories.AddOrUpdate(x => x.Id,
                new Category() { Id = 1, Name = "C#" },
                new Category() { Id = 2, Name = "Java" },
                new Category() { Id = 3, Name = "Asp.Net" }
                );
            
            context.Users.AddOrUpdate(x => x.Id,
                new User() { Id = 1, Name = "John", Email = "john@john.com" },
                new User() { Id = 2, Name = "Mary", Email = "mary@mary.com" },
                new User() { Id = 3, Name = "Mark", Email = "mark@mark.com" }
                );

            context.Votes.AddOrUpdate(x => x.Id,
                new Vote() { Id = 1, Value = +1, CreationDate = DateTime.Now , User = context.Users.Find(1) },
                new Vote() { Id = 2, Value = -1, CreationDate = DateTime.Now , User = context.Users.Find(1) }
                );

            context.Questions.AddOrUpdate(x => x.Id,
                new Question() { Id = 1, QuestionName = "How can I do XYZ?", CreationDate = DateTime.Now, ViewCount = 3, AnswerCount = 2, VoteCount = 1, Votes = new System.Collections.Generic.List<Vote>(), User = context.Users.Find(1), Category = context.Categories.Find(1) },
                new Question() { Id = 2, QuestionName = "Is it possible to do ABC?", CreationDate = DateTime.Now, ViewCount = 2, AnswerCount = 1, VoteCount = -1, Votes = new System.Collections.Generic.List<Vote>(), User = context.Users.Find(1), Category = context.Categories.Find(2) },
                new Question() { Id = 3, QuestionName = "Is it possible to do ABCXYZ?", CreationDate = DateTime.Now, ViewCount = 5, AnswerCount = 0, VoteCount = 0, Votes = new System.Collections.Generic.List<Vote>(), User = context.Users.Find(2), Category = context.Categories.Find(1) }
                );

            context.Questions.Find(1).Votes.Add(context.Votes.Find(1));
            context.Questions.Find(2).Votes.Add(context.Votes.Find(2));

            context.Answers.AddOrUpdate(x => x.Id,
                new Answer() { Id = 1, CreationDate = DateTime.Now, Text = "You should do qwerty", Question = context.Questions.Find(1), User = context.Users.Find(2) },
                new Answer() { Id = 2, CreationDate = DateTime.Now, Text = "You should do qwertyzxc", Question = context.Questions.Find(1), User = context.Users.Find(1) },
                new Answer() { Id = 3, CreationDate = DateTime.Now, Text = "You should do abc", Question = context.Questions.Find(2), User = context.Users.Find(3) }
                );
        }
    }
}
