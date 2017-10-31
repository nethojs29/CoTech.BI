﻿using System;
using CoTech.Bi.Modules.Budget.Models;

namespace CoTech.Bi.Modules.Budget.Controllers{
    public interface BudgetConceptRequest{}

    public class CreateBudgetConceptReq : BudgetConceptRequest{
        public long BudgetId{ set; get; }
        public float Amount{ set; get; }
        public long CompanyId{ set; get; }

        public BudgetConceptEntity toEntity(long userId){
            return new BudgetConceptEntity {
                BudgetId = BudgetId,
                Amount = Amount,
                CompanyId = CompanyId,
                CreatorId = userId,
                CreatedAt = DateTime.Now
            };
        }
    }

    public class UpdateBudgetConceptReq : BudgetRequest{
        public long BudgetId{ set; get; }
        public float Amount{ set; get; }
        public long CompanyId{ set; get; }
    }
}