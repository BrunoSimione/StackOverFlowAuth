using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StackOverFlowAuth.Models;
using System.Data.Entity;
using System.Web.Security;

namespace StackOverFlowAuth.Controllers
{
    public class QuestionsController : Controller
    {
        // GET: Questions
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(int? id)
        {
            ViewBag.Message = "Your questions page.";
            using (QuestionAnswerContext context = new QuestionAnswerContext())
            {
                List<Question> list;
                if (id != null)
                {
                    if (id == 0)
                    {
                        list = context.Questions
                        .OrderBy(x => x.CreationDate)
                        .Include(x => x.User)
                        .Include(x => x.Category)
                        .Include(x => x.Votes)
                        .ToList();
                    }
                    else
                    {

                        list = context.Questions
                        .OrderBy(x => x.CreationDate)
                        .Include(x => x.User)
                        .Include(x => x.Category)
                        .Include(x => x.Votes)
                        .Where(x => x.Category.Id == id)
                        .ToList();
                    }
                }
                else
                {
                    list = context.Questions
                    .OrderBy(x => x.CreationDate)
                    .Include(x => x.User)
                    .Include(x => x.Category)
                    .Include(x => x.Votes)
                    .ToList();
                }

                return View(list);
            }
        }

        [Authorize]
        public ActionResult Create()
        {
            using (QuestionAnswerContext context = new QuestionAnswerContext())
            {
                var categories = context.Categories.ToList();
                QuestionCategories qc = new QuestionCategories();
                qc.Categories = categories;
                return View(qc);
            }
        }

        // POST: Test/Create
        [HttpPost]
        [Authorize]
        public ActionResult Create(QuestionCategories questionCat)
        {
            try
            {
                var question = questionCat.Question;

                using (QuestionAnswerContext context = new QuestionAnswerContext())
                {
                    question.Category = context.Categories.Find(question.Category.Id);
                    question.User = context.Users.Find(1);

                    question.CreationDate = DateTime.Now;

                    question.AnswerCount = 0;
                    question.ViewCount = 0;
                    question.Votes = null;

                    context.Questions.Add(question);
                    context.SaveChanges();

                    return RedirectToAction("Questions", "Home", null);
                }
            }
            catch
            {
                using (QuestionAnswerContext context = new QuestionAnswerContext())
                {
                    var categories = context.Categories.ToList();
                    QuestionCategories qc = new QuestionCategories();
                    qc.Categories = categories;
                    return View(qc);
                }
            }
        }


        public ActionResult AnswerQuestion(int id)
        {
            using (QuestionAnswerContext context = new QuestionAnswerContext())
            {

                var question = context.Questions
                        .Where(q => q.Id == id)
                        .Include(q => q.Category)
                        .Include(q => q.User)
                        .Include(q => q.Answers)
                        .Include(q => q.Votes)
                        .Include(q => q.Answers.Select(lp => lp.User))
                        .First();

                question.ViewCount += 1;
                context.SaveChanges();


                var questionanswermodel = new QuestionAnswer();
                questionanswermodel.Question = question;


                var user = context.Users
                        .Where(q => q.Email == User.Identity.Name)
                        .FirstOrDefault();
                
                int userid;
                if(user == null)
                {
                    userid = 0;
                }
                else
                {
                    userid = user.Id;
                }

                ViewBag.ID = userid;

                return View(questionanswermodel);
            }

        }

        [Authorize]
        [HttpPost]
        public ActionResult AnswerQuestion(QuestionAnswer questionAnswermodel)
        {
            using (QuestionAnswerContext context = new QuestionAnswerContext())
            {
                var questionId = questionAnswermodel.Question.Id;
                var newAnswer = questionAnswermodel.Answer;

                var question = context.Questions.Find(questionId);
                newAnswer.Question = question;

                //question.AnswerCount += 1;
                context.Entry(question).State = EntityState.Modified;

                var username = User.Identity.Name;

                var user = context.Users.Where(x => x.Email == username).FirstOrDefault();
                newAnswer.User = user;
                newAnswer.CreationDate = DateTime.Now;

                context.Answers.Add(newAnswer);
                context.SaveChanges();

                var updatedquestion = context.Questions
                        //.OrderBy(q => q.CreationDate)
                        .Where(q => q.Id == questionId)
                        .Include(q => q.Category)
                        .Include(q => q.User)
                        .Include(q => q.Answers)
                        .Include(q => q.Votes)
                        .Include(q => q.Answers.Select(lp => lp.User))
                        .First();

                var questionanswermodel = new QuestionAnswer();
                questionanswermodel.Question = updatedquestion;
                
                var userid = context.Users
                        .Where(q => q.Email == User.Identity.Name)
                        .First();

                ViewBag.ID = userid.Id;

                return View(questionanswermodel);
            }

        }

        [Authorize]
        [HttpPost]
        public ActionResult QuestionVote(QuestionAnswer questionAnswermodel)
        {
            using (QuestionAnswerContext context = new QuestionAnswerContext())
            {
                var questionId = questionAnswermodel.Question.Id;
                var question = context.Questions.Where(q => q.Id == questionId)
                        .Include(q => q.Category)
                        .Include(q => q.User)
                        .Include(q => q.Answers)
                        .Include(q => q.Votes)
                        .Include(q => q.Answers.Select(lp => lp.User))
                        .First();

                var user = question.Votes.Where(x => x.User.Id == question.User.Id).FirstOrDefault();

                if (user == null)
                {
                    String submit = Request.Form["submit"];
                    switch (submit)
                    {
                        case "upvote":
                            Vote newVote = new Vote();
                            newVote.CreationDate = DateTime.Now;
                            newVote.User = context.Users.Find(question.User.Id);
                            newVote.Value = +1;
                            question.VoteCount += 1;
                            question.Votes.Add(newVote);

                            context.SaveChanges();
                            break;
                        case "downvote":
                            Vote newVote2 = new Vote();
                            newVote2.CreationDate = DateTime.Now;
                            newVote2.User = context.Users.Find(question.User.Id);
                            newVote2.Value = -1;
                            question.VoteCount -= 1;
                            question.Votes.Add(newVote2);

                            context.SaveChanges();
                            break;
                        default:
                            //
                            break;
                    }
                }

                
                var updatedquestion = context.Questions
                        .OrderBy(q => q.CreationDate)
                        .Where(q => q.Id == questionId)
                        .Include(q => q.Category)
                        .Include(q => q.User)
                        .Include(q => q.Answers)
                        .Include(q => q.Votes)
                        .Include(q => q.Answers.Select(lp => lp.User))
                        .First();

                var questionanswermodel = new QuestionAnswer();
                questionanswermodel.Question = updatedquestion;

                return View("AnswerQuestion", questionanswermodel);
            }

        }

        [Authorize]
        public ActionResult EditAnswer(int id)
        {
            using (QuestionAnswerContext context = new QuestionAnswerContext())
            {

                var answer = context.Answers
                    .Find(id);

                context.Entry(answer).Reference(x => x.Question).Load();
                context.Entry(answer).Reference(x => x.User).Load();

                return View(answer);
            }

        }

        [Authorize]
        [HttpPost]
        public ActionResult EditAnswer(Answer answer)
        {
            using (QuestionAnswerContext context = new QuestionAnswerContext())
            {

                answer.CreationDate = DateTime.Now;
                context.Entry(answer).State = EntityState.Modified;
                context.SaveChanges();

                context.Entry(answer).Reference(x => x.Question).Load();

                //return View(answer);
                return RedirectToAction("AnswerQuestion", "Questions", new { id = answer.Question.Id });
            }

        }
    }
}