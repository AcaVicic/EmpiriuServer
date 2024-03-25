using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    /// <summary>
    /// Class <c>EmpiriuController</c> represents service class which handles API requests. 
    /// </summary>
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmpiriuController : ControllerBase
    {
        /// <summary>
        /// <value> 
        /// Property <c>context</c> represents the instance which communicates with Empiriu database.
        /// </value>
        /// </summary>
        private EmpiriuContext context;

        /// <summary>
        /// This costructor initializes the new context.
        /// </summary>
        public EmpiriuController()
        {
            this.context = new EmpiriuContext();
        }

        /// <summary>
        /// This method returns daily journal of a given date written by the given user.
        /// </summary>
        /// <param name="userId">the id of daily journal's author</param>
        /// <param name="strDate">the string representation of date of daily journal's creation</param>
        /// <returns>Daily journal of a given date written by the given user.</returns>
        // GET api/<EmpiriuController>/5
        [HttpGet("Journal/{userId}/{strDate}")]
        public DailyJournal GetJournal(int userId, string strDate)
        {
            try
            {
                string[] s = strDate.Split('.');
                List<DailyJournal> list = context.DailyJournals!.Include(j => j.User).ToList();
                DailyJournal dj = list.First(j => j.Date.Day == Int32.Parse(s[0]) && j.Date.Month == Int32.Parse(s[1]) && j.Date.Year == Int32.Parse(s[2]) && j.User!.Id == userId);
                return dj;
            }
            catch (Exception)
            {
                return new DailyJournal() { Text = "" , User = new User()};
            }
            
        }

        [HttpGet("Journal/{userId}")]
        public async Task<List<DailyJournal>> GetAllJournals(int userId)
        {
            var journals = await context.DailyJournals!.Where(j => j.User!.Id == userId).OrderBy(j => j.Date).ToListAsync();
            return journals;
        }

        /// <summary>
        /// This method returns today's quote.
        /// </summary>
        /// <param name="id">the id of today's quote</param>
        /// <returns>Today's quote.</returns>
        // GET api/<EmpiriuController>/5
        [HttpGet("Quote/{id}")]
        public Quote GetQuote(int id)
        {
            Quote quote = context.Quotes!.First(q => q.Id == id);
            return quote;
        }

        /// <summary>
        /// This method returns user which uses given email.
        /// </summary>
        /// <param name="email">the email of given user</param>
        /// <returns>User which uses given email.</returns>
        [HttpGet("User/{email}")]
        public User GetUser(string email)
        {
            User user = context.Users!.First(u => u.Email == email);
            return user;
        }

        /// <summary>
        /// This method inserts daily journal in the Empiriu database.
        /// </summary>
        /// <param name="journal">the daily journal to be inserted</param>
        // POST api/<EmpiriuController>
        [HttpPost("Journal")]
        public void PostDailyJournal([FromBody] DailyJournal journal)
        {
            journal.User = context.Users!.First(u => u.Id == journal.User!.Id);
            context.DailyJournals!.Add(journal);
            context.SaveChanges();
        }

        /// <summary>
        /// This method updates daily journal in the Empiriu database.
        /// </summary>
        /// <param name="journal">the daily journal to be updated</param>
        // PUT api/<EmpiriuController>/5
        [HttpPut("Journal/{id}")]
        public void PutDailyJournal([FromBody] DailyJournal journal)
        {
            journal.User = context.Users!.First(u => u.Id == journal.User!.Id);
            context.DailyJournals!.Update(journal);
            context.SaveChanges();
        }

        /// <summary>
        /// This method deletes daily journal from the Empiriu database.
        /// </summary>
        /// <param name="journal">the daily journal to be deleted</param>
        // DELETE api/<EmpiriuController>/5
        [HttpDelete("Journal/{id}")]
        public void DeleteDailyJournal(int id)
        {
            DailyJournal journal = context.DailyJournals!.First(j => j.Id == id);
            context.DailyJournals!.Remove(journal);
            context.SaveChanges();
        }

        [HttpGet("MementoMori/{userId}")]
        public async Task<MementoMori> GetMementoMori(int userId)
        {
            var mementoMori = await context.MementoMori!.Where(j => j.User!.Id == userId).FirstAsync();
            return mementoMori;
        }

        [HttpGet("SaveMementoMori/{userId}")]
        public async Task SaveMementoMori(int userId)
        {
            var mementoMori = await context.MementoMori!.Where(j => j.User!.Id == userId).FirstAsync();
            mementoMori.FilledNumber += 1;
            mementoMori.LastDate = DateTime.UtcNow;
            await context.SaveChangesAsync();
        }
    }
}
