namespace CoTech.Bi.Modules.Requisitions.Models{
    public class RequisitionFileEntity{
        public long Id { set; get; }
        public string Name { set; get; }
        public string Mime { set; get; }
        public string Uri { set; get; }
        public string Extension { set; get; }
        
        public long RequisitionId { set; get; }
        public RequisitionEntity Requisition { set; get; }
    }
}