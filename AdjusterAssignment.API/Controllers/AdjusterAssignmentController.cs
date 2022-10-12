using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using AdjusterAssignment.API.Data;
using AdjusterAssignment.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdjusterAssignment.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdjusterAssignmentController
    {
        private readonly AdjusterAssignmentContext _context;
        private readonly ILogger<AdjusterAssignmentController> _logger;

        public AdjusterAssignmentController(AdjusterAssignmentContext context, ILogger<AdjusterAssignmentController> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<IEnumerable<AdjAssignment>> Get()
        {
            return await _context
                            .AdjusterAssignments
                            .Find(p => true)
                            .ToListAsync();
        }

        [HttpPost]
        public  async Task CreateAsync(AdjAssignment adjAssignment)
        {
             await _context.InsertOneAsync(adjAssignment);  
        }

        [HttpPut("{id:length(24)}")]
        public async Task UpdateAsync(string id, AdjAssignment adjAssignment)
        {
            await _context.UpdateAsync(id, adjAssignment);
        }

        [HttpDelete("{id:length(24)}")]
        public async Task DeleteAsync(string id)
        {
            await _context.DeleteAsync(id);
        }
    }
}
