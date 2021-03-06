﻿using System;
using CoTech.Bi.Modules.Expenses.Models;

namespace CoTech.Bi.Modules.Expenses.Controllers{
    public interface ExpensesRequest{}

    public class CreateExpenseReq : ExpensesRequest{
        public float Price{ set; get; }
        public int Quantity{ set; get; }
        public string Observations{ set; get; }
        public long ProviderId{ set; get; }
        public long ExpenseGroupId{ set; get; }
        public long CompanyId{ set; get; }
        public long RequisitionId{ set; get; }

        public ExpenseEntity toEntity(){
            return new ExpenseEntity {
                Price = Price,
                Quantity = Quantity,
                ProviderId = ProviderId,
                Observations = Observations,
                ExpenseGroupId = ExpenseGroupId,
                RequisitionId = RequisitionId,
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

        public ExpenseTypeEntity toEntity(long userId){
            return new ExpenseTypeEntity {
                Name = Name,
                CompanyId = CompanyId,
                CreatorId = userId,
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

        public ExpenseGroupEntity toEntity(long userId){
            return new ExpenseGroupEntity {
                Name = Name,
                TypeId = TypeId,
                CompanyId = CompanyId,
                CreatorId = userId,
                CreatedAt = DateTime.Now
            };
        }
    }

    public class UpdateExpenseGroupReq : ExpensesRequest{
        public string Name{ set; get; }
        public int TypeId{ set; get; }
    }
}