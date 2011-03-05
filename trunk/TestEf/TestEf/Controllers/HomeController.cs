using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestEf.Models;

using System.Data.Objects;


using DynamicJson.Extensions;

namespace TestEf.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult ListUsingStronglyTypedModel()
        {
            using (var db = new EntContext())
            {
                var r = (from p in db.Persons
                         join q in db.Qualifieds on p.PersonId equals q.PersonId into pQ
                         from l in pQ.DefaultIfEmpty()
                         orderby p.Lastname
                         select new QualifiedStatus { Person = p, IsQualified = l != null })
                        .Skip(0).Take(10) // do the paging logic here
                        .ToList();

                return View(r);
            }
        }


        public ActionResult ListUsingDynamicModelAssistedByJson()
        {
            using (var db = new EntContext())
            {
                var r = (from p in db.Persons
                         join q in db.Qualifieds on p.PersonId equals q.PersonId into pQ
                         from l in pQ.DefaultIfEmpty()
                         orderby p.Lastname
                         select new { Person = p, IsQualified = l != null })
                        .Skip(0).Take(10) // do the paging logic here
                        .ToList(); 

                
                return View((object)r.JsSerialize());
            }
        }


    }
}



/*
    Generated query when traced on Sql Server Profiler (let's hope that in future version of Sql Server that it will support LIMIT and OFFSET clause): 

    SELECT TOP (10) 
    [Project1].[FavoriteNumber] AS [FavoriteNumber], 
    [Project1].[PersonId] AS [PersonId], 
    [Project1].[Lastname] AS [Lastname], 
    [Project1].[Firstname] AS [Firstname], 
    [Project1].[C1] AS [C1]
    FROM ( SELECT [Project1].[PersonId] AS [PersonId], [Project1].[Lastname] AS [Lastname], [Project1].[Firstname] AS [Firstname], [Project1].[FavoriteNumber] AS [FavoriteNumber], [Project1].[C1] AS [C1], row_number() OVER (ORDER BY [Project1].[Lastname] ASC) AS [row_number]
        FROM ( SELECT 
            [Extent1].[PersonId] AS [PersonId], 
            [Extent1].[Lastname] AS [Lastname], 
            [Extent1].[Firstname] AS [Firstname], 
            [Extent1].[FavoriteNumber] AS [FavoriteNumber], 
            CASE WHEN ([Extent2].[QualifiedId] IS NOT NULL) THEN cast(1 as bit) WHEN ([Extent2].[QualifiedId] IS NULL) THEN cast(0 as bit) END AS [C1]
            FROM  [dbo].[Person] AS [Extent1]
            LEFT OUTER JOIN [dbo].[Qualified] AS [Extent2] ON [Extent1].[PersonId] = [Extent2].[PersonId]
        )  AS [Project1]
    )  AS [Project1]
    WHERE [Project1].[row_number] > 0
    ORDER BY [Project1].[Lastname] ASC
                     
    */