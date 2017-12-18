using System.Collections.Generic;
using CoTech.Bi.Core.Companies.Models;
using CoTech.Bi.Modules.Wer.Models.Entities;

namespace CoTech.Bi.Modules.Wer.Models.Files
{
    public class ReportsPdf
    {
        public string company { set; get; }
        public string color { set; get; }
        public List<(long,string,string)> responsables { set; get; }
        public List<ChildCompany> children { set; get; }

        public ReportsPdf(CompanyEntity entity)
        {
            this.company = entity.Name;
            this.color = entity.Color;
            this.responsables = new List<(long, string, string)>();
            this.children = new List<ChildCompany>();
        }
    }

    public class ChildCompany
    {
        public string company { set; get; }
        public List<DataReport> reports { set; get; }

        public ChildCompany(CompanyEntity entity)
        {
            this.company = entity.Name;
            this.reports = new List<DataReport>();
        }

        public string operative()
        {
            var data = "";
            foreach (var report in reports)
            {
                if (report.operative != null)
                {
                    data += report.operative + "\n";
                }
            }
            return data;
        }
        public string finance()
        {
            var data = "";
            foreach (var report in reports)
            {
                if (report != null)
                {
                    data += report.finance + "\n";
                }
            }
            return data;
        }
    }

    public class DataReport
    {
        public string operative { set; get; }
        public string finance { set; get; }

        public DataReport(ReportEntity entity)
        {
            this.finance = entity.Financial.Trim().Equals("") == true ? "" : entity.User.Name[0].ToString() + 
                           entity.User.Lastname[0].ToString() + ": " + entity.Financial;
            this.operative = entity.Operative.Trim().Equals("") == true ? "" : entity.User.Name[0].ToString() + 
                             entity.User.Lastname[0].ToString() + ": " + entity.Operative;
        }
    }
}