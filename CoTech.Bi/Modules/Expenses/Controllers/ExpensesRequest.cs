using System;
using CoTech.Bi.Modules.Expenses.Models;

namespace CoTech.Bi.Modules.Expenses.Controllers{
    public interface ExpensesRequest{}

    public class CreateExpenseReq : ExpensesRequest{
        public string Description{ set; get; }
        public float Price{ set; get; }
        public int Quantity{ set; get; }
        public string Observations{ set; get; }
        public string ImageUrl{ set; get; }
        public long ProviderId{ set; get; }
        public long ExpenseGroupId{ set; get; }
        public long CompanyId{ set; get; }

        public ExpenseEntity toEntity(){
            return new ExpenseEntity {
                Description = Description,
                Price = Price,
                Quantity = Quantity,
                Observations = Observations,
                ImageUrl = ImageUrl,
                ExpenseGroupId = ExpenseGroupId,
                CompanyId = CompanyId,
                CreatedAt = DateTime.Now
            };
        }
    }

    public class UpdateExpenseReq : ExpensesRequest{
        public string Description{ set; get; }
        public float Price{ set; get; }
        public int Quantity{ set; get; }
        public string Observations{ set; get; }
        public string ImageUrl{ set; get; }
        public long ProviderId{ set; get; }
        public long ExpenseGroupId{ set; get; }
    }

    public class CreateExpenseTypeReq : ExpensesRequest{
        public string Name{ set; get; }
        public int CompanyId{ set; get; }

        public ExpenseTypeEntity toEntity(){
            return new ExpenseTypeEntity {
                Name = Name,
                CompanyId = CompanyId,
                CreatedAt = DateTime.Now
            };
        }
    }

    public class UpdateExpenseTypeReq : ExpensesRequest{
        public string Name{ set; get; }
    }

    public class CreateExpenseGroupReq : ExpensesRequest{
        public string Name{ set; get; }
        public int TypeId{ set; get; }
        public int CompanyId{ set; get; }

        public ExpenseGroupEntity toEntity(){
            return new ExpenseGroupEntity {
                Name = Name,
                TypeId = TypeId,
                CompanyId = CompanyId,
                CreatedAt = DateTime.Now
            };
        }
    }

    public class UpdateExpenseGroupReq : ExpensesRequest{
        public string Name{ set; get; }
        public int TypeId{ set; get; }
    }
}