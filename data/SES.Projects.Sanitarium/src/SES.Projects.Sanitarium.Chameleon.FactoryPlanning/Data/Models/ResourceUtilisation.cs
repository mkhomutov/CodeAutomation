namespace SES.Projects.Sanitarium.Chameleon.FactoryPlanning
{
    using System;

    public class ResourceUtilisation
    {
        public string Location { get; set; }
        public string Resource { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime StartOfWeekDate{ get; set; }
        public int? Duration { get; set; }
        public double? TotalCapacity { get; set; }
        public double? TotalLoad { get; set; }
        public double? AvailableCapactiy { get; set; }
        public double? PercentCapacityUsed { get; set; }
        public double? RegularCapacity { get; set; }
        public double? RegularLoad { get; set; }
        public double? OvertimeCapacity { get; set; }
        public double? OvertimeLoad { get; set; }

        public override string ToString()
        {
            return string.Empty;
            //return $"Resource: {Resource}, StartTime: {StartTimeNumber}, FinishTime: {FinishTimeNumber}, Id: {Id}";
        }
    }
}
