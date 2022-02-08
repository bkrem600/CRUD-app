using System;
namespace UWS.Shared
{
    public class Invoice_Item
    {
        public int InvoiceLineId { get; set; }
        public int InvoiceId { get; set; }
        public int TrackId { get; set; }
        public decimal? UnitPrice { get; set; }
        public int Quantity { get; set; }

        //related entities
        public Invoice Invoice { get; set; }
        public Track Track { get; set; }      
    }
}