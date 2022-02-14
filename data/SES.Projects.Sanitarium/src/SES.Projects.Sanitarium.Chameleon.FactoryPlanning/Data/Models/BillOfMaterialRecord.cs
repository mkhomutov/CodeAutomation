namespace SES.Projects.Sanitarium.Chameleon.FactoryPlanning
{
    using System;

    public class BillOfMaterialRecord : IEquatable<BillOfMaterialRecord>
    {
        public string ParentItemCode { get; set; }
        public Item ParentItem { get; set; }
        public string ChildItemCode { get; set; }
        public Item ChildItem { get; set; }
        public string Location { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public double? DrawQuantity { get; set; }
        public string BillOfMaterialNumber { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as BillOfMaterialRecord);
        }

        public bool Equals(BillOfMaterialRecord other)
        {
            return other is not null &&
                   ParentItemCode == other.ParentItemCode &&
                   ChildItemCode == other.ChildItemCode &&
                   Location == other.Location &&
                   StartDate == other.StartDate &&
                   EndDate == other.EndDate &&
                   DrawQuantity == other.DrawQuantity &&
                   BillOfMaterialNumber == other.BillOfMaterialNumber;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ParentItemCode, ChildItemCode, Location, StartDate, EndDate, DrawQuantity, BillOfMaterialNumber);
        }

        public override string ToString()
        {
            return $"{BillOfMaterialNumber}";
        }
    }
}
