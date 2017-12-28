using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoTech.Bi.Entity;
using CoTech.Bi.Modules.Invoices.Models;
using CoTech.Bi.Modules.Invoices.Requests;
using Microsoft.EntityFrameworkCore;

namespace CoTech.Bi.Modules.Invoices.Repositories{
    public class InvoicePaymentRepository{
        private readonly BiContext context;

        private DbSet<InvoicePayment> db => context.Set<InvoicePayment>();

        public InvoicePaymentRepository(BiContext context){
            this.context = context;
        }

        public Task<List<InvoicePayment>> getAll(long idCompany){
            return db.Where(p => !p.DeletedAt.HasValue && p.CompanyId == idCompany).ToListAsync();
        }

        public Task<List<InvoicePayment>> getAllByInvoice(long idCompany, long idInvoice){
            return db.Where(i => !i.DeletedAt.HasValue && i.InvoiceId == idInvoice && i.CompanyId == idCompany).ToListAsync();
        }

        public Task<InvoicePayment> WithId(long id){
            return db.FindAsync(id);
        }

        public async Task Create(InvoicePayment payment){
            db.Add(payment);
            await context.SaveChangesAsync();
        }

        public async Task Update(long id, UpdateInvoicePaymentReq payment){
            var pay = db.Find(id);
            context.Entry(pay).CurrentValues.SetValues(payment);
            await context.SaveChangesAsync();
        }

        public async Task Delete(InvoicePayment payment){
            payment.DeletedAt = DateTime.Now;
            db.Update(payment);
            await context.SaveChangesAsync();
        }
    }
}