using AdjusterMatching.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdjusterMatching.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdjusterMatchingController : ControllerBase
    {

        private readonly ILogger<AdjusterMatchingController> _logger;
        private readonly IEnumerable<Adjuster> _adjusters;
        public AdjusterMatchingController(ILogger<AdjusterMatchingController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _adjusters = GetAvailableAdjusters().Result;

        }

        [HttpPost]
        public async Task<AdjAssignment?> AdjusterMatch(Claim claim)
        {
            var adjusterMatched = _adjusters.Where(x => x.Department == claim.ClaimGroupId).Select(y => new AdjAssignment
            {
                AdjusterName = y?.AdjusterName ?? string.Empty,
                Id = Guid.NewGuid().ToString(),
                AdjuterId = y?.AdjuterId ?? string.Empty,
                ClaimGroupId = claim?.ClaimGroupId ?? string.Empty,
                ClaimId = claim?.ClaimId ?? string.Empty
            }).FirstOrDefault();

            return await Task.FromResult(adjusterMatched);

        }

        private async Task<IEnumerable<Adjuster>> GetAvailableAdjusters()
        {
            var adjusters = new List<Adjuster>
            {
                new Adjuster
                {
                    AdjusterName = "Martin Flower",
                    AdjuterId = Guid.NewGuid().ToString(),
                    Availability = true,
                    Department = "Auto"
                },
                new Adjuster
                {
                    AdjusterName = "Brian Adams",
                    AdjuterId = Guid.NewGuid().ToString(),
                    Availability = true,
                    Department = "Property"
                },
                new Adjuster
                {
                    AdjusterName = "Hari Nattuva",
                    AdjuterId = Guid.NewGuid().ToString(),
                    Availability = true,
                    Department = "Health"
                },
                new Adjuster()
                {
                    AdjusterName = "Diana Adams",
                    AdjuterId = Guid.NewGuid().ToString(),
                    Availability = true,
                    Department = "Auto"
                },
                new Adjuster()
                {
                    AdjusterName = "Eric Peter",
                    AdjuterId = Guid.NewGuid().ToString(),
                    Department = "Property",
                    Availability = true
                },
                new Adjuster()
                {
                    AdjusterName = "Ian juliet",
                    AdjuterId = Guid.NewGuid().ToString(),
                    Department = "Health",
                    Availability = true
                }
            };
            return await Task.FromResult(adjusters);
        }
    }
}