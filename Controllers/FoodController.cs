using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Foods.Models;

namespace Foods.Controllers;

public class FoodController : Controller
{
    private readonly ILogger<FoodController> _logger;
    private MyContext _context;
    public FoodController(ILogger<FoodController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("foods/new")]
    public ViewResult NewFood()
    {
        return View();
    }


    [HttpPost("foods/create")]
    public IActionResult CreateFood(Food newFood)
    {
        if (!ModelState.IsValid)
        {
            return View("NewFood");
        }
        _context.Add(newFood);
        _context.SaveChanges();
        return RedirectToAction("Index", "Home");
    }

    [HttpGet("")]
    public ViewResult AllFoods()
    {
        List<Food> Foods = _context.Foods.OrderByDescending(p => p.CreatedAt).ToList();
        return View(Foods);
    }

    [HttpGet("foods/{foodId}")]
    public IActionResult ViewFood(int foodId)
    {
        Food? SingleFood = _context.Foods.FirstOrDefault(p => p.FoodId == foodId);
        if (SingleFood == null)
        {
            return RedirectToAction("AllFoods");
        }
        return View(SingleFood);
    }

    [HttpGet("foods/{foodId}/edit")]
    public IActionResult EditFood(int foodId)
    {
        Food? ToBeEdited = _context.Foods.FirstOrDefault(p => p.FoodId == foodId);
        if (ToBeEdited == null)
        {
            return RedirectToAction("AllFoods");
        }
        return View(ToBeEdited);
    }

    [HttpPost("foods/{foodId}/update")]
    public IActionResult UpdateFood(int foodId, Food editedFood)
    {
        Food? ToBeUpdated = _context.Foods.FirstOrDefault(p => p.FoodId == foodId);
        if(!ModelState.IsValid || ToBeUpdated == null)
        {
            return View("EditFood", ToBeUpdated);
        }
        ToBeUpdated.Dish = editedFood.Dish;
        ToBeUpdated.Chef = editedFood.Chef;
        ToBeUpdated.Tasty = editedFood.Tasty;
        ToBeUpdated.Cals = editedFood.Cals;
        ToBeUpdated.Description = editedFood.Description;
        ToBeUpdated.UpdatedAt = DateTime.Now;
        _context.SaveChanges();

        return RedirectToAction("ViewFood", new{foodId = foodId});
    } 

    [HttpPost("foods/{foodId}/delete")]
    public IActionResult DeleteFood(int foodId)
    {
        Food? ToBeDeleted = _context.Foods.SingleOrDefault(p => p.FoodId == foodId);
        if (ToBeDeleted != null)
        {
            _context.Remove(ToBeDeleted);
            _context.SaveChanges();
        }
        return RedirectToAction("AllFoods");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
