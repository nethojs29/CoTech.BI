namespace CoTech.Bi.Core.Companies.Models{
    public interface DepartmentRequest{}

    public class CreateDepartmentReq : DepartmentRequest{
        public string Name{ set; get; }
        public long CompanyId{ set; get; }

        public DepartmentEntity toEntity(){
            return new DepartmentEntity {
                Name = Name,
                CompanyId = CompanyId
            };
        }
    }

    public class UpdateDepartmentReq : DepartmentRequest{
        public string Name{ set; get; }
    }
}