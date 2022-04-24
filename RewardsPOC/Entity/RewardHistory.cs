using System;

namespace RewardsPOC.Entity
{
    public class RewardHistory
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int RewardTypeId { get; set; }
        public DateTime AddedOn { get; set; }
        public int AwardedBy { get; set; }

    }
}
