using Microsoft.AspNetCore.Mvc;
using CRUEDelicious.Models;

namespace CRUEDelicious.Controllers;

public class DishController : Controller
{
    private MyContext _context;

    public DishController(MyContext context)
    {
        _context = context;
    }

    [HttpGet("/")]
    public IActionResult All()
    {
        List<Dish> AllDishes = _context.Dishes.OrderByDescending(d => d.CreatedAt).ToList();

        return View("All", AllDishes);
    }

    [HttpGet("/new")]
    public IActionResult New()
    {
        return View("NewDish");
    }

    [HttpPost("/post/create")]
    public IActionResult Create(Dish newDish)
    {
        if (ModelState.IsValid == false)
        {
            return New();
        }
        _context.Dishes.Add(newDish);
        _context.SaveChanges();

        return RedirectToAction("All");
    }

    [HttpGet("/{dishId}")]
    public IActionResult ViewOne(int dishId)
    {
        Dish? dish = _context.Dishes.FirstOrDefault(dish => dish.DishId == dishId);

        if (dish == null)
        {
            return RedirectToAction("All");
        }
        return View("ViewOne", dish);
    }

    [HttpGet("/edit/{dishId}")]
    public IActionResult Edit(int dishId)
    {
        Dish? dish = _context.Dishes.FirstOrDefault(dish => dish.DishId == dishId);

        if (dish == null)
        {
            return RedirectToAction("All");
        }
        return View("Edit", dish);
    }

    [HttpPost("/edit/{dishId}")]
    public IActionResult UpdateOne(int dishId, Dish dish)
    {
        if (ModelState.IsValid == false)
        {
            return Edit(dishId);
        }
        Dish? dbDish = _context.Dishes.FirstOrDefault(dish => dish.DishId == dishId);

        if (dbDish == null)
        {
            return RedirectToAction("All");
        }

        dbDish.Name = dish.Name;
        dbDish.Chef = dish.Chef;
        dbDish.Tastiness = dish.Tastiness;
        dbDish.Calories = dish.Calories;
        dbDish.Description = dish.Description;
        dbDish.UpdatedAt = DateTime.Now;

        _context.Dishes.Update(dbDish);
        _context.SaveChanges();

        return RedirectToAction("All", new { postId = dbDish });
    }


    [HttpPost("{deletedDishId}")]
    public IActionResult DeleteOne(int deletedDishId)
    {
        Dish? dishToBeDelete = _context.Dishes.FirstOrDefault(d => d.DishId == deletedDishId);


        if (dishToBeDelete != null)
        {
            _context.Dishes.Remove(dishToBeDelete);
            _context.SaveChanges();
            Console.WriteLine(dishToBeDelete);
        }
        return RedirectToAction("All");
    }


}
