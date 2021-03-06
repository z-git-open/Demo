﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CountingKs.Data;
using CountingKs.Data.Entities;
using CountingKs.Models;

namespace CountingKs.Controllers
{
  public class FoodsController : ApiController
  {
    ICountingKsRepository _repo;
    ModelFactory _modelFactory;
    
    public FoodsController(ICountingKsRepository repo)
    {
      _repo = repo;
      _modelFactory = new ModelFactory();
    }

    public IEnumerable<FoodModel> Get(bool includeMeasures = true)
    {
      IQueryable<Food> query;

      if (includeMeasures)
      {
        query = _repo.GetAllFoodsWithMeasures();
      }
      else
      {
        query = _repo.GetAllFoods();
      }
      
      var results = query.OrderBy(f => f.Description)
                         .Take(25)
                         .ToList()
                         .Select(f => _modelFactory.Create(f));

      return results;
    }

    public FoodModel Get(int foodid)
    {
      return _modelFactory.Create(_repo.GetFood(foodid));
    }
  }
}
