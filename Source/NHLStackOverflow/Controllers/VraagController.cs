﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NHLStackOverflow.Classes;
using NHLStackOverflow.Models;

namespace NHLStackOverflow.Controllers
{
    public class VraagController : Controller
    {
        private NHLdb db = new NHLdb();
        //
        // GET: Vraag/View/detailNum
        public ActionResult View(int id)
        {
            var questionDetails = from questionDetail in db.Questions
                                  where questionDetail.QuestionID == id
                                  select questionDetail;
            ViewBag.QuestionDetail = questionDetails.First();
            return View();
        }

        //
        // GET: /Vraag/StelEen
        public ActionResult StelEen()
        {
            ViewBag.ActionName = "steleen";
            ViewBag.ControllerName = "vraag";
            return View();
        }

        //
        // POST: /Vraag/StelEen
        [HttpPost]
        public ActionResult StelEen(string vraag)
        {
            // For cleaing up the spaces
            char[] toTrim = new char[7] { '?', '.', ',', '!', ':', ':', '/' };
            string vraagTrimmed = vraag.Trim(toTrim);
            vraagTrimmed = vraagTrimmed.ToLower();

            List<Question> questionResultList = new List<Question>();
            // check the database for an existing somewhat simalair question
            string[] toSearch = StringFilter.Trim(vraagTrimmed);
            foreach (string a in toSearch)
            {
                var questionsFound = from question in db.Questions
                                     where question.Title.Contains(a)
                                     select question;
                foreach (Question abc in questionsFound)
                    questionResultList.Add(abc);
            }

            ViewBag.SearchResults = from question in questionResultList
                                      group question by question.QuestionID into qsorted
                                      orderby qsorted.Count() descending
                                      select qsorted.First() ;

            ViewBag.ActionName = "steleen2";
            ViewBag.ControllerName = "vraag";

            return View();
        }
    }
}
