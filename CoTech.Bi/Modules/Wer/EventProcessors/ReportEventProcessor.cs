using System;
using System.Reactive.Linq;
using CoTech.Bi.Core.EventSourcing.Models;
using CoTech.Bi.Entity;
using CoTech.Bi.Modules.Wer.Models.Entities;
using CoTech.Bi.Modules.Wer.Models.Events;
using EntityFrameworkCore.Rx;
using EntityFrameworkCore.Triggers;

namespace CoTech.Bi.Modules.Wer.EventProcessors
{
    public class ReportEventProcessor
    {
        public ReportEventProcessor()
        {

            var eventObs = DbObservable<BiContext>.FromInserting<EventEntity>();
            eventObs.Where(evt => evt.Entity.Body is ReportUpdatedEvt).Subscribe(OnUpdate);
        }

        private void OnUpdate(IBeforeEntry<EventEntity, BiContext> entry)
        {
            var _dbReport = entry.Context.Set<ReportEntity>();
            var body = entry.Entity.Body as ReportUpdatedEvt;
            var report = _dbReport.Find(body.Id);
            if (report == null)
            {
                entry.Cancel = true;
                return;
            }
            report.Updated = DateTime.Now;
            entry.Context.Entry(report).CurrentValues.SetValues(body);
        }
    }
}