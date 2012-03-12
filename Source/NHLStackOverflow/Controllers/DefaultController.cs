﻿using System.Linq;
using System.Web.Mvc;
using NHLStackOverflow.Models;
using NHLStackOverflow.Classes;
using System;

namespace NHLStackOverflow.Controllers
{ 
    public class DefaultController : Controller
    {
        private NHLdb db = new NHLdb();

        //
        // GET: /

        public ViewResult Index()
        {
            var TagsList = from tags in db.Tags
                           orderby tags.Count descending
                           select tags;
            ViewBag.TagList = TagsList;

            var QuestionList = from questions in db.Questions
                               orderby questions.Created_At descending
                               select questions;
            foreach (Question vraag in QuestionList)
            {
                vraag.Created_At = StringToDateTime.toSmootherTime(StringToDateTime.toDateTime(vraag.Created_At));
            }
 
            ViewBag.QuestionList = QuestionList;

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}