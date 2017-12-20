using System;
using CoTech.Bi.Modules.Budget.Models;

namespace CoTech.Bi.Modules.Budget.Controllers{
    public interface BudgetRequest{}

    public class CreateBudgetReq : BudgetRequest{
        public int Month{ set; get; }
        public int Year{ set; get; }
        public long ExpenseTypeId{ set; get; }
        public long DinningRoomId { set; get; }
        public int Type{ set; get; }
        public float Amount{ set; get; }
        public long CompanyId{ set; get; }

        public BudgetEntity toEntity(long userId){
            return new BudgetEntity {
                Month = Month,
                Year = Year,
                Amount = Amount,
                ExpenseTypeId = ExpenseTypeId,
                DinningRoomId = DinningRoomId,
                Type = Type,
                CompanyId = CompanyId,
                CreatedAt = DateTime.Now,
                CreatorId = userId
            };
        }
    }

    public class UpdateBudgetReq : BudgetRequest{
        public int Month{ set; get; }
        public int Year{ set; get; }
        public long ExpenseGroupId{ set; get; }
        public int Type{ set; get; }
        public float Amount{ set; get; }
    }
}