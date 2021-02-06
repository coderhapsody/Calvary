using System;

namespace Calvary.ViewModels.Activities.Worship
{
    public class ListWorshipViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Subject { get; set; }        
        public string WorshipTypeName { get; set; }
        public string Priest { get; set; }
    }
}
