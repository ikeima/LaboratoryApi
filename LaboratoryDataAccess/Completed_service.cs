//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LaboratoryDataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class Completed_service
    {
        public int Completed_service_id { get; set; }
        public int Service_code { get; set; }
        public System.DateTime Complete_date { get; set; }
        public int Technician_id { get; set; }
        public int Analyzer_id { get; set; }
    
        public virtual Analyzers Analyzers { get; set; }
        public virtual Services Services { get; set; }
    }
}
