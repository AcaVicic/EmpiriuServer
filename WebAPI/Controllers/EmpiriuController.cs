using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmpiriuController : ControllerBase
    {

        private EmpiriuContext context;

        public EmpiriuController()
        {
            this.context = new EmpiriuContext();
        }

        // GET api/<EmpiriuController>/5
        [HttpGet("Journal/{userId}/{strDate}")]
        public DailyJournal GetJournal(int userId, string strDate)
        {
            try
            {
                string[] s = strDate.Split('.');
                List<DailyJournal> list = context.DailyJournals.Include(j => j.User).ToList();
                DailyJournal dj = list.First(j => j.Date.Day == Int32.Parse(s[0]) && j.Date.Month == Int32.Parse(s[1]) && j.Date.Year == Int32.Parse(s[2]) && j.User.Id == userId);
                return dj;
            }
            catch (Exception)
            {
                return new DailyJournal() { Text = "" , User = new User()};
            }
            
        }

        // GET api/<EmpiriuController>/5
        [HttpGet("Quote/{id}")]
        public Quote GetQuote(int id)
        {
            Quote quote = context.Quotes.Find(id);
            return quote;
        }

        [HttpGet("User/{email}")]
        public User GetUser(string email)
        {
            User user = context.Users.First(u => u.Email == email);
            return user;
        }

        // POST api/<EmpiriuController>
        [HttpPost("Journal")]
        public void PostDailyJournal([FromBody] DailyJournal journal)
        {
            journal.User = context.Users.Find(journal.User.Id);
            context.DailyJournals.Add(journal);
            context.SaveChanges();
        }

        // PUT api/<EmpiriuController>/5
        [HttpPut("Journal/{id}")]
        public void PutDailyJournal(int id, [FromBody] DailyJournal journal)
        {
            journal.User = context.Users.Find(journal.User.Id);
            context.DailyJournals.Update(journal);
            context.SaveChanges();
        }

        // DELETE api/<EmpiriuController>/5
        [HttpDelete("Journal/{id}")]
        public void DeleteDailyJournal(int id)
        {
            DailyJournal journal = context.DailyJournals.Find(id);
            context.DailyJournals.Remove(journal);
            context.SaveChanges();
        }
    }
}
