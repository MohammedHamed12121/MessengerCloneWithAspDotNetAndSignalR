using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RealTimeChatApp.Data;
using RealTimeChatApp.Interfaces;
using RealTimeChatApp.Models;
using RealTimeChatApp.ViewModels;

namespace RealTimeChatApp.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ApplicationDbContext _context;
    private readonly IMessageRepository _messageRepo;

    public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, IMessageRepository messageRepo, ApplicationDbContext context)
    {
        _logger = logger;
        _userManager = userManager;
        _messageRepo = messageRepo;
        _context = context;
    }

    public async Task<IActionResult> Index(string id)
    {
        // get current user id
        var userId = _userManager.GetUserId(User);
        var sender = _context.Users.Where(u => u.Id == userId).FirstOrDefault();

        if(id == null)
        {
            id = userId;
        }
        // get the other user id
        var receiver = _context.Users.Where(u => u.Id == id).FirstOrDefault();
        var receiverId = receiver.Id;

        // get the messages 
        var messages = await _messageRepo.GetAllAsync(userId, receiverId);
        
        var indexVM = new IndexViewModel(){
            ToUser = receiver,
            FromUser = sender,
            Messages = messages
        };
        
        return View(indexVM);
    }

    public IActionResult UserCount()
    {
        return View();
    }
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
