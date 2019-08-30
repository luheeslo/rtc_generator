using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using rtc.Data.DAL;
using rtc.Data.Models;
using rtc.Data.Repositories;
using rtc.Web.Common.Resources;

namespace rtc.Web.Mvc.Controllers
{
    [Authorize(Roles = "Admin, User")]
    [Route("[controller]/[action]")]
    public class {{ model }}Controller : Controller
    {
        private readonly IRepository<{{ model }}> _{{ model|lower }}Repository;

        public {{ model }}Controller(IRepository<{{ model }}> productRepository)
        {
            _{{ model|lower }}Repository = productRepository;
        }

        // GET: {{ model }}
        public ActionResult Index()
        {
            return View(_{{ model|lower }}Repository.FindAll());
        }

        // GET: {{ model }}/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _{{ model|lower }}Repository.FindById(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: {{ model }}/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: {{ model }}/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("{{ attrs|join(',')}}")] {{ model }} product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _{{ model|lower }}Repository.Add(product);
                    _{{ model|lower }}Repository.Save();
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException is SqlException && e.InnerException.Message != null) {
                        if (e.InnerException.Message.Contains("duplicate key"))
                        {
                            ModelState.AddModelError("", Language.Duplicate{{ model }}Code);
                            return View(product);
                        }   
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: {{ model }}/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _{{ model|lower }}Repository.FindById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: {{ model }}/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(long id, [Bind("{{ attrs|join(',')}}")] {{ model }} product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _{{ model|lower }}Repository.Update(product);
                    _{{ model|lower }}Repository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!{{ model }}Exists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException is SqlException && e.InnerException.Message != null) {
                        if (e.InnerException.Message.Contains("duplicate key"))
                        {
                            ModelState.AddModelError("", Language.Duplicate{{ model }}Code);
                            return View(product);
                        }   
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: {{ model }}/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _{{ model|lower }}Repository.FindById(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: {{ model }}/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            var product = _{{ model|lower }}Repository.FindById(id);
            _{{ model|lower }}Repository.Remove(product);
            _{{ model|lower }}Repository.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool {{ model }}Exists(long id)
        {
            return this._{{ model|lower }}Repository.FindById(id) == null ? false : true;
        }
    }
}
