using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BHFunctioning.Data;
using BHFunctioning.Models;

namespace BHFunctioning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Api
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HealthData>>> GetHealthData()
        {
            return await _context.HealthData.ToListAsync();
        }

        // GET: api/Api/5 
        // https://localhost:44327/api/entry?Medical=1&ChildDX=1&Selfharm=1&Sofas=6&ClinicalStage=1&Circadian=1&Tripartite=1&Psychosis=1&NEET=1 example
        [HttpGet("/api/entry")]
        public async Task<ActionResult<HealthDataRes>> GetHealthData([FromQuery]HealthDataInput input)
        {
            bool NEET, Selfharm, Psychosis, Medical, ChildDx, Circadian;
            if (input.NEET == 1) NEET = true;
            else NEET = false;

            if (input.Selfharm == 1) Selfharm = true;
            else Selfharm = false;

            if (input.Psychosis == 1) Psychosis = true;
            else Psychosis = false;

            if (input.Medical == 1) Medical = true;
            else Medical = false;

            if (input.ChildDx == 1) ChildDx = true;
            else ChildDx = false;

            if (input.Circadian == 1) Circadian = true;
            else Circadian = false;

            var healthData = _context.HealthData.FirstOrDefault(a =>
                a.Medical == Medical &&
                a.ChildDx == ChildDx &&
                a.Selfharm == Selfharm &&
                a.Sofas == input.Sofas &&
                a.ClinicalStage == input.ClinicalStage &&
                a.Circadian == Circadian &&
                a.Tripartite == input.Tripartite &&
                a.Psychosis == Psychosis &&
                a.NEET == NEET
                );

            if (healthData == null)
            {
                return NotFound();
            }
            HealthDataRes temp = new HealthDataRes();
            temp.Constant = healthData.Constant;
            temp.Mean = healthData.Mean;
            temp.StandardDeviation = healthData.StandardDeviation;
            temp.Threshold_50 = healthData.Threshold_50;
            temp.Threshold_60 = healthData.Threshold_60;
            temp.Threshold_70 = healthData.Threshold_70;
            return temp;
        }

        //    // PUT: api/Api/5
        //    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //    [HttpPut("{id}")]
        //    public async Task<IActionResult> PutHealthData(string id, HealthData healthData)
        //    {
        //        if (id != healthData.Id)
        //        {
        //            return BadRequest();
        //        }

        //        _context.Entry(healthData).State = EntityState.Modified;

        //        try
        //        {
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!HealthDataExists(id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }

        //        return NoContent();
        //    }

        //    // POST: api/Api
        //    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //    [HttpPost]
        //    public async Task<ActionResult<HealthData>> PostHealthData(HealthData healthData)
        //    {
        //        _context.HealthData.Add(healthData);
        //        try
        //        {
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateException)
        //        {
        //            if (HealthDataExists(healthData.Id))
        //            {
        //                return Conflict();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }

        //        return CreatedAtAction("GetHealthData", new { id = healthData.Id }, healthData);
        //    }

        //    // DELETE: api/Api/5
        //    [HttpDelete("{id}")]
        //    public async Task<IActionResult> DeleteHealthData(string id)
        //    {
        //        var healthData = await _context.HealthData.FindAsync(id);
        //        if (healthData == null)
        //        {
        //            return NotFound();
        //        }

        //        _context.HealthData.Remove(healthData);
        //        await _context.SaveChangesAsync();

        //        return NoContent();
        //    }

        private bool HealthDataExists(string id)
        {
            return _context.HealthData.Any(e => e.Id == id);
        }
    }
}
