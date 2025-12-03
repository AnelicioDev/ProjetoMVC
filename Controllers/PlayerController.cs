using System;
using Microsoft.AspNetCore.Mvc;
using ProjetoMVC.Context;
using ProjetoMVC.Models;

namespace ProjetoMVC.Controllers;

public class PlayerController : Controller
{
    private readonly AppDbContext _context;

    public PlayerController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var players = _context.Soccer.ToList();
        return View(players);
    }

    public IActionResult Criar()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Criar(Player player)
    {
        if (ModelState.IsValid)
        {
            _context.Soccer.Add(player);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        else
        {
            return View(player);   
        }
    }

    public IActionResult Editar(int id)
    {
        var playerId = _context.Soccer.Find(id);

        if (playerId == null)
        {
            return NotFound();
        }

        return View(playerId);
    }

    [HttpPost]
    public IActionResult Editar(Player player)
    {
        var playerDB = _context.Soccer.Find(player.id);
        playerDB.name = player.name;
        playerDB.position = player.position;

        _context.Soccer.Update(playerDB);
        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }
}
