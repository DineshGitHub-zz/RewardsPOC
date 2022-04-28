using RewardsPOC.Entity;
using System.Collections.Generic;

namespace RewardsPOC.Models
{
    public class RewardsViewModel
    {
        public int RewardPoints { get; set; }
        public List<Rewards> Rewards { get; set; }
    }
}
