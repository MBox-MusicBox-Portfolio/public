using MBox.Models.Db;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MBox.Controllers
{
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger _logger;

        // GET: UserController
        //[HttpGet(Name = "GetRole")]
        //public IEnumerable<Role> Get()
        //{
        //    List<Role> roles = new List<Role>();

        //    // Генерируем 10 тестовых пользователей
        //    for (int i = 0; i < 3; i++)
        //    {
        //        Role role = new Role
        //        {
        //            Name = "Admin",
        //        };

        //        _context.Roles.Add(role);
        //        _context.SaveChanges();
        //    }
        //    return roles;
        //} 

        //// POST: UserController/Edit/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> Edit(Guid id, User newUser)
        //{
        //    try
        //    {
        //        var oldUser = _context.Users.FirstOrDefault(x => x.Id == id);
        //        if (oldUser == null) return NotFound();

        //        oldUser.Email = newUser.Email;
        //        //Other properties . . .


        //        _context.Users.Update(oldUser);
        //        await _context.SaveChangesAsync();

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogInformation(ex.Message);
        //        return View();
        //    }
        //}

        //// POST: UserController/Delete/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(Guid id)
        //{
        //    try
        //    {
        //        var user = await _context.Users.FindAsync(id);
        //        if (user == null) { return NotFound(); }

        //        _context.Users.Remove(user);
        //        await _context.SaveChangesAsync();

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogInformation(ex.Message);
        //        return View();
        //    }
        //}
    }
}
