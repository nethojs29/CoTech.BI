using System;

namespace CoTech.Bi.Modules.Sales.Models{
    public interface SSaleRequest{}
    
    public class CreateSSaleRequest:SSaleRequest{
        public double Date{ set; get; }
        public long CompanyId{ set; get; }

        public SSaleEntity toEntity(long userId){
            DateTime date = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddMilliseconds(Date).ToLocalTime();
            return new SSaleEntity {
                Date = date,
                CompanyId = CompanyId,
                Total = 0,
                CreatorId = userId,
                CreatedAt = DateTime.Now
            };
        }
    }

    public class UpdateSSaleRequest : SSaleRequest{
        public double Total{ set; get; }
        public double Date{ set; get; }
        public long CompanyId{ set; get; }
    }
}